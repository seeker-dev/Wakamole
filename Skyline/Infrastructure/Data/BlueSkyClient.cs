using System.Text.Json;
using System.Text.Encodings.Web;
using Skyline.Core.Entities;
using Skyline.Core.Interfaces;
using Skyline.Infrastructure.Configuration;

namespace Skyline.Infrastructure.Data
{
    public class BlueSkyClient : IBlueSkyClient, IDisposable
    {
        private string? decentralizedIdentifier;
        private readonly HttpClient _httpClient;

        public BlueSkyClient(BlueSkyConfiguration config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.BaseUrl),
                DefaultRequestHeaders =
                {
                    {"User-Agent", "Wakamole/1.0"},
                }
            };
        }

        public async Task LoginAsync(string username, string password)
        {
            try
            {
                var authRequest = new
                {
                    identifier = username,
                    password = password
                };

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var json = JsonSerializer.Serialize(authRequest, options);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/xrpc/com.atproto.server.createSession", content);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var session = JsonSerializer.Deserialize<Session>(responseContent, options);

                // Store the session information as needed
                if (session != null)                
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessJwt);
                    decentralizedIdentifier = session.Did;
                }
            }
            catch (HttpRequestException)
            {   
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogoutAsync()
        {
            if (string.IsNullOrEmpty(decentralizedIdentifier))
            {
                throw new InvalidOperationException("Not logged in.");
            }

            var response = await _httpClient.PostAsync("/xrpc/com.atproto.server.deleteSession", null);
            response.EnsureSuccessStatusCode();

            // Clear the session information
            _httpClient.DefaultRequestHeaders.Authorization = null;
            decentralizedIdentifier = null;
        }

        // Methods to interact with the BlueSky API would go here
        public async Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            var response = await _httpClient.GetAsync("/xrpc/app.bsky.actor.getPreferences");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(responseContent);
            var preferences = document.RootElement.GetProperty("preferences").EnumerateArray();
            var savedFeeds = from preference in preferences
                where preference.GetProperty("$type").GetString() == "app.bsky.actor.defs#savedFeedsPrefV2"
                from feed in preference.GetProperty("items").EnumerateArray()
                select new SavedFeed
                {
                    id = feed.GetProperty("id").GetString(),
                    type = feed.GetProperty("type").GetString(),
                    value = feed.GetProperty("value").GetString()
                };

            var feedUris = savedFeeds
                .Where(f => f.value != null && f.value.StartsWith("at://"))
                .Select(f => Uri.EscapeDataString(f.value!));
            var feedString = string.Join("&feeds=", feedUris);
            var feedGenerators = await _httpClient.GetAsync($"/xrpc/app.bsky.feed.getFeedGenerators?feeds={feedString}");
            feedGenerators.EnsureSuccessStatusCode();

            var responseContent2 = await feedGenerators.Content.ReadAsStringAsync();
            var feedsElement = JsonDocument.Parse(responseContent2).RootElement.GetProperty("feeds");
            var feeds = feedsElement.EnumerateArray().Select(f => JsonSerializer.Deserialize<Feed>(f.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })).Where(f => f != null).Select(f => f!).ToList();

            return feeds ?? Enumerable.Empty<Feed>();
        }

        public async Task<FeedResponse> GetPostsAsync(string feedId, int limit = 30, string cursor = "")
        {
            var url = $"/xrpc/app.bsky.feed.getFeed?feed={feedId}";
            if (!string.IsNullOrEmpty(cursor))
            {
                url += $"&cursor={Uri.EscapeDataString(cursor)}";
            }

            url += $"&limit={limit}";

            var response = await _httpClient.GetAsync(url);

            // Ran into cursor being expired before, so trying again without it
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && !string.IsNullOrEmpty(cursor))
            {
                url = $"/xrpc/app.bsky.feed.getFeed?feed={feedId}&limit={limit}";
                response = await _httpClient.GetAsync(url);
            }
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var feedResponse = JsonSerializer.Deserialize<FeedResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return feedResponse ?? new FeedResponse();
        }

        public async Task<Author> GetAuthorAsync(string did)
        {
            var response = await _httpClient.GetAsync($"/xrpc/app.bsky.actor.getProfile?actor={did}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var author = JsonSerializer.Deserialize<Author>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return author;
        }
        
        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        // For later
        /* In the Domain or Core layer, define a Moderation service that filters posts based on settings.  One function looks at the posts and searches for unwanted words in descriptions for example */
    }
}
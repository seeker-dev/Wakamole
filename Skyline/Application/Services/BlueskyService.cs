using Skyline.Application.DTOs;
using Skyline.Application.Interfaces;
using Skyline.Core.DTOs;
using Skyline.Core.Interfaces;
using Skyline.Core.Entities;

namespace Skyline.Application.Services;

public class BlueskyService(IBlueSkyClient blueSkyClient) : IBlueskyService
{
    private readonly IBlueSkyClient _blueSkyClient = blueSkyClient;

    public async Task LoginAsync(string username, string password)
    {
        await _blueSkyClient.LoginAsync(username, password);
    }

    public async Task<IEnumerable<FeedDto>> ListUsersFeedsAsync()
    {
        var feeds = await _blueSkyClient.GetFeedsAsync();
        return feeds.Select(f => new FeedDto
        {
            Name = f.displayName,
            Url = f.uri
        });
    }

    public async Task<PagedResult<PostDto>> GetFeedPostsAsync(string feedId, int limit = 30, string cursor = "")
    {
        var response = await _blueSkyClient.GetPostsAsync(feedId, limit, cursor);
        var pagedResult = new PagedResult<PostDto>
        {
            Items = [.. response.Feed.Select(ToPostDto)],
            NextPageToken = response.Cursor
        };
        return pagedResult;
    }

    public async Task<string> GetProfileDescriptionAsync(string did)
    {
        var author = await _blueSkyClient.GetAuthorAsync(did);
        return author.Description;
    }

    private PostDto ToPostDto(FeedItem item)
    {
        var dto = new PostDto
        {
            Content = item.Post.record.Text,
            Url = item.Post.uri,
            AuthorName = item.Post.author.DisplayName,
            AuthorHandle = item.Post.author.Handle,
            AuthorDid = item.Post.author.Did,
            AuthorDescription = item.Post.author.Description,
            CreatedAt = item.Post.record.CreatedAt
        };

        if (item.Post.embed != null)
        {
            if (item.Post.embed.cid != null)
            {
                dto.EmbeddedVideos = new List<EmbeddedVideo>
                {
                    new() {
                        cid = item.Post.embed.cid,
                        playlist = item.Post.embed.playlist,
                        aspectRatioHeight = item.Post.embed.aspectRatio.height,
                        aspectRatioWidth = item.Post.embed.aspectRatio.width,
                        thunbnail = item.Post.embed.thunbnail
                    }
                };
            }

            if (item.Post.embed.images != null)
            {
                dto.EmbeddedImages = [.. item.Post.embed.images.Select(x => new EmbeddedImage
                {
                    alt = x.Alt,
                    fullsize = x.Fullsize,
                    thumb = x.Thumb,
                    aspectRatioHeight = x.aspectRatio.height,
                    aspectRatioWidth = x.aspectRatio.width
                })];
            }
        }

        return dto;
    }
}
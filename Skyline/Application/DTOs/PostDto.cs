namespace Skyline.Application.DTOs
{
    public class PostDto
    {
        public string Url { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorHandle { get; set; }
        public string AuthorDid { get; set; }
        public string AuthorDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<EmbeddedVideo>? EmbeddedVideos { get; set; }
        public List<EmbeddedImage>? EmbeddedImages { get; set; }
        public bool IsInViolation { get; set; } = false;
    }

    public class EmbeddedVideo
    {
        public string cid { get; set; }
        public string playlist { get; set; }
        public string thunbnail { get; set; }
        public int aspectRatioWidth { get; set; }
        public int aspectRatioHeight { get; set; }
    }

    public class EmbeddedImage
    {
        public string thumb { get; set; }
        public string fullsize { get; set; }
        public string alt { get; set; }
        public int aspectRatioWidth { get; set; }
        public int aspectRatioHeight { get; set; }
    }
}
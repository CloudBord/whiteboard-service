using System.Text.Json.Serialization;

namespace Whiteboard.Service.Requests
{
    public class SaveBoardRequest
    {
        [JsonPropertyName("id")]
        public required uint Id { get; set; }
        [JsonPropertyName("boardContents")]
        public required string BoardContents { get; set; }
    }
}

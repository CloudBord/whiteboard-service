using System.Text.Json.Serialization;

namespace Whiteboard.Service.Request
{
    public class CreateBoardRequest
    {
        [JsonPropertyName("name")]
        public required string Title { get; set; }

        public CreateBoardRequest() { }
    }
}

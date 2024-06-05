using System.Text.Json.Serialization;

namespace Whiteboard.Service.Request
{
    public class CreateBoardRequest
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        public CreateBoardRequest() { }
    }
}

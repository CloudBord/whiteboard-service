using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Whiteboard.Service.Request
{
    public class CreateBoardRequest
    {
        [JsonProperty("title")]
        [NotNull]
        public required string Title { get; set; }

        public CreateBoardRequest() { }
    }
}

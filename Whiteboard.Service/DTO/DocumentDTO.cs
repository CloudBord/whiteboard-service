using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.DTO
{
    public class DocumentDTO
    {
        [JsonProperty("boardId")]
        public uint BoardId { get; set; }
        [JsonProperty("memberIds")]
        public IEnumerable<Guid> MemberIds { get; set; } = [];
    }
}

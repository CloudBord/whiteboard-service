using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.DataAccess.Models
{
    public class Board
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("ownerId")]
        public Guid OwnerId { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("memberIds")]
        public List<Guid> MemberIds { get; set; } = [];

        public Board() { }
    }
}

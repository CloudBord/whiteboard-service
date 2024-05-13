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
        [JsonProperty("boardId")]
        public required string BoardId { get; set; }
        [JsonProperty("ownerId")]
        public required string OwnerId { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("memberIds")]
        public List<string> MemberIds { get; set; } = [];

        public Board() 
        {
            if(OwnerId != null && !MemberIds.Contains(OwnerId)) MemberIds.Add(OwnerId);
        }
    }
}

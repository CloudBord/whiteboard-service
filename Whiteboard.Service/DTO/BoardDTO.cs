using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Models
{
    public class BoardDTO
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }

        public BoardDTO() {}
    }
}

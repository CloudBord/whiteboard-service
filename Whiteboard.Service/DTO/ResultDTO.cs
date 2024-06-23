using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.DTO
{
    public class ResultDTO
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("boardId")]
        public uint BoardId { get; set; }
    }
}

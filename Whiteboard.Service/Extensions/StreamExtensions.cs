using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Extensions
{
    public static class StreamExtensions
    {
        public async static Task<dynamic> Deserialize<T>(this Stream stream)
        {
            using (var reader = new StreamReader(stream, leaveOpen: true))
            {
                string text = await reader.ReadToEndAsync();
                stream.Position = 0;
                return JsonConvert.DeserializeObject<T>(text)!;
            }
        }
    }
}

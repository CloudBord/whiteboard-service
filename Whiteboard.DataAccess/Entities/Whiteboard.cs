using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.DataAccess.Entities
{
    public class Whiteboard
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public Guid BoardId { get; set; }
        public int OwnerId { get; set; }
        public required string Name { get; set; }
        public ICollection<int> MemberIds { get; set; } = [];

        public Whiteboard() 
        {
            if(!MemberIds.Contains(OwnerId)) MemberIds.Add(OwnerId);
        }
    }
}

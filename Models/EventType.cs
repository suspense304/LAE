using LAE.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LostArkEng.Models
{
    public class EventType
    {
        [Key]
        public int TypeId { get; set; }
        public string Type { get; set; }
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GameDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Developer { get; set; } = "Unknown";
        public int? YearOfRelease { get; set; }
        public List<string> Platforms { get; set; } = new();
        public float? Rating { get; set; }
        public string Description { get; set; } = "Empty";
        public string Icon { get; set; }
        public List<string> Screenshots { get; set; } = new();
        public List<ReviewDTO> Reviews { get; set; } = new();
    }
}

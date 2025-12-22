using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Game : IDomainObject
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Developer { get; set; } = "Unknown";
        public int? YearOfRelease { get; set; }
        public List<EnumPlatforms> Platforms { get; set; }
        public float? Rating { get; set; }
        public string Description { get; set; } = "Empty";
        public string Icon { get; set; }
        public List<string> Screenshots { get; set; }
        public List<Review> Reviews { get; set; }
    }
}

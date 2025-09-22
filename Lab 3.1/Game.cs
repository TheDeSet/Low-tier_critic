using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3._1
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Developer { get; set; } = "Unknown";
        public int? YearOfRelease { get; set; }
        public List<EnumPlatforms> Platforms { get; set; }
        public float? Rating { get; set; }
        public string Description { get; set; } = "Empty";
        public Image Icon { get; set; }
        public List<Image> Screenshots { get; set; }
        public List<Review> Reviews { get; set; }
    }
}

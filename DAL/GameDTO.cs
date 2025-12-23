using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GameDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public int YearOfRelease { get; set; }
        public string Platforms { get; set; }
        public float? Rating { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; }
        public string? Screenshots { get; set; }
        public string? Reviews { get; set; }
    }
}

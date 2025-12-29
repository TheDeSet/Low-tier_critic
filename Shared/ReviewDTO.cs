using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ReviewDTO
    {
        public int? ID { get; set; }
        public string? Username { get; set; }
        public float Rating { get; set; }
        public string ReviewText { get; set; } = "Empty";
        public int GameId { get; set; }
    }
}

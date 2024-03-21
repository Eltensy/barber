using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int fk_ClientId { get; set; }
        public int fk_BarberId { get; set; }
        public string? Text { get; set; }
        public float Rating { get; set; }
        public DateTime Date { get; set; }
    }
}

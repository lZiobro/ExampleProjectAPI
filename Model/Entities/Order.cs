using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string pictureUrl { get; set; }
        public decimal price { get; set; }

        public string productBrand { get; set; }=null!;
        public string productType { get; set; }=null!;
    }
}

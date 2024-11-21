using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Application.DTOs
{
    public class UpdateOrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

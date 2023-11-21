using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts
{
	public class Order
	{
		public int Id { get; set; }
		public decimal TotalPrice { get; set; } = 0;
		public bool ResultStatus { get; set; } = false;
		public int CustomerId { get; set; }
		public bool NeedForDelivery { get; set; }
		public List<int> ProductsId { get; } = new();
		public List<OrdersProducts> OrdersProducts { get; } = new();
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SalesPersonId { get; set; }
        public DateTime Date { get; set; }
        public int BoatId { get; set; }
        public string DeliveryAddress { get; set; }
        public string City { get; set; }
        public int Status { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public Customer Customer { get; set; }
        public Boat Boat { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}

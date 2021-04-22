using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Accessory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Vat { get; set; }
        public int Inventory { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public IEnumerable<AccessoryToBoat> AccessoryToBoat { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}

namespace WorldYachts.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; } 
        public int AccessoryId { get; set; }
        public int OrderId { get; set; }
        public Accessory Accessory { get; set; }
        public Order Order { get; set; }
    }
}

namespace WorldYachts.Data.Entities
{
    public class AccessoryToBoat
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public int AccessoryId { get; set; }
        public Boat Boat { get; set; }
        public Accessory Accessory { get; set; }

        public const string Path = "accessories/compatibilities";
    }
}

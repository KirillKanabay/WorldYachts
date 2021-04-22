using System.Collections.Generic;

namespace WorldYachts.Data.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public IEnumerable<Accessory> Accessories { get; set; }
    }
}

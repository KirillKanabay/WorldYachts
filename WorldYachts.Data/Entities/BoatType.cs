using System;

namespace WorldYachts.Data.Entities
{
    public class BoatType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}{Environment.NewLine}" +
                   $"Type: {Type}";
        }
    }
}

using System;

namespace WorldYachts.Data.Entities
{
    public class BoatWood
    {
        public int Id { get; set; }
        public string Wood { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}{Environment.NewLine}" +
                   $"Тип дерева: {Wood}";
        }
    }
}

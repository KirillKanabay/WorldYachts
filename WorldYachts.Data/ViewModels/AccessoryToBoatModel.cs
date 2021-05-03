using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.ViewModels
{
    public class AccessoryToBoatModel
    {
        [Range(0, Double.MaxValue)]
        public int AccessoryId { get; set; }
        
        [Range(0, Double.MaxValue)]
        public int BoatId { get; set; }
    }
}

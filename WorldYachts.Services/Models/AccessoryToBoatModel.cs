using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Services.Models
{
    public class AccessoryToBoatModel
    {
        [Range(0, Double.MaxValue)]
        public int AccessoryId { get; set; }
        
        [Range(0, Double.MaxValue)]
        public int BoatId { get; set; }
    }
}

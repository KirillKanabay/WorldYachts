using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.ViewModels
{
    public class BoatWoodModel
    {
        [MaxLength(64)]
        public string Wood { get; set; }
    }
}

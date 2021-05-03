using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.ViewModels
{
    public class BoatTypeModel
    {
        [MaxLength(64)]
        public string Type { get; set; }
    }
}

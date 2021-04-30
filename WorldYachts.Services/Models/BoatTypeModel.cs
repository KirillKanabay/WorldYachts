using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class BoatTypeModel
    {
        [MaxLength(64)]
        public string Type { get; set; }
    }
}

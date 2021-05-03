using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.ViewModels
{
    public class OrderDetailModel
    {
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int AccessoryId { get; set; }

        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required] public int OrderId { get; set; }
    }
}

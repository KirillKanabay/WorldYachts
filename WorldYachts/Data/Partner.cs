using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorldYachts.Data
{
    public class Partner
    {
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        [Required] public int Id { get; set; }

        /// <summary>
        /// Название партнера
        /// </summary>
        [Required] public string Name { get; set; }
        
        /// <summary>
        /// Адрес партнера
        /// </summary>
        [Required] public string Address { get; set; }

        /// <summary>
        /// Город нахождения партнера
        /// </summary>
        [Required] public string City { get; set; }
    }
}

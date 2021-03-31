using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class Partner : IPartner
    {
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Название партнера
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Город нахождения партнера
        /// </summary>
        [Required]
        public string City { get; set; }
    }
}
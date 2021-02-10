using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace WorldYachts.Data
{
    public class Partner : IComparable, IComparer
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

        public int CompareTo(object? obj)
        {
            return Compare(this, obj);
        }

        public int Compare(object? x, object? y)
        {
            var partner1 = (Partner) x;
            var partner2 = (Partner) y;

            return String.Compare(partner1.Name, partner2.Name, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
        }
    }
}
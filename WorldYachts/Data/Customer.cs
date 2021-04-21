using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.Data
{
    public class Customer : IComparable, IComparer
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Имя клиента
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Город клиента
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Email клиента
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Название организации клиента
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        [Required]
        public string IdNumber { get; set; }

        /// <summary>
        /// Название документа
        /// </summary>
        [Required]
        public string IdDocumentName { get; set; }

        [ForeignKey("CustomerId")] public List<Order> Orders { get; set; }

        public int CompareTo(object? obj)
        {
            return Compare(this, obj);
        }

        public int Compare(object? x, object? y)
        {
            var customer1 = (Customer) x;
            var customer2 = (Customer) y;
            if (String.Compare(customer1.IdNumber, customer2.IdNumber, CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreCase) == 0 ||
                String.Compare(customer1.Phone, customer2.Phone, CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreCase) == 0 ||
                String.Compare(customer1.Email, customer2.Email, CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreCase) == 0)
            {
                return 0;
            }

            return 1;
        }
    }
}
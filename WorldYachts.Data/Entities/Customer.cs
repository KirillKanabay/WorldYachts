using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Customer:IUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
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
    }
}

using System;
using System.Collections.Generic;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface ICustomer
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        string SecondName { get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        DateTime BirthDate { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Город клиента
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        string Phone { get; set; }

        /// <summary>
        /// Email клиента
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Название организации клиента
        /// </summary>
        string OrganizationName { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        string IdNumber { get; set; }

        /// <summary>
        /// Название документа
        /// </summary>
        string IdDocumentName { get; set; }

        List<IOrder> Orders { get; set; }
    }
}
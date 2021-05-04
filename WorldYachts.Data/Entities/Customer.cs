using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Customer:IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string OrganizationName { get; set; }
        public string IdNumber { get; set; }
        public string IdDocumentName { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}{Environment.NewLine}" +
                   $"Имя: {FirstName}{Environment.NewLine}" +
                   $"Фамилия: {SecondName}{Environment.NewLine}" +
                   $"Дата рождения: {BirthDate:yyyy MMMM dd}{Environment.NewLine}" +
                   $"Город: {City}{Environment.NewLine}" +
                   $"Адрес: {Address}{Environment.NewLine}" +
                   $"Номер телефона: {Phone}{Environment.NewLine}" +
                   $"Название организации: {OrganizationName}{Environment.NewLine}";
        }
    }
}

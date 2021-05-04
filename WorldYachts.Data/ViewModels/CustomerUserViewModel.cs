using System;
using WorldYachts.Data.Entities;

namespace WorldYachts.Data.ViewModels
{
    public class CustomerUserViewModel:UserViewModel
    {
        public CustomerUserViewModel()
        {
        }

        public CustomerUserViewModel(Customer customer, string username, string email, string password)
        {
            FirstName = customer.FirstName;
            SecondName = customer.SecondName;
            BirthDate = customer.BirthDate;
            Address = customer.Address;
            City = customer.City;
            Phone = customer.Phone;
            Email = email;
            OrganizationName = customer.OrganizationName;
            IdNumber = customer.IdNumber;
            IdDocumentName = customer.IdDocumentName;
            Username = username;
            Password = password;
        }
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
    }
}

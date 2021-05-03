using WorldYachts.Data.Entities;

namespace WorldYachts.Data.ViewModels
{
    public class SalesPersonUserViewModel:UserViewModel
    {
        public SalesPersonUserViewModel()
        {
        }

        public SalesPersonUserViewModel(SalesPerson sp, string username, string email, string password)
        {
            FirstName = sp.FirstName;
            SecondName = sp.SecondName;
            Username = username;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}

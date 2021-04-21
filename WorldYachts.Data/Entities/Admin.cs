using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Admin:IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}

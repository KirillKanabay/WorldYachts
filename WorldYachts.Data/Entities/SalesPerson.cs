using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class SalesPerson:IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}{Environment.NewLine}" +
                   $"Имя: {FirstName}{Environment.NewLine}" +
                   $"Фамилия: {SecondName}{Environment.NewLine}";
        }
    }
}

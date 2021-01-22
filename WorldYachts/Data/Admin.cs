using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data
{
    public class Admin:IUser
    {
        /// <summary>
        /// Идентификатор администратора
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя администратора
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия администратора
        /// </summary>
        public string SecondName { get; set; }
    }
}

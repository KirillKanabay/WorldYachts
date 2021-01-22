using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data
{
    public interface IUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string SecondName { get; set; }

    }
}

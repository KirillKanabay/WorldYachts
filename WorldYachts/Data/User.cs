using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Controls.Primitives;

namespace WorldYachts.Data
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        [Required] public int TypeUser { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required] public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required] public string Password { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required] public int UserId { get; set; }


    }
}

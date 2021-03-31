using System.ComponentModel.DataAnnotations;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class User : IUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        [Required] public int TypeUser { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required] public string Login { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required] public int UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using WorldYachts.DataLibrary.Models.Interfaces;

namespace WorldYachts.DataLibrary.Models
{
    public class Admin : IAdmin
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

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
    }
}

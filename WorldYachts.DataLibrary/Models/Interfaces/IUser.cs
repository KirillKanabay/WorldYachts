namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        int TypeUser { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        string Login { get; set; }
        
        /// <summary>
        /// Id пользователя
        /// </summary>
        int UserId { get; set; }
    }
}
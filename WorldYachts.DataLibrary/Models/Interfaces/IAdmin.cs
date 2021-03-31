namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IAdmin
    {
        /// <summary>
        /// Идентификатор администратора
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Имя администратора
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Фамилия администратора
        /// </summary>
        string SecondName { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface IPartner
    {
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Название партнера
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Город нахождения партнера
        /// </summary>
        string City { get; set; }
    }
}
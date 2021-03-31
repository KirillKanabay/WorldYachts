using System.Collections.Generic;

namespace WorldYachts.DataLibrary.Models.Interfaces
{
    public interface ISalesPerson
    {
        /// <summary>
        /// Идентификатор менеджера 
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Имя менеджера
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Фамилия менеджера
        /// </summary>
        string SecondName { get; set; }

        List<IOrder> Orders { get; set; }
    }
}
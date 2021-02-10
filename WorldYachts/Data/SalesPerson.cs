﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.Data
{
    public class SalesPerson:IUser
    {
        /// <summary>
        /// Идентификатор менеджера 
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Имя менеджера
        /// </summary>
        [Required] public string Name { get; set; }
        
        /// <summary>
        /// Фамилия менеджера
        /// </summary>
        [Required] public string SecondName { get; set; }

        [ForeignKey("SalesPersonId")]
        public List<Order> Orders { get; set; }
    }
}

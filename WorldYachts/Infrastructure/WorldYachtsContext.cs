using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using Admin = WorldYachts.Data.Admin;
using Customer = WorldYachts.Data.Customer;

namespace WorldYachts.Infrastructure
{
    public class WorldYachtsContext:DbContext
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        public DbSet<Data.AccessoryToBoat> AccessoryToBoat { get; set; }
        /// <summary>
        /// Лодки
        /// </summary>
        public DbSet<Boat> Boats { get; set; }
        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Contract> Contracts { get; set; }
        /// <summary>
        /// Покупатели
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// Счета
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }
        /// <summary>
        /// Доставки
        /// </summary>
        public DbSet<Data.Order> Orders { get; set; }
        /// <summary>
        /// Критерии доставки
        /// </summary>
        public DbSet<OrderDetails> OrderDetails { get; set; }
        /// <summary>
        /// Менеджеры
        /// </summary>
        public DbSet<SalesPerson> SalesPersons { get; set; }
        /// <summary>
        /// Администраторы
        /// </summary>
        public DbSet<Admin> Admin { get; set; }
        public WorldYachtsContext(DbContextOptions<WorldYachtsContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=worldyachtsdb;Trusted_Connection=True;");
        }
        public static WorldYachtsContext GetDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorldYachtsContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=worldyachtsdb;Trusted_Connection=True;")
                .Options;
            return new WorldYachtsContext(options);
        }
    }
}

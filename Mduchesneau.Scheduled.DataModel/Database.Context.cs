﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mduchesneau.Scheduled.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database : DbContext
    {
        /// <summary></summary>
        private static Database database;

        /// <summary>Return the database database.</summary>
        public static Database getInstance()
        {
            if (database == null)
                database = new Database();
            return database;
        }

        public Database()
            : base("name=Database")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; }
    }
}
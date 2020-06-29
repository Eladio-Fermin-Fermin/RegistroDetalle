using Microsoft.EntityFrameworkCore;
using Registro_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registro_Detalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<PrestamoMoras> PrestamoMoras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\PrestamoMora.db");
        }
    }
}

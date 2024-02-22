using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTarefasVitor.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

    }

}

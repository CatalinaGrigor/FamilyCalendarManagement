using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tpCatalinaV4.Models;

namespace tpCatalinaV4.Data
{
    public class tpCatalinaV4Context : DbContext
    {
        public tpCatalinaV4Context (DbContextOptions<tpCatalinaV4Context> options)
            : base(options)
        {
        }

        public DbSet<tpCatalinaV4.Models.Horaire> Horaire { get; set; } = default!;

        public DbSet<tpCatalinaV4.Models.Tache> Tache { get; set; }

        public DbSet<tpCatalinaV4.Models.Utilisateur> Utilisateur { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALabs.Database
{
    public class UserDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source = Alabs.db");
        }

        public void UpdateUser(User user)
        {
            // Attach the user to the context if it's not already tracked
            if (!Users.Local.Any(u => u.Id == user.Id))
            {
                Entry(user).State = EntityState.Modified;
            }

            SaveChanges();
        }

        public DbSet<User> Users { get; set; }
    }
}

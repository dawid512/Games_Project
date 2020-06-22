using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesProject
{
    class Context :DbContext
    {
       // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
          //  base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().ToTable("User");
            
        //}



        public Context() : base()
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }
        public DbSet<Users> Uzytkownik { get; set; }


    }






}

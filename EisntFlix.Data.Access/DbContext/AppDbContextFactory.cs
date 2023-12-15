using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace EisntFlix.Data.Access.DbContenxt
//{
//    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//            string? connectionString = "<string-com-dados-de-conexão-da-base-de-dados>";

//            optionsBuilder.UseSqlServer(connectionString);
//            // UseSqlServer é pra base de dados da Microsoft, 
//            // se etiver usando outro tem que mudar pro apropriado.

//            return new AppDbContext(optionsBuilder.Options);
//        }
//    }
//}
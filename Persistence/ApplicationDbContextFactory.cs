using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Creates an instance of <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="args">The arguments passed. To pass the connection string using the "netcore ef" command you need to add "-- \<connectionstring\>".
        /// To pass the connectionstring in the package manager console, you need to use -Args '"\<connectionString\>"'. 
        /// Note that you need to use "(double quotes) inside '(songle quotes) for the package manager console. The string is considered a list of args
        /// and you need to specify a single arg inside double quotes</param>
        /// <returns>an instance of <see cref="ApplicationDbContext"/></returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

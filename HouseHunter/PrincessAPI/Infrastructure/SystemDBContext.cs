using System.Data.Entity;
using PrincessAPI.Models;

namespace PrincessAPI.Infrastructure
{
    public class SystemDBContext : DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<HighscoreModel> HighscoreModels { get; set; }
    }
}
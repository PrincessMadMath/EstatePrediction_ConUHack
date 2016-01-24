using System.Data.Entity;
using PrincessAPI.Models;

namespace PrincessAPI.Infrastructure
{
    public class SystemDBContext : DbContext
    {
        public DbSet<HouseModel> Houses { get; set; }
    }
}
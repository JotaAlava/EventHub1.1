using System.Data.Entity;
using EventHub1._1.Models;

namespace EventHub1._1.DAL
{
    public class EventHubDb : DbContext
    {
        DbSet<Location> Locations { get; set; }
    }
}
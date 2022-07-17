using AAC.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace AAC.Infra.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
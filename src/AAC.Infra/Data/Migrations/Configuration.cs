using System.Data.Entity.Migrations;
using AAC.Infra.Data.Context;

namespace AAC.Infra.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
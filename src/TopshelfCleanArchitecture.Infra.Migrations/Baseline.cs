using FluentMigrator;
using System.Data.SqlServerCe;

namespace TopshelfCleanArchitecture.Infra.Migrations
{
    [Migration(1, "Tools")]
    public class Baseline : Migration
    {
        public override void Up()
        {
            Create.Table("Product")
                .WithColumn("Id").AsInt32().Identity()
                .WithColumn("Name").AsString(100)
                .WithColumn("Price").AsFloat();

            Insert.IntoTable("Product").Row(new { Name = "Potatoes", Price = 3.6 });
            Insert.IntoTable("Product").Row(new { Name = "Fish", Price = 4.49 });
            Insert.IntoTable("Product").Row(new { Name = "Milk", Price = 0.79 });
            Insert.IntoTable("Product").Row(new { Name = "Bread", Price = 1.29 });
            Insert.IntoTable("Product").Row(new { Name = "Cheese", Price = 2.1 });
            Insert.IntoTable("Product").Row(new { Name = "Waffles", Price = 2.41 });
        }

        public override void Down()
        {
            Delete.Table("Product");
        }

    }

    [Migration(0)]
    public class Setup : Migration
    {
        public override void Up()
        {

        }

        public override void Down()
        {

        }
    }
}

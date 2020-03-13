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

            InsertProduct("Potatoes", 3.6);
            InsertProduct("Fish", 4.49);
            InsertProduct("Milk", 0.79);
            InsertProduct("Bread", 1.29);
            InsertProduct("Cheese", 2.1);
            InsertProduct("Waffles", 2.41);
        }

        void InsertProduct(string name, double price)
        {
            Insert.IntoTable("Product").Row(new { Name = name, Price = (float)price });
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

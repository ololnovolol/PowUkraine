using FluentMigrator;

namespace Pow.Infrastructure.Migrations
{
    [Migration(20221007224600)]
    public class Migration_202210072246 : Migration
    {
        public override void Up()
        {
            Alter.Column("Title").OnTable("Attachments").AsString(200);
            Alter.Column("Description").OnTable("Messages").AsString(300);
        }

        public override void Down()
        {
            Alter.Column("Title").OnTable("Attachments").AsString(30);
            Alter.Column("Description").OnTable("Messages").AsString(100);
        }
    }
}

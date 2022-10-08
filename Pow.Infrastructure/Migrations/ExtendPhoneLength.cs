using FluentMigrator;

namespace Pow.Infrastructure.Migrations
{
    [Migration(20221007111200)]
    public class ExtendPhoneLength : Migration
    {
        public override void Up()
        {
            Alter.Column("Phone").OnTable("Messages").AsString(100);
            



        }

        public override void Down()
        {
            Alter.Column("Phone").OnTable("Messages").AsString(13);
            
        }
    }
}

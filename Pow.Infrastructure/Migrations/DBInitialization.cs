using FluentMigrator;

namespace Pow.Infrastructure.Migrations
{
    [Migration(20220911200000)]
    public class DbInitialization : Migration
    {
        public override void Up()
        {
            Create.Table("Messages")
                .WithColumn("Id")
                .AsString(450)
                .WithDefaultValue(SystemMethods.NewGuid)
                .NotNullable()
                .PrimaryKey()
                .WithColumn("Description")
                .AsString(100)
                .WithColumn("CreatedDate")
                .AsDate()
                .WithColumn("EventDate")
                .AsDate()
                .WithColumn("Phone")
                .AsString(13)
                .WithColumn("Email")
                .AsString(100)
                .WithColumn("UserId")
                .AsString(450)
                .Nullable();

            Create.Table("Marks")
                .WithColumn("Id")
                .AsString(450)
                .WithDefaultValue(SystemMethods.NewGuid)
                .NotNullable()
                .PrimaryKey()
                .WithColumn("Desabled")
                .AsBoolean()
                .WithColumn("MessageId")
                .AsString(450)
                .WithColumn("Country")
                .AsString(100)
                .WithColumn("City")
                .AsString(100)
                .WithColumn("Region")
                .AsString(100)
                .WithColumn("Address")
                .AsString(100)
                .WithColumn("StreetNumber")
                .AsString(10)
                .WithColumn("PostalCode")
                .AsString(100)
                .WithColumn("County")
                .AsString(100)
                .WithColumn("MapUrl")
                .AsString(100)
                .WithColumn("GpsLongitude")
                .AsString(100)
                .WithColumn("GpsLatitude")
                .AsString(100);

            Create.Table("Attachments")
                .WithColumn("Id")
                .AsString(450)
                .WithDefaultValue(SystemMethods.NewGuid)
                .NotNullable()
                .PrimaryKey()
                .WithColumn("Title")
                .AsString(30)
                .WithColumn("File")
                .AsBinary()
                .WithColumn("MessageId")
                .AsString(450);

            Create.ForeignKey("fk_Marks_MessagesId_MessagesId")
                .FromTable("Marks")
                .ForeignColumn("MessageId")
                .ToTable("Messages")
                .PrimaryColumn("Id");

            Create.ForeignKey("fk_Attachments_MessagesId_MessagesId")
                .FromTable("Attachments")
                .ForeignColumn("MessageId")
                .ToTable("Messages")
                .PrimaryColumn("Id");

            Create.ForeignKey("fk_Users_MessagesId_MessagesId")
                .FromTable("Messages")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Messages");
            Delete.Table("Marks");
            Delete.Table("Attachments");
        }
    }
}

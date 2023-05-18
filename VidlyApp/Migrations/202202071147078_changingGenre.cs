namespace VidlyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingGenre : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Movies", name: "GenreId_Id", newName: "Genre_Id");
            RenameIndex(table: "dbo.Movies", name: "IX_GenreId_Id", newName: "IX_Genre_Id");
            AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "GenreId");
            RenameIndex(table: "dbo.Movies", name: "IX_Genre_Id", newName: "IX_GenreId_Id");
            RenameColumn(table: "dbo.Movies", name: "Genre_Id", newName: "GenreId_Id");
        }
    }
}

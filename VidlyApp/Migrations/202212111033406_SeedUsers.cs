namespace VidlyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'725951e7-f7f5-4418-b39a-e5ca8a4349c3', N'guest@vidly.com', 0, N'AMxb+ajFGWu9w32z1Av6XneFSd5cWABf/EHLn+s/6WTqr/p1jFpNv6Ak9P6cYs+HAw==', N'fd909ebc-f80a-465b-8f53-c4c7cabf74ca', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9214c7ac-2593-425d-80fc-6c3eaa3a75b3', N'admin@vidly.com', 0, N'AGG5eJXk6LHwLQEsF96leSeQHJcqg8NyjQpKjHAojI3AQ0SptJqX9KBWhN0CEt0Y+g==', N'62dc44cf-032c-49ab-945e-d21b8d6f5eaf', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'54af6af3-6d70-4017-9d59-b6466313299f', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9214c7ac-2593-425d-80fc-6c3eaa3a75b3', N'54af6af3-6d70-4017-9d59-b6466313299f')
                
");
        }
        
        public override void Down()
        {
        }
    }
}

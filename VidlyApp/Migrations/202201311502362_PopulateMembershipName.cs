namespace VidlyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipName : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipName = 'Pay as You Go' WHERE Id=1");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Monthly' WHERE Id=2");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Quaterly' WHERE Id=3");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Annually' WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}

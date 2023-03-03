using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitirme_Projesi.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "UserLists",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            IsDone = table.Column<bool>(type: "bit", nullable: true),
        //            UserId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_UserLists", x => x.Id);

            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserLists_UserId",
            //        table: "UserLists",
            //        column: "UserId");
            //}
}
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLists");
        }
    
    }
}

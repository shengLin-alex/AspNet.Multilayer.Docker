using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNet.Multilayer.Docker.Repository.Migrations.SqlServerDb
{
    public partial class InitialCreatePostgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "multilayerdemo");

            migrationBuilder.CreateTable(
                name: "ORDER",
                schema: "multilayerdemo",
                columns: table => new
                {
                    ORDER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_USERID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ORDER_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER",
                schema: "multilayerdemo");
        }
    }
}

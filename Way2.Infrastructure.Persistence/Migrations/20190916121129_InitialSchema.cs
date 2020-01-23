using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Way2.Infrastructure.Persistence.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    city_id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    modified_at = table.Column<DateTimeOffset>(nullable: true),
                    version = table.Column<long>(nullable: false),
                    name = table.Column<string>(type: "TEXT COLLATE LATIN1_GENERAL_CS_AI", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.city_id);
                });

            migrationBuilder.CreateTable(
                name: "city_weather",
                columns: table => new
                {
                    city_weather_id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    modified_at = table.Column<DateTimeOffset>(nullable: true),
                    version = table.Column<long>(nullable: false),
                    city_id = table.Column<Guid>(nullable: false),
                    date = table.Column<string>(nullable: false),
                    temperature = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_weather", x => x.city_weather_id);
                    table.ForeignKey(
                        name: "FK_city_weather_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "city_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_name",
                table: "city",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_city_weather_city_id",
                table: "city_weather",
                column: "city_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_weather");

            migrationBuilder.DropTable(
                name: "city");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApis.Migrations
{
    /// <inheritdoc />
    public partial class gri_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GDate",
                table: "PersonelDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDate",
                table: "PersonelDatas");
        }
    }
}

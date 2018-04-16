using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MarsStreetView.Migrations
{
    public partial class EarthDateFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EarthDate",
                table: "Favorite",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CameraFullName",
                table: "Favorite",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraFullName",
                table: "Favorite");

            migrationBuilder.AlterColumn<string>(
                name: "EarthDate",
                table: "Favorite",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}

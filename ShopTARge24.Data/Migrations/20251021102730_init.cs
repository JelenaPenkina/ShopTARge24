using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge24.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "KindergartenName",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RealEstates");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Kindergarten",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "Kindergarten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Kindergarten",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Kindergarten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KindergartenName",
                table: "Kindergarten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Kindergarten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Kindergarten",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kindergarten",
                table: "Kindergarten",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kindergarten",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "KindergartenName",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Kindergarten");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Kindergarten");

            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KindergartenName",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RealEstates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

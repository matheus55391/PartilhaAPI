using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartilhaAPI.Migrations
{
    /// <inheritdoc />
    public partial class adduserfirendcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FriendCode",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendCode",
                table: "Users");
        }
    }
}

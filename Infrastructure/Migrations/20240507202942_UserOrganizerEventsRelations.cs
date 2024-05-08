using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserOrganizerEventsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizerEventIds");

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_UserId",
                table: "Organizers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizers_Users_UserId",
                table: "Organizers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizers_Users_UserId",
                table: "Organizers");

            migrationBuilder.DropIndex(
                name: "IX_Organizers_UserId",
                table: "Organizers");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "OrganizerEventIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizerEventIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizerEventIds_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizerEventIds_OrganizerId",
                table: "OrganizerEventIds",
                column: "OrganizerId");
        }
    }
}

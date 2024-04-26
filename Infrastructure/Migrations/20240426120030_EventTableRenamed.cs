using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventTableRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubEvents_EventIds_EventId",
                table: "SubEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventIds",
                table: "EventIds");

            migrationBuilder.RenameTable(
                name: "EventIds",
                newName: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubEvents_Events_EventId",
                table: "SubEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubEvents_Events_EventId",
                table: "SubEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "EventIds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventIds",
                table: "EventIds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubEvents_EventIds_EventId",
                table: "SubEvents",
                column: "EventId",
                principalTable: "EventIds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

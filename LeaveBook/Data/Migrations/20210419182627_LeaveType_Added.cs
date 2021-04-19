using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveBook.Data.Migrations
{
    public partial class LeaveType_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAudits_LeaveTypes_LeaveTypeId",
                table: "LeaveAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes");

            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                newName: "LeaveType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveType",
                table: "LeaveType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAudits_LeaveType_LeaveTypeId",
                table: "LeaveAudits",
                column: "LeaveTypeId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveType_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAudits_LeaveType_LeaveTypeId",
                table: "LeaveAudits");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveType_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveType",
                table: "LeaveType");

            migrationBuilder.RenameTable(
                name: "LeaveType",
                newName: "LeaveTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAudits_LeaveTypes_LeaveTypeId",
                table: "LeaveAudits",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

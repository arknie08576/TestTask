using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequest",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employes_Employee",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Employee",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaveRequest",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequest",
                table: "ApprovalRequests",
                column: "LeaveRequest",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employes_Employee",
                table: "LeaveRequests",
                column: "Employee",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequest",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employes_Employee",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Employee",
                table: "LeaveRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveRequest",
                table: "ApprovalRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequest",
                table: "ApprovalRequests",
                column: "LeaveRequest",
                principalTable: "LeaveRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employes_Employee",
                table: "LeaveRequests",
                column: "Employee",
                principalTable: "Employes",
                principalColumn: "Id");
        }
    }
}

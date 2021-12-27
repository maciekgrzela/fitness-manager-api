using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessManager.DataAccess.Migrations
{
    public partial class AddressNullableGuids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FitnessClubId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_InstructorId",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FitnessClubId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts",
                column: "DepartmentId",
                unique: true,
                filter: "[DepartmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FitnessClubId",
                table: "Contacts",
                column: "FitnessClubId",
                unique: true,
                filter: "[FitnessClubId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_InstructorId",
                table: "Contacts",
                column: "InstructorId",
                unique: true,
                filter: "[InstructorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FitnessClubId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_InstructorId",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FitnessClubId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DepartmentId",
                table: "Contacts",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FitnessClubId",
                table: "Contacts",
                column: "FitnessClubId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_InstructorId",
                table: "Contacts",
                column: "InstructorId",
                unique: true);
        }
    }
}

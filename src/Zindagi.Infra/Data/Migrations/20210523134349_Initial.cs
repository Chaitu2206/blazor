using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zindagi.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "zg");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "zg",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    auth_id = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    mobile_number = table.Column<string>(type: "TEXT", nullable: false),
                    alternate_mobile_number = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    is_email_verified = table.Column<int>(type: "INTEGER", nullable: false),
                    is_active = table.Column<int>(type: "INTEGER", nullable: false),
                    is_donor = table.Column<int>(type: "INTEGER", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    blood_group = table.Column<int>(type: "INTEGER", nullable: false),
                    picture_url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                    table.UniqueConstraint("ak_users_auth_id", x => x.auth_id);
                });

            migrationBuilder.CreateTable(
                name: "blood_requests",
                schema: "zg",
                columns: table => new
                {
                    blood_request_id = table.Column<long>(type: "INTEGER", nullable: false),
                    quantity_in_units = table.Column<double>(type: "REAL", nullable: false),
                    patient_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    reason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    donation_type = table.Column<int>(type: "INTEGER", nullable: false),
                    blood_group = table.Column<int>(type: "INTEGER", nullable: false),
                    priority = table.Column<int>(type: "INTEGER", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    requestor_id = table.Column<long>(type: "INTEGER", nullable: false),
                    assignee_id = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blood_requests", x => x.blood_request_id);
                    table.ForeignKey(
                        name: "fk_blood_requests_users_assignee_id",
                        column: x => x.assignee_id,
                        principalSchema: "zg",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_blood_requests_users_user_id",
                        column: x => x.requestor_id,
                        principalSchema: "zg",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_blood_requests_assignee_id",
                schema: "zg",
                table: "blood_requests",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "ix_blood_requests_requestor_id",
                schema: "zg",
                table: "blood_requests",
                column: "requestor_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "zg",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blood_requests",
                schema: "zg");

            migrationBuilder.DropTable(
                name: "users",
                schema: "zg");
        }
    }
}

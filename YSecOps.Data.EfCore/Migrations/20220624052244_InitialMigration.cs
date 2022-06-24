using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YSecOps.Data.EfCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannedList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsHotel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pronouns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pronouns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    IsCurrentlyOccupied = table.Column<bool>(type: "bit", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Keys = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    ProvidedKeys = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((4))"),
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    IsOnBreak = table.Column<bool>(type: "bit", nullable: false),
                    NeedsCrashSpace = table.Column<bool>(type: "bit", nullable: false),
                    IsBlackShirt = table.Column<bool>(type: "bit", nullable: false),
                    IsRaveApproved = table.Column<bool>(type: "bit", nullable: false),
                    BreakStartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BreakEndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShirtSize = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Staff_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Pronoun_Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((14))"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FacebookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreferredName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Pronouns_Id",
                        column: x => x.Pronoun_Id,
                        principalTable: "Pronouns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Staff_Id",
                        column: x => x.Staff_Id,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RadioSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    LastStaffToHave_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RadioNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CheckedOutAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckedInAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCharging = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadioSchedule_Location",
                        column: x => x.Location_Id,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RadioSchedule_Staff",
                        column: x => x.LastStaffToHave_Id,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckedInAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckedOutAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastReportedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_CurrentLocation",
                        column: x => x.CurrentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shifts_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shifts_StartingLocation",
                        column: x => x.StartingLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffTypesRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffTypeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTypesRoles", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_StaffTypesRoles_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffTypesRoles_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffTypesRoles_StaffTypes",
                        column: x => x.StaffTypeId,
                        principalTable: "StaffTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    RecordedBy_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpsManager_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shift_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    RecordedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsFollowUpRequired = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpResponse = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Injuries = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Locations_LocationId",
                        column: x => x.Location_Id,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Shifts_ShiftId",
                        column: x => x.Shift_Id,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Staff_OpsManagerId",
                        column: x => x.OpsManager_Id,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Staff_RecordedBy",
                        column: x => x.RecordedBy_Id,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NonStaffPeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    PronounId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonStaffPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonStaff_Incidents_Id",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NonStaff_Pronouns_Id",
                        column: x => x.PronounId,
                        principalTable: "Pronouns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_LastName_FirstName",
                table: "Contacts",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Pronoun_Id",
                table: "Contacts",
                column: "Pronoun_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_StaffId",
                table: "Contacts",
                column: "Staff_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_Location_Id",
                table: "Incidents",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_OpsManagerId",
                table: "Incidents",
                column: "OpsManager_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_RecordedBy",
                table: "Incidents",
                column: "RecordedBy_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_Severity",
                table: "Incidents",
                columns: new[] { "RecordedOn", "Severity" });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_Shift_Id",
                table: "Incidents",
                column: "Shift_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NonStaffPeople_IncidentId",
                table: "NonStaffPeople",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_NonStaffPeople_LastName_FirstName",
                table: "NonStaffPeople",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_NonStaffPeople_PronounId",
                table: "NonStaffPeople",
                column: "PronounId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioSchedule_LastStaffId",
                table: "RadioSchedule",
                column: "LastStaffToHave_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RadioSchedule_Location",
                table: "RadioSchedule",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RadioSchedule_RadioNumber",
                table: "RadioSchedule",
                column: "RadioNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_Floor_Number",
                table: "RoomSchedule",
                columns: new[] { "Floor", "Number" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedule_Location",
                table: "RoomSchedule",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CheckedInAt_CheckedOutAt",
                table: "Shifts",
                columns: new[] { "CheckedInAt", "CheckedOutAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CurrentLocationId",
                table: "Shifts",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_StaffId",
                table: "Shifts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_StartAt_EndAt",
                table: "Shifts",
                columns: new[] { "StartAt", "EndAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_StartingLocationId",
                table: "Shifts",
                column: "StartingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_IncidentId",
                table: "Staff",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoomId",
                table: "Staff",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypesRoles_RoleId",
                table: "StaffTypesRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypesRoles_StaffId",
                table: "StaffTypesRoles",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTypesRoles_StaffTypeId",
                table: "StaffTypesRoles",
                column: "StaffTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedList");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "NonStaffPeople");

            migrationBuilder.DropTable(
                name: "RadioSchedule");

            migrationBuilder.DropTable(
                name: "RoomSchedule");

            migrationBuilder.DropTable(
                name: "StaffTypesRoles");

            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Pronouns");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "StaffTypes");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}

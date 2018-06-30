using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coreSQLite.Migrations
{
    public partial class PrimaVersione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datis",
                columns: table => new
                {
                    DatiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    categoria = table.Column<string>(type: "TEXT", nullable: true),
                    datati = table.Column<string>(type: "TEXT", nullable: true),
                    descrizione = table.Column<string>(type: "TEXT", nullable: true),
                    importo = table.Column<string>(type: "TEXT", nullable: true),
                    nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datis", x => x.DatiId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    UserLogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    utentelog = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.UserLogId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    utente = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datis");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HadilHadjAlouaneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    AnalyseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DureeResultat = table.Column<int>(type: "int", nullable: false),
                    PrixAnalyse = table.Column<double>(type: "float", nullable: false),
                    TypeAnalyse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValeurAnalyse = table.Column<float>(type: "real", nullable: false),
                    ValeurMaxNormale = table.Column<float>(type: "real", nullable: false),
                    ValeurMinNormale = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.AnalyseId);
                });

            migrationBuilder.CreateTable(
                name: "Laboratoires",
                columns: table => new
                {
                    LaboratoireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseLabo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratoires", x => x.LaboratoireId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    CodePatient = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    EmailPatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Informations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.CodePatient);
                });

            migrationBuilder.CreateTable(
                name: "Infirmiers",
                columns: table => new
                {
                    InfirmierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialite = table.Column<int>(type: "int", nullable: false),
                    LaboratoireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infirmiers", x => x.InfirmierId);
                    table.ForeignKey(
                        name: "FK_Infirmiers_Laboratoires_LaboratoireId",
                        column: x => x.LaboratoireId,
                        principalTable: "Laboratoires",
                        principalColumn: "LaboratoireId");
                });

            migrationBuilder.CreateTable(
                name: "Bilans",
                columns: table => new
                {
                    DatePrelevement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InfirmierId = table.Column<int>(type: "int", nullable: false),
                    CodePatient = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    EmailMedecin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paye = table.Column<bool>(type: "bit", nullable: false),
                    AnalyseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilans", x => new { x.InfirmierId, x.CodePatient, x.DatePrelevement });
                    table.ForeignKey(
                        name: "FK_Bilans_Analyses_AnalyseId",
                        column: x => x.AnalyseId,
                        principalTable: "Analyses",
                        principalColumn: "AnalyseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilans_Infirmiers_InfirmierId",
                        column: x => x.InfirmierId,
                        principalTable: "Infirmiers",
                        principalColumn: "InfirmierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilans_Patients_CodePatient",
                        column: x => x.CodePatient,
                        principalTable: "Patients",
                        principalColumn: "CodePatient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilans_AnalyseId",
                table: "Bilans",
                column: "AnalyseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilans_CodePatient",
                table: "Bilans",
                column: "CodePatient");

            migrationBuilder.CreateIndex(
                name: "IX_Infirmiers_LaboratoireId",
                table: "Infirmiers",
                column: "LaboratoireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilans");

            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Infirmiers");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Laboratoires");
        }
    }
}

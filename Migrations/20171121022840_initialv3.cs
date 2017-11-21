using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TccFrotaApp.Migrations
{
    public partial class initialv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtFim",
                table: "Apontamentos");

            migrationBuilder.RenameColumn(
                name: "DtInicio",
                table: "Apontamentos",
                newName: "DtAtualizacao");

            migrationBuilder.AddColumn<int>(
                name: "AditionalInformation",
                table: "Apontamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApontamentoInicialId",
                table: "Apontamentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apontamentos_ApontamentoInicialId",
                table: "Apontamentos",
                column: "ApontamentoInicialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apontamentos_Apontamentos_ApontamentoInicialId",
                table: "Apontamentos",
                column: "ApontamentoInicialId",
                principalTable: "Apontamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apontamentos_Apontamentos_ApontamentoInicialId",
                table: "Apontamentos");

            migrationBuilder.DropIndex(
                name: "IX_Apontamentos_ApontamentoInicialId",
                table: "Apontamentos");

            migrationBuilder.DropColumn(
                name: "AditionalInformation",
                table: "Apontamentos");

            migrationBuilder.DropColumn(
                name: "ApontamentoInicialId",
                table: "Apontamentos");

            migrationBuilder.RenameColumn(
                name: "DtAtualizacao",
                table: "Apontamentos",
                newName: "DtInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtFim",
                table: "Apontamentos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

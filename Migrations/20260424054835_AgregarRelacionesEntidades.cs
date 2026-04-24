using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportReserva.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionesEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdCancha_FechaReserva_IdHorario",
                table: "Reserva",
                columns: new[] { "IdCancha", "FechaReserva", "IdHorario" },
                unique: true,
                filter: "[EstadoReserva] <> 'Cancelada'");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdCliente",
                table: "Reserva",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdHorario",
                table: "Reserva",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdReserva",
                table: "Pago",
                column: "IdReserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_DNI",
                table: "Cliente",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdUsuario",
                table: "Cliente",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Usuario_IdUsuario",
                table: "Cliente",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Reserva_IdReserva",
                table: "Pago",
                column: "IdReserva",
                principalTable: "Reserva",
                principalColumn: "IdReserva",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Cancha_IdCancha",
                table: "Reserva",
                column: "IdCancha",
                principalTable: "Cancha",
                principalColumn: "IdCancha",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Cliente_IdCliente",
                table: "Reserva",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Horario_IdHorario",
                table: "Reserva",
                column: "IdHorario",
                principalTable: "Horario",
                principalColumn: "IdHorario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Usuario_IdUsuario",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Reserva_IdReserva",
                table: "Pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Cancha_IdCancha",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Cliente_IdCliente",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Horario_IdHorario",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_IdCancha_FechaReserva_IdHorario",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_IdCliente",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_IdHorario",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Pago_IdReserva",
                table: "Pago");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_DNI",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_IdUsuario",
                table: "Cliente");
        }
    }
}

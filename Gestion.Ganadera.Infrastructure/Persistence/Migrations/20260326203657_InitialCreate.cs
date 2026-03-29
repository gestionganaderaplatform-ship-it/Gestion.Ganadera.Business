using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "Auditoria",
                schema: "Seguridad",
                columns: table => new
                {
                    Auditoria_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auditoria_Api_Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Auditoria_Nombre_Tabla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditoria_Valor_Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditoria_Valores_Viejos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditoria_Nuevos_Valores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditoria_Modificado_Por = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditoria_Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.Auditoria_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Metrica_Solicitud",
                schema: "Seguridad",
                columns: table => new
                {
                    Metrica_Solicitud_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metrica_Solicitud_Api_Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Metrica_Solicitud_Ruta_Request = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Metrica_Solicitud_Metodo_Http = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Metrica_Solicitud_Codigo_Estado = table.Column<int>(type: "int", nullable: false),
                    Metrica_Solicitud_Tiempo_Respuesta_Ms = table.Column<long>(type: "bigint", nullable: false),
                    Metrica_Solicitud_Correlation_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Metrica_Solicitud_Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrica_Solicitud", x => x.Metrica_Solicitud_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Seguridad_Evento",
                schema: "Seguridad",
                columns: table => new
                {
                    Evento_Seguridad_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evento_Seguridad_Api_Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Evento_Seguridad_Tipo_Evento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_Seguridad_Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_Seguridad_Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_Seguridad_Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento_Seguridad_UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento_Seguridad_CorrelationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento_Seguridad_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguridad_Evento", x => x.Evento_Seguridad_Codigo);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Metrica_Solicitud",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Seguridad_Evento",
                schema: "Seguridad");
        }
    }
}

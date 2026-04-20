using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGanaderiaCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ganaderia");

            migrationBuilder.CreateTable(
                name: "Categoria_Animal",
                schema: "Ganaderia",
                columns: table => new
                {
                    Categoria_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria_Animal_Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Categoria_Animal_Sexo_Esperado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Categoria_Animal_Orden = table.Column<int>(type: "int", nullable: false),
                    Categoria_Animal_Activa = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Animal", x => x.Categoria_Animal_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Finca",
                schema: "Ganaderia",
                columns: table => new
                {
                    Finca_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finca_Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Finca_Activa = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finca", x => x.Finca_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Rango_Edad",
                schema: "Ganaderia",
                columns: table => new
                {
                    Rango_Edad_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rango_Edad_Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Rango_Edad_Edad_Minima_Meses = table.Column<int>(type: "int", nullable: true),
                    Rango_Edad_Edad_Maxima_Meses = table.Column<int>(type: "int", nullable: true),
                    Rango_Edad_Orden = table.Column<int>(type: "int", nullable: false),
                    Rango_Edad_Activo = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rango_Edad", x => x.Rango_Edad_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Identificador",
                schema: "Ganaderia",
                columns: table => new
                {
                    Tipo_Identificador_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Identificador_Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Tipo_Identificador_Codigo_Interno = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Tipo_Identificador_Operativo = table.Column<bool>(type: "bit", nullable: false),
                    Tipo_Identificador_Permite_Busqueda = table.Column<bool>(type: "bit", nullable: false),
                    Tipo_Identificador_Permite_Principal = table.Column<bool>(type: "bit", nullable: false),
                    Tipo_Identificador_Activo = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Identificador", x => x.Tipo_Identificador_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Ganadero",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finca_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Ganadero_Tipo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Evento_Ganadero_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Ganadero_Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Evento_Ganadero_Registrado_Por = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Evento_Ganadero_Estado = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Evento_Ganadero_Origen_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Evento_Ganadero_Es_Correccion = table.Column<bool>(type: "bit", nullable: false),
                    Evento_Ganadero_Es_Anulacion = table.Column<bool>(type: "bit", nullable: false),
                    Evento_Ganadero_Observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Ganadero", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Ganadero_Evento_Ganadero_Evento_Ganadero_Origen_Codigo",
                        column: x => x.Evento_Ganadero_Origen_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Ganadero_Finca_Finca_Codigo",
                        column: x => x.Finca_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Finca",
                        principalColumn: "Finca_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Potrero",
                schema: "Ganaderia",
                columns: table => new
                {
                    Potrero_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finca_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Potrero_Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Potrero_Activo = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potrero", x => x.Potrero_Codigo);
                    table.ForeignKey(
                        name: "FK_Potrero_Finca_Finca_Codigo",
                        column: x => x.Finca_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Finca",
                        principalColumn: "Finca_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                schema: "Ganaderia",
                columns: table => new
                {
                    Animal_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finca_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Potrero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Categoria_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Animal_Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Animal_Activo = table.Column<bool>(type: "bit", nullable: false),
                    Animal_Origen_Ingreso = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Animal_Fecha_Ingreso_Inicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Animal_Fecha_Registro_Ingreso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Animal_Fecha_Ultimo_Evento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Animal_Codigo);
                    table.ForeignKey(
                        name: "FK_Animal_Categoria_Animal_Categoria_Animal_Codigo",
                        column: x => x.Categoria_Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Animal_Finca_Finca_Codigo",
                        column: x => x.Finca_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Finca",
                        principalColumn: "Finca_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Animal_Potrero_Potrero_Codigo",
                        column: x => x.Potrero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Potrero",
                        principalColumn: "Potrero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Registro_Existente",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Tipo_Identificador_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Registro_Existente_Identificador_Valor = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Categoria_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Rango_Edad_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Potrero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Registro_Existente_Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Evento_Detalle_Registro_Existente_Fecha_Informada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Registro_Existente", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Registro_Existente_Categoria_Animal_Categoria_Animal_Codigo",
                        column: x => x.Categoria_Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Registro_Existente_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Registro_Existente_Potrero_Potrero_Codigo",
                        column: x => x.Potrero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Potrero",
                        principalColumn: "Potrero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Registro_Existente_Rango_Edad_Rango_Edad_Codigo",
                        column: x => x.Rango_Edad_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Rango_Edad",
                        principalColumn: "Rango_Edad_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Registro_Existente_Tipo_Identificador_Tipo_Identificador_Codigo",
                        column: x => x.Tipo_Identificador_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Tipo_Identificador",
                        principalColumn: "Tipo_Identificador_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Ganadero_Animal",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Ganadero_Animal_Orden = table.Column<int>(type: "int", nullable: true),
                    Evento_Ganadero_Animal_Estado_Afectacion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Evento_Ganadero_Animal_Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Ganadero_Animal", x => x.Evento_Ganadero_Animal_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Ganadero_Animal_Animal_Animal_Codigo",
                        column: x => x.Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Animal",
                        principalColumn: "Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Ganadero_Animal_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identificador_Animal",
                schema: "Ganaderia",
                columns: table => new
                {
                    Identificador_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Tipo_Identificador_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Identificador_Animal_Valor = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Identificador_Animal_Es_Principal = table.Column<bool>(type: "bit", nullable: false),
                    Identificador_Animal_Activo = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identificador_Animal", x => x.Identificador_Animal_Codigo);
                    table.ForeignKey(
                        name: "FK_Identificador_Animal_Animal_Animal_Codigo",
                        column: x => x.Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Animal",
                        principalColumn: "Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Identificador_Animal_Tipo_Identificador_Tipo_Identificador_Codigo",
                        column: x => x.Tipo_Identificador_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Tipo_Identificador",
                        principalColumn: "Tipo_Identificador_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Categoria_Animal_Codigo",
                schema: "Ganaderia",
                table: "Animal",
                column: "Categoria_Animal_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Cliente_Codigo_Animal_Activo",
                schema: "Ganaderia",
                table: "Animal",
                columns: new[] { "Cliente_Codigo", "Animal_Activo" });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Finca_Codigo",
                schema: "Ganaderia",
                table: "Animal",
                column: "Finca_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Potrero_Codigo",
                schema: "Ganaderia",
                table: "Animal",
                column: "Potrero_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Animal_Cliente_Codigo_Categoria_Animal_Nombre",
                schema: "Ganaderia",
                table: "Categoria_Animal",
                columns: new[] { "Cliente_Codigo", "Categoria_Animal_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Registro_Existente_Categoria_Animal_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente",
                column: "Categoria_Animal_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Registro_Existente_Potrero_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente",
                column: "Potrero_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Registro_Existente_Rango_Edad_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente",
                column: "Rango_Edad_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Registro_Existente_Tipo_Identificador_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente",
                column: "Tipo_Identificador_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Ganadero_Cliente_Codigo_Finca_Codigo_Evento_Ganadero_Tipo_Evento_Ganadero_Fecha",
                schema: "Ganaderia",
                table: "Evento_Ganadero",
                columns: new[] { "Cliente_Codigo", "Finca_Codigo", "Evento_Ganadero_Tipo", "Evento_Ganadero_Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Ganadero_Evento_Ganadero_Origen_Codigo",
                schema: "Ganaderia",
                table: "Evento_Ganadero",
                column: "Evento_Ganadero_Origen_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Ganadero_Finca_Codigo",
                schema: "Ganaderia",
                table: "Evento_Ganadero",
                column: "Finca_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Ganadero_Animal_Animal_Codigo",
                schema: "Ganaderia",
                table: "Evento_Ganadero_Animal",
                column: "Animal_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Ganadero_Animal_Evento_Ganadero_Codigo_Animal_Codigo",
                schema: "Ganaderia",
                table: "Evento_Ganadero_Animal",
                columns: new[] { "Evento_Ganadero_Codigo", "Animal_Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Finca_Cliente_Codigo_Finca_Nombre",
                schema: "Ganaderia",
                table: "Finca",
                columns: new[] { "Cliente_Codigo", "Finca_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Identificador_Animal_Animal_Codigo_Tipo_Identificador_Codigo",
                schema: "Ganaderia",
                table: "Identificador_Animal",
                columns: new[] { "Animal_Codigo", "Tipo_Identificador_Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identificador_Animal_Tipo_Identificador_Codigo_Identificador_Animal_Valor",
                schema: "Ganaderia",
                table: "Identificador_Animal",
                columns: new[] { "Tipo_Identificador_Codigo", "Identificador_Animal_Valor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Potrero_Finca_Codigo_Potrero_Nombre",
                schema: "Ganaderia",
                table: "Potrero",
                columns: new[] { "Finca_Codigo", "Potrero_Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rango_Edad_Cliente_Codigo_Rango_Edad_Nombre",
                schema: "Ganaderia",
                table: "Rango_Edad",
                columns: new[] { "Cliente_Codigo", "Rango_Edad_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Identificador_Cliente_Codigo_Tipo_Identificador_Nombre",
                schema: "Ganaderia",
                table: "Tipo_Identificador",
                columns: new[] { "Cliente_Codigo", "Tipo_Identificador_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Registro_Existente",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Evento_Ganadero_Animal",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Identificador_Animal",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Rango_Edad",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Evento_Ganadero",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Animal",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Tipo_Identificador",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Categoria_Animal",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Potrero",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Finca",
                schema: "Ganaderia");
        }
    }
}

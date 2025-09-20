using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Garante a extensão necessária para gen_random_uuid()
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS pgcrypto;");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pecas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Codigo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pecas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Especialidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "servicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    PrecoBase = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Modelo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_motos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ordens_servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    MotoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Total = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordens_servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ordens_servico_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_motos_MotoId",
                        column: x => x.MotoId,
                        principalTable: "motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itens_ordem_servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false), // "SERVICO" | "PECA"
                    ServicoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PecaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantidade = table.Column<decimal>(type: "numeric(12,3)", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_ordem_servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itens_ordem_servico_ordens_servico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalTable: "ordens_servico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itens_ordem_servico_pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_itens_ordem_servico_servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Índices
            migrationBuilder.CreateIndex(
                name: "IX_clientes_Email",
                table: "clientes",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_motos_ClienteId",
                table: "motos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_motos_Placa",
                table: "motos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_ClienteId",
                table: "ordens_servico",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_MotoId",
                table: "ordens_servico",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_ProfissionalId",
                table: "ordens_servico",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_DataAbertura_Status",
                table: "ordens_servico",
                columns: new[] { "DataAbertura", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_itens_ordem_servico_OrdemServicoId",
                table: "itens_ordem_servico",
                column: "OrdemServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_itens_ordem_servico_PecaId",
                table: "itens_ordem_servico",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_itens_ordem_servico_ServicoId",
                table: "itens_ordem_servico",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_pecas_Codigo",
                table: "pecas",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "itens_ordem_servico");
            migrationBuilder.DropTable(name: "ordens_servico");
            migrationBuilder.DropTable(name: "pecas");
            migrationBuilder.DropTable(name: "servicos");
            migrationBuilder.DropTable(name: "motos");
            migrationBuilder.DropTable(name: "profissionais");
            migrationBuilder.DropTable(name: "clientes");
        }
    }
}

//dotnet ef migrations add InitialCreate -p Modules/Cadastro/Oficina.Cadastro.Infrastructure -s ApiGateway/Oficina.Api
//dotnet ef database update -p Modules/Cadastro/Oficina.Cadastro.Infrastructure -s ApiGateway/Oficina.Api
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    [DbContext(typeof(OficinaDbContext))]
    partial class OficinaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity("Cliente", b =>
            {
                b.ToTable("clientes");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<DateTime?>("CreatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Email")
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)");

                b.Property<string>("Telefone")
                    .HasMaxLength(30)
                    .HasColumnType("character varying(30)");

                b.HasKey("Id");

                b.HasIndex("Email");

                //b.Metadata.SetClrType(typeof(Cliente));
            });

            modelBuilder.Entity("Profissional", b =>
            {
                b.ToTable("profissionais");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Especialidade")
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)");

                b.HasKey("Id");

                //b.Metadata.SetClrType(typeof(Profissional));
            });

            modelBuilder.Entity("Peca", b =>
            {
                b.ToTable("pecas");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Codigo")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("character varying(60)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<decimal>("Preco")
                    .HasColumnType("numeric(12,2)");

                b.HasKey("Id");

                b.HasIndex("Codigo")
                    .IsUnique();

                //b.Metadata.SetClrType(typeof(Peca));
            });

            modelBuilder.Entity("Servico", b =>
            {
                b.ToTable("servicos");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnType("character varying(120)");

                b.Property<decimal>("PrecoBase")
                    .HasColumnType("numeric(12,2)");

                b.HasKey("Id");

                //b.Metadata.SetClrType(typeof(Servico));
            });

            modelBuilder.Entity("Moto", b =>
            {
                b.ToTable("motos");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<int?>("Ano")
                    .HasColumnType("integer");

                b.Property<Guid>("ClienteId")
                    .HasColumnType("uuid");

                b.Property<string>("Modelo")
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnType("character varying(120)");

                b.Property<string>("Placa")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)");

                b.HasKey("Id");

                b.HasIndex("ClienteId");

                b.HasIndex("Placa")
                    .IsUnique();

                b.HasOne("Oficina.Cadastro.Domain.Cliente", "Cliente")
                    .WithMany("Motos")
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("Cliente");

                //b.Metadata.SetClrType(typeof(Moto));
            });

            modelBuilder.Entity("OrdemServico", b =>
            {
                b.ToTable("ordens_servico");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<Guid>("ClienteId")
                    .HasColumnType("uuid");

                b.Property<DateTime>("DataAbertura")
                    .HasColumnType("timestamp without time zone");

                b.Property<DateTime?>("DataFechamento")
                    .HasColumnType("timestamp without time zone");

                b.Property<Guid>("MotoId")
                    .HasColumnType("uuid");

                b.Property<Guid>("ProfissionalId")
                    .HasColumnType("uuid");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("character varying(40)");

                b.Property<decimal>("Total")
                    .HasColumnType("numeric(12,2)");

                b.HasKey("Id");

                b.HasIndex("ClienteId");

                b.HasIndex("MotoId");

                b.HasIndex("ProfissionalId");

                b.HasIndex("DataAbertura", "Status");

                b.HasOne("Oficina.Cadastro.Domain.Cliente", null)
                    .WithMany()
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Oficina.Cadastro.Domain.Moto", null)
                    .WithMany()
                    .HasForeignKey("MotoId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Oficina.Cadastro.Domain.Profissional", null)
                    .WithMany()
                    .HasForeignKey("ProfissionalId")
                    .OnDelete(DeleteBehavior.Cascade);

                //b.Metadata.SetClrType(typeof(OrdemServico));
            });

            modelBuilder.Entity("ItemOrdemServico", b =>
            {
                b.ToTable("itens_ordem_servico");

                b.Property<Guid>("Id")
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<Guid>("OrdemServicoId")
                    .HasColumnType("uuid");

                b.Property<Guid?>("PecaId")
                    .HasColumnType("uuid");

                b.Property<decimal>("Preco")
                    .HasColumnType("numeric(12,2)");

                b.Property<Guid?>("ServicoId")
                    .HasColumnType("uuid");

                b.Property<string>("Tipo")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("character varying(20)");

                b.Property<decimal>("Quantidade")
                    .HasColumnType("numeric(12,3)");

                b.HasKey("Id");

                b.HasIndex("OrdemServicoId");

                b.HasIndex("PecaId");

                b.HasIndex("ServicoId");

                b.HasOne("Oficina.Cadastro.Domain.OrdemServico", "OrdemServico")
                    .WithMany("Itens")
                    .HasForeignKey("OrdemServicoId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Oficina.Cadastro.Domain.Peca", "Peca")
                    .WithMany()
                    .HasForeignKey("PecaId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("Oficina.Cadastro.Domain.Servico", "Servico")
                    .WithMany()
                    .HasForeignKey("ServicoId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("OrdemServico");
                b.Navigation("Peca");
                b.Navigation("Servico");

                //b.Metadata.SetClrType(typeof(ItemOrdemServico));
            });

            // Navegações de coleções em entidades
            modelBuilder.Entity("Oficina.Cadastro.Domain.Cliente", b =>
            {
                b.Navigation("Motos");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.OrdemServico", b =>
            {
                b.Navigation("Itens");
            });

#pragma warning restore 612, 618
        }
    }
}

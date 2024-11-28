﻿// <auto-generated />
using System;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eficaz_WebApi.Migrations
{
    [DbContext(typeof(EficazDbContext))]
    [Migration("20241127225449_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Core.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeRua")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroResidencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Excluido")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Core.Models.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Excluido")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("Core.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Excluido")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<double>("PrecoDe")
                        .HasColumnType("double");

                    b.Property<double>("PrecoPor")
                        .HasColumnType("double");

                    b.Property<bool>("ProdutoDestaque")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("MarcaId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Models.Address", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithMany("Enderecos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Product", b =>
                {
                    b.HasOne("Core.Models.Category", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Marca", "Marca")
                        .WithMany("Produtos")
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("Core.Models.Category", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("Core.Models.Marca", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
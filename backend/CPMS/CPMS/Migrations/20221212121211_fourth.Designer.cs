﻿// <auto-generated />
using System;
using CPMS.DBConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CPMS.Migrations
{
    [DbContext(typeof(CPMDbContext))]
    [Migration("20221212121211_fourth")]
    partial class fourth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CPMS.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AgreementPaperName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(30);

                    b.Property<string>("Organization")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(10);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(10);

                    b.Property<string>("ProfileImageName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(100);

                    b.Property<string>("Role")
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CPMS.Models.Client_Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Client_Projects");
                });

            modelBuilder.Entity("CPMS.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Designation")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(15);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(11);

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CPMS.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Budget");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FRequirement")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("NFRequirement")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Technology")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CPMS.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(30);

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CPMS.Models.Client_Project", b =>
                {
                    b.HasOne("CPMS.Models.Client", "Client")
                        .WithMany("Client_Projects")
                        .HasForeignKey("ClientId");

                    b.HasOne("CPMS.Models.Project", "Project")
                        .WithMany("Client_Projects")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("CPMS.Models.Employee", b =>
                {
                    b.HasOne("CPMS.Models.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("CPMS.Models.Team", b =>
                {
                    b.HasOne("CPMS.Models.Project", "Project")
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}

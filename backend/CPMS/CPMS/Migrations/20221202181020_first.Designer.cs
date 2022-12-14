// <auto-generated />
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
    [Migration("20221202181020_first")]
    partial class first
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
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.Property<string>("Organization")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(10);

                    b.Property<string>("Phone")
                        .HasMaxLength(10);

                    b.Property<string>("ProfileImageName")
                        .HasMaxLength(100);

                    b.Property<string>("Role")
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
                        .HasMaxLength(30);

                    b.Property<string>("Email")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(15);

                    b.Property<string>("Phone")
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
                        .IsRequired();

                    b.Property<string>("NFRequirement")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Technology")
                        .IsRequired()
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

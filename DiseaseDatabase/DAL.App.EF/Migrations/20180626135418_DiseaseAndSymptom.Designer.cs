﻿// <auto-generated />
using DAL.App.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.App.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180626135418_DiseaseAndSymptom")]
    partial class DiseaseAndSymptom
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Disease", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.HasKey("DiseaseId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("Domain.DiseaseSymptom", b =>
                {
                    b.Property<int>("DiseaseSymptomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DiseaseId");

                    b.Property<int>("SymptomId");

                    b.HasKey("DiseaseSymptomId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("SymptomId");

                    b.ToTable("DiseaseSymptoms");
                });

            modelBuilder.Entity("Domain.Symptom", b =>
                {
                    b.Property<int>("SymptomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SymptomName")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.HasKey("SymptomId");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("Domain.DiseaseSymptom", b =>
                {
                    b.HasOne("Domain.Disease", "Disease")
                        .WithMany("Symptoms")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Symptom", "Symptom")
                        .WithMany("Diseases")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
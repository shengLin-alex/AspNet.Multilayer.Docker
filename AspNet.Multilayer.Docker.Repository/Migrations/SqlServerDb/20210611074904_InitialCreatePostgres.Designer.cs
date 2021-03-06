// <auto-generated />
using AspNet.Multilayer.Docker.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNet.Multilayer.Docker.Repository.Migrations.SqlServerDb
{
    [DbContext(typeof(SqlServerDbContext))]
    [Migration("20210611074904_InitialCreatePostgres")]
    partial class InitialCreatePostgres
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("multilayerdemo")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNet.Multilayer.Docker.Repository.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ORDER_ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("ORDER_USERID");

                    b.HasKey("Id");

                    b.ToTable("ORDER");
                });
#pragma warning restore 612, 618
        }
    }
}

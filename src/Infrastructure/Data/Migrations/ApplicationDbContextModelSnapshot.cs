﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(6);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(7);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(8);

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(9);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(5);

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(10);

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(11);

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Core.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("BusinessRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(5);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(6);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(7);

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(8);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnOrder(3);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(9);

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6c809ef3-4d53-47b8-9014-e3d45c7a4707"),
                            BusinessRegistrationNumber = "R-000000",
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "TEA"
                        },
                        new
                        {
                            Id = new Guid("36a735d5-2dd9-4c95-9870-08db2b0faddb"),
                            BusinessRegistrationNumber = "R-111111",
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            IsActive = false,
                            IsDeleted = false,
                            Name = "Test Company1"
                        },
                        new
                        {
                            Id = new Guid("09d94c77-db51-4ecc-9871-08db2b0faddb"),
                            BusinessRegistrationNumber = "R-222222",
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            IsActive = false,
                            IsDeleted = false,
                            Name = "Test Company2"
                        });
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(9);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(10);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(11);

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(12);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnOrder(7);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(8);

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(13);

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(14);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<int>("TotalScore")
                        .HasColumnType("int")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f7ca5d8a-c88e-4dbb-8015-fc974994a6d9"),
                            CompanyId = new Guid("6c809ef3-4d53-47b8-9014-e3d45c7a4707"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            Email = "bruno@techexpertacademy.com",
                            FirstName = "Bruno",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "Danelon",
                            PhoneNumber = "333333333",
                            TotalScore = 0
                        },
                        new
                        {
                            Id = new Guid("5f753883-cc79-4fb4-9d16-08db2b1280be"),
                            CompanyId = new Guid("36a735d5-2dd9-4c95-9870-08db2b0faddb"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            Email = "testuser1@test.com",
                            FirstName = "Test",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "User1",
                            PhoneNumber = "111111111",
                            TotalScore = 0
                        },
                        new
                        {
                            Id = new Guid("50ec5ac0-2ba7-4288-9d17-08db2b1280be"),
                            CompanyId = new Guid("09d94c77-db51-4ecc-9871-08db2b0faddb"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatedBy = "Seeder",
                            Email = "testuser2@test.com",
                            FirstName = "Test",
                            IsActive = false,
                            IsDeleted = false,
                            LastName = "User2",
                            PhoneNumber = "222222222",
                            TotalScore = 0
                        });
                });

            modelBuilder.Entity("Core.Entities.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Core.Entities.Guess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("AnswerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("GuessedEmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Guesses");
                });

            modelBuilder.Entity("Core.Entities.LeadershipBoard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("LeadershipBoards");
                });

            modelBuilder.Entity("Core.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(5);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(6);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(7);

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(8);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnOrder(3);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(4);

                    b.Property<DateTimeOffset?>("LastModifiedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(9);

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(10);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Core.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c490685a-9a1f-42a1-8238-4a2e6b4ea8b9"),
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = new Guid("ae4c6594-a871-4563-b30d-12e27f208bca"),
                            Name = "CompanyAdmin",
                            NormalizedName = "COMPANYADMIN"
                        },
                        new
                        {
                            Id = new Guid("63b0a573-bf6e-46a4-96d9-bb1d0cce89d1"),
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = new Guid("8173c4ba-3e72-4fc7-9c53-ffd1dd801174"),
                            Name = "QuestionHost",
                            NormalizedName = "QUESTIONHOST"
                        });
                });

            modelBuilder.Entity("Core.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4531aaa1-97bd-43a7-82e2-2e040469154f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "add11d33-4651-4495-ba96-8772e64c4ac0",
                            Email = "bruno@techexpertacademy.com",
                            EmailConfirmed = false,
                            EmployeeId = new Guid("f7ca5d8a-c88e-4dbb-8015-fc974994a6d9"),
                            LockoutEnabled = false,
                            NormalizedEmail = "BRUNO@TECHEXPERTACADEMY.COM",
                            NormalizedUserName = "BRUNO DANELON",
                            PasswordHash = "AQAAAAIAAYagAAAAEMgpzUuKp0sQRyrZJhBubeE/Q3qiAoJzsfUNTjPwNd0Fbw6YPymyrseT7jnrsqmr2g==",
                            PhoneNumber = "333333333",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "O2QAJA2JY53X3MU476YW24YL3FRZFSXN",
                            TwoFactorEnabled = false,
                            UserName = "Bruno Danelon"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Core.Identity.ApplicationUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.HasDiscriminator().HasValue("ApplicationUserRole");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("4531aaa1-97bd-43a7-82e2-2e040469154f"),
                            RoleId = new Guid("c490685a-9a1f-42a1-8238-4a2e6b4ea8b9")
                        });
                });

            modelBuilder.Entity("Core.Entities.Answer", b =>
                {
                    b.HasOne("Core.Entities.Employee", "AnsweredBy")
                        .WithMany("Answers")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnsweredBy");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.HasOne("Core.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Core.Entities.Feedback", b =>
                {
                    b.HasOne("Core.Entities.Employee", "SentBy")
                        .WithMany("Feedbacks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SentBy");
                });

            modelBuilder.Entity("Core.Entities.Guess", b =>
                {
                    b.HasOne("Core.Entities.Answer", "Answer")
                        .WithMany("Guesses")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Employee", "Guesser")
                        .WithMany("Guesses")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Guesser");
                });

            modelBuilder.Entity("Core.Entities.LeadershipBoard", b =>
                {
                    b.HasOne("Core.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Core.Entities.Question", b =>
                {
                    b.HasOne("Core.Entities.Employee", "PostedBy")
                        .WithMany("Questions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PostedBy");
                });

            modelBuilder.Entity("Core.Identity.ApplicationUser", b =>
                {
                    b.HasOne("Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Core.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Core.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Core.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Core.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Core.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.Answer", b =>
                {
                    b.Navigation("Guesses");
                });

            modelBuilder.Entity("Core.Entities.Company", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Core.Entities.Employee", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Feedbacks");

                    b.Navigation("Guesses");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Core.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
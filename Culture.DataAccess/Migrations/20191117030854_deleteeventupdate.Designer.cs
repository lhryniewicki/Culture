﻿// <auto-generated />
using System;
using Culture.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Culture.DataAccess.Migrations
{
    [DbContext(typeof(CultureDbContext))]
    [Migration("20191117030854_deleteeventupdate")]
    partial class deleteeventupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Culture.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarPath");

                    b.Property<int>("CalendarId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecretAnswer");

                    b.Property<string>("SecretQuestion");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<Guid>("UserConfigurationId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Culture.Models.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("BelongsToId");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToId")
                        .IsUnique();

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("Culture.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("EventId");

                    b.Property<string>("ImagePath");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("EventId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Culture.Models.Emoticon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NameId");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Emoticons");
                });

            modelBuilder.Entity("Culture.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("CityName");

                    b.Property<string>("Content");

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("ImagePath");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(18,15)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(18,15)");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.Property<string>("StreetName");

                    b.Property<DateTime>("TakesPlaceDate");

                    b.Property<string>("UrlSlug");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Culture.Models.EventInCalendar", b =>
                {
                    b.Property<int>("CalendarId");

                    b.Property<int>("EventId");

                    b.HasKey("CalendarId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventsInCalendar");
                });

            modelBuilder.Entity("Culture.Models.EventReaction", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<Guid>("UserId");

                    b.Property<int>("Type");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("EventReactions");
                });

            modelBuilder.Entity("Culture.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<int>("EventId");

                    b.Property<bool>("IsRead");

                    b.Property<string>("RedirectUrl");

                    b.Property<DateTime>("SentData");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Culture.Models.UserConfiguration", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<bool>("Anonymous");

                    b.Property<bool>("CalendarPastEvents");

                    b.Property<int>("CommentsDisplayAmount");

                    b.Property<int>("EventsDisplayAmount");

                    b.Property<int>("LogOutAfter");

                    b.Property<bool>("SendEmailNotification");

                    b.HasKey("UserId");

                    b.ToTable("UserConfigurations");
                });

            modelBuilder.Entity("Culture.Models.UserInEvent", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<Guid>("UserId");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersInEvent");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Culture.Models.Calendar", b =>
                {
                    b.HasOne("Culture.Models.AppUser", "BelongsTo")
                        .WithOne("Calendar")
                        .HasForeignKey("Culture.Models.Calendar", "BelongsToId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.Comment", b =>
                {
                    b.HasOne("Culture.Models.AppUser", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Culture.Models.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.Emoticon", b =>
                {
                    b.HasOne("Culture.Models.Emoticon", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");
                });

            modelBuilder.Entity("Culture.Models.Event", b =>
                {
                    b.HasOne("Culture.Models.AppUser", "CreatedBy")
                        .WithMany("HostedEvents")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Culture.Models.EventInCalendar", b =>
                {
                    b.HasOne("Culture.Models.Calendar", "Calendar")
                        .WithMany("Events")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Culture.Models.Event", "Event")
                        .WithMany("EventsInCalendar")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.EventReaction", b =>
                {
                    b.HasOne("Culture.Models.Event", "Event")
                        .WithMany("Reactions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Culture.Models.AppUser", "User")
                        .WithMany("EventReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.Notification", b =>
                {
                    b.HasOne("Culture.Models.Event", "Event")
                        .WithMany("Notifications")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Culture.Models.AppUser", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.UserConfiguration", b =>
                {
                    b.HasOne("Culture.Models.AppUser", "User")
                        .WithOne("UserConfiguration")
                        .HasForeignKey("Culture.Models.UserConfiguration", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Culture.Models.UserInEvent", b =>
                {
                    b.HasOne("Culture.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Culture.Models.AppUser", "User")
                        .WithMany("ParticipatedEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Culture.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Culture.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Culture.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Culture.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

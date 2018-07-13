﻿// <auto-generated />
using login_registration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace login_registration.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20180625051017_addFutureDateValidation")]
    partial class addFutureDateValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("login_registration.Models._Activity", b =>
                {
                    b.Property<int>("_ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(105);

                    b.Property<int>("Duration");

                    b.Property<string>("DurationFormat")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(105);

                    b.Property<int>("UserId");

                    b.HasKey("_ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("activitys");
                });

            modelBuilder.Entity("login_registration.Models.Particpant", b =>
                {
                    b.Property<int>("ParticpantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("_ActivityId");

                    b.HasKey("ParticpantId");

                    b.HasIndex("UserId");

                    b.HasIndex("_ActivityId");

                    b.ToTable("participants");
                });

            modelBuilder.Entity("login_registration.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(105);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(105);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(105);

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("login_registration.Models._Activity", b =>
                {
                    b.HasOne("login_registration.Models.User", "creator")
                        .WithMany("activitysCreated")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("login_registration.Models.Particpant", b =>
                {
                    b.HasOne("login_registration.Models.User", "user")
                        .WithMany("activitysParticipatingIn")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("login_registration.Models._Activity", "ActivityAttending")
                        .WithMany("participants")
                        .HasForeignKey("_ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

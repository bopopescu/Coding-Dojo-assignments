﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RESTauranter;
using System;

namespace RESTauranter.Migrations
{
    [DbContext(typeof(ReviewContext))]
    [Migration("20180611182749_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("RESTauranter.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Rating");

                    b.Property<string>("ResturantName");

                    b.Property<string>("ReviewerName");

                    b.Property<string>("_Review");

                    b.Property<DateTime>("visit");

                    b.HasKey("Id");

                    b.ToTable("reviews");
                });
#pragma warning restore 612, 618
        }
    }
}

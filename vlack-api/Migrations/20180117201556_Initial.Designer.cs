﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using VlackApi.Models;

namespace vlackapi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180117201556_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("VlackApi.Models.Channel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Channel");
                });

            modelBuilder.Entity("VlackApi.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ChannelId");

                    b.Property<long>("UserId");

                    b.Property<string>("body");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("VlackApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("VlackApi.Models.Message", b =>
                {
                    b.HasOne("VlackApi.Models.Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VlackApi.Models.User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingList.Data;

#nullable disable

namespace ShoppingList.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240530103524_AddProductMacro")]
    partial class AddProductMacro
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("ShoppingList.Models.ListContent", b =>
                {
                    b.Property<int>("ListContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ListContentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ListContent");
                });

            modelBuilder.Entity("ShoppingList.Models.Macro", b =>
                {
                    b.Property<int>("MacroID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MacroName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MacroID");

                    b.HasIndex("ProductId");

                    b.ToTable("Macro");
                });

            modelBuilder.Entity("ShoppingList.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameProduct")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShoppingList.Models.ProductMacro", b =>
                {
                    b.Property<int>("ProductMacroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MacroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductMacroId");

                    b.HasIndex("MacroId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductMacro");
                });

            modelBuilder.Entity("ShoppingList.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ShoppingList.Models.UserShoppingList", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShoppingListName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ShoppingListId");

                    b.HasIndex("UserId");

                    b.ToTable("UserShoppingList");
                });

            modelBuilder.Entity("ShoppingList.Models.ListContent", b =>
                {
                    b.HasOne("ShoppingList.Models.Product", "Product")
                        .WithMany("ListContents")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingList.Models.UserShoppingList", "UserShoppingList")
                        .WithMany("ListContents")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UserShoppingList");
                });

            modelBuilder.Entity("ShoppingList.Models.Macro", b =>
                {
                    b.HasOne("ShoppingList.Models.Product", null)
                        .WithMany("Macro")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("ShoppingList.Models.ProductMacro", b =>
                {
                    b.HasOne("ShoppingList.Models.Macro", "Macro")
                        .WithMany("ProductMacros")
                        .HasForeignKey("MacroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingList.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Macro");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingList.Models.UserShoppingList", b =>
                {
                    b.HasOne("ShoppingList.Models.User", "User")
                        .WithMany("ShoppingLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingList.Models.Macro", b =>
                {
                    b.Navigation("ProductMacros");
                });

            modelBuilder.Entity("ShoppingList.Models.Product", b =>
                {
                    b.Navigation("ListContents");

                    b.Navigation("Macro");
                });

            modelBuilder.Entity("ShoppingList.Models.User", b =>
                {
                    b.Navigation("ShoppingLists");
                });

            modelBuilder.Entity("ShoppingList.Models.UserShoppingList", b =>
                {
                    b.Navigation("ListContents");
                });
#pragma warning restore 612, 618
        }
    }
}

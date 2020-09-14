﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Infrastructure;

namespace Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingContext))]
    partial class OrderingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.orderitemseq", "'orderitemseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:ordering.buyerseq", "'buyerseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:ordering.orderseq", "'orderseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:ordering.paymentseq", "'paymentseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "buyerseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("buyers","ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("cardTypes","ordering");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Amex"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Visa"
                        },
                        new
                        {
                            Id = 3,
                            Name = "MasterCard"
                        });
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "paymentseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("_alias")
                        .IsRequired()
                        .HasColumnName("Alias")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("_cardHolderName")
                        .IsRequired()
                        .HasColumnName("CardHolderName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("_cardNumber")
                        .IsRequired()
                        .HasColumnName("CardNumber")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int>("_cardTypeId")
                        .HasColumnName("CardTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("_expiration")
                        .HasColumnName("Expiration")
                        .HasColumnType("datetime2")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("_cardTypeId");

                    b.ToTable("paymentMethods","ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int?>("_buyerId")
                        .HasColumnName("BuyerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("_orderDate")
                        .HasColumnName("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("_orderStatusId")
                        .HasColumnName("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("_paymentMethodId")
                        .HasColumnName("PaymentMethodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("_buyerId");

                    b.HasIndex("_orderStatusId");

                    b.HasIndex("_paymentMethodId");

                    b.ToTable("orders","ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderitemseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("_discount")
                        .HasColumnName("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("_pictureUrl")
                        .IsRequired()
                        .HasColumnName("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_productName")
                        .IsRequired()
                        .HasColumnName("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("_unitPrice")
                        .HasColumnName("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("_units")
                        .HasColumnName("Units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems","ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("orderStatus","ordering");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Submitted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "AwaitingValidation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "StockConfirmed"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Paid"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Shipped"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("_cardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("_buyerId");

                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("_orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("_paymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("Ordering.Domain.AggregatesModel.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Infrastructure.EntityConfigurations
{
    //Aggregate'ler persistance'ta nasıl tutulacak, bunun yönergelerini EF'e veriyoruz.
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            //Tablo ismini belirttim.
            orderConfiguration.ToTable("orders", OrderingContext.DEFAULT_SCHEMA);
            //Primary key.
            orderConfiguration.HasKey(o => o.Id);

            //orderConfiguration.Ignore(o => o.DomainEvents);
            orderConfiguration.Property(o => o.Id).UseHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);

            orderConfiguration
                .OwnsOne(o => o.Address, a => {
                    a.WithOwner();                           
                });
            
            orderConfiguration
                .Property<int?>("_buyerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BuyerId")
                .IsRequired(false);

            orderConfiguration
                .Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            
            orderConfiguration
                .Property<int>("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            orderConfiguration
                .Property<int?>("_paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId")
                .IsRequired(false);

            
            /* BEGIN: SET TABLE RELATIONS */
            var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Order.OrderItems));

            orderConfiguration.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("_paymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            orderConfiguration.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                // .HasForeignKey("BuyerId");
                .HasForeignKey("_buyerId");

            orderConfiguration.HasOne(o => o.OrderStatus)
                .WithMany()
                // .HasForeignKey("OrderStatusId");
                .HasForeignKey("_orderStatusId");
            /* END: SET TABLE RELATIONS */


        }
    }
}
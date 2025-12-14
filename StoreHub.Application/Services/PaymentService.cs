using Domain.Contracts;
using Domain.Models;
using StoreHub.Application.Dtos.CustomBasket;
using StoreHub.Application.Services.Contracts;
using StoreHub.Core.Models.Orders;
using Stripe;
using OrderProduct = Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace StoreHub.Application.Services
{
    public class PaymentService(IMapper mapper, ICustomBasketRepository basketRepository, IUnitOfWork unit, IConfiguration config) : IpaymentService
    {
        public async Task<CustomBasketDto> CreatePaymentIntentAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new Exception("Basket not found");
            foreach (var item in basket.Items)
            {
                var product = await unit.GetGenericRepo<OrderProduct, int>().GetById(item.Id);
                if (product is null) throw new Exception($"Product with id {item.Id} not found");
                item.Price = product.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("Delivery method not selected");
            var shippingPrice = await unit.GetGenericRepo<DeliveryMethod, int>().GetById(basket.DeliveryMethodId.Value);

            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + shippingPrice.Cost) * 100;

            StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("stripeSecretKey");
            var createPaymentIntent = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {

                //create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                var payment = await createPaymentIntent.CreateAsync(options);

                basket.PaymentIntentId = payment.Id;
                basket.ClientSecret = payment.ClientSecret;
            }
            else
            {

                //update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount,

                };
                await createPaymentIntent.UpdateAsync(basket.PaymentIntentId, options);


            }

            await basketRepository.UpdateBasketAsync(basket);
            var result = mapper.Map<CustomBasketDto>(basket);
            return result;
        }
    }
}

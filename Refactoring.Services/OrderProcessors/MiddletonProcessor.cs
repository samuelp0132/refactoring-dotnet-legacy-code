using System;
using System.Threading.Tasks;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class MiddletonProcessor : OrderProcessor
    {
        private readonly IAdvertPrinter _advertPrinter;

        public MiddletonProcessor(IAdvertPrinter advertPrinter)
        {
            _advertPrinter = advertPrinter;
        }
        public override async Task<Order> PrintAdvertAndUpdateOrderAsync(Order order)
        {
            var advert = new Advert
            {
                CreatedOn = DateTime.Now,
                Heading = "County Diner",
                Content = "Kids eat free every Thursday night"
            };
            order.Advert = advert;
            _advertPrinter.PrintAdvert(advert, false);
            order.Status = "Complete";
            return order;
        }
    }
}
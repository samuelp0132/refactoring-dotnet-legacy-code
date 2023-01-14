using System;
using System.Threading.Tasks;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class DowntownProcessor : OrderProcessor
    {
        private readonly IAdvertPrinter _advertPrinter;

        public DowntownProcessor(IAdvertPrinter advertPrinter)
        {
            _advertPrinter = advertPrinter;
        }

        public override async Task<Order> PrintAdvertAndUpdateOrderAsync(Order order)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) {
                _advertPrinter.PrintAdvert(null, true);
            }
            var advert = new Advert
            {
                CreatedOn = DateTime.Now,
                Heading = "Downtown Coffee Roasters",
                Content = "Get a free coffee drink when you buy 1lb of coffee beans"
            };
            order.Advert = advert;
            _advertPrinter.PrintAdvert(advert, false);
            order.Status = "Complete";
            return order;
        }

    }
}
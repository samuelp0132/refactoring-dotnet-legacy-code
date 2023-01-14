using System;
using System.Threading.Tasks;
using Rectoring.Services;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class CambridgeProcessor : OrderProcessor
    {
        private readonly IChamberOfCommerceApi _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDateTimeResolver _dateTimeResolver;

        public CambridgeProcessor(IChamberOfCommerceApi chamberOfCommerceApi,
            IAdvertPrinter advertPrinter,
            IDateTimeResolver dateTimeResolver)
        {
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
            _dateTimeResolver = dateTimeResolver;
        }

        public override async Task<Order> PrintAdvertAndUpdateOrderAsync(Order order)
        {
            var advert = new Advert
            {
                CreatedOn = DateTime.Now,
                Heading = "Cambridge Bakery",
                Content = "Custom Birthday and Wedding Cakes"
            };
            if (_dateTimeResolver.IsItTuesday()) {
                var result = await _chamberOfCommerceApi.GetImageAndThumbnailDataFor(District.Cambridge);
                advert.ImageUrl = result.ThumbnailUrl;
            }
            order.Advert = advert;
            _advertPrinter.PrintAdvert(advert, false);
            order.Status = "Complete";
            return order;
        }

    }
}
using System;
using System.Threading.Tasks;
using Rectoring.Services;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services.OrderProcessors
{
    public class CountyProcessor : OrderProcessor
    {
        private readonly IDealService _dealService;
        private readonly IChamberOfCommerceApi _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;

        public CountyProcessor(IDealService dealService,
            IChamberOfCommerceApi chamberOfCommerceApi,
            IAdvertPrinter advertPrinter)
        {
            _dealService = dealService;
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
        }
        public override async Task<Order> PrintAdvertAndUpdateOrderAsync(Order order)
        {
            var deal = _dealService.GenerateDeal(DateTime.Now);
            var biz = _dealService.GetRandomLocalBusiness();
            var result = await _chamberOfCommerceApi.GetImageAndThumbnailDataFor(District.Country);
            
            var advert = new Advert
            {
                CreatedOn = DateTime.Now,
                Heading = "Middleton " + biz,
                ImageUrl = result.ThumbnailUrl,
                Content =  "Get " + deal * 100 + "Percent off your next purchase!"
            };
            
            order.Advert = advert;
            _advertPrinter.PrintAdvert(advert, false);
            order.Status = "Complete";
            return order;
        }
    }
}
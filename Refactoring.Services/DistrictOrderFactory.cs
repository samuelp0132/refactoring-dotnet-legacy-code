using System;
using Rectoring.Services;
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;
using Refactoring.Web.Services.OrderProcessors;

namespace Refactoring.Web.Services
{
    public interface IDistrictOrderFactory
    {
        OrderProcessor For(string district);
    }

    public class DistrictOrderFactory : IDistrictOrderFactory
    {
        private readonly IChamberOfCommerceApi _chamberOfCommerceApi;
        private readonly IAdvertPrinter _advertPrinter;
        private readonly IDealService _dealService;
        private readonly IDateTimeResolver _dateTimeResolver;

        public DistrictOrderFactory(IChamberOfCommerceApi chamberOfCommerceApi,
            IAdvertPrinter advertPrinter,
            IDealService dealService,
            IDateTimeResolver dateTimeResolver)
        {
            _chamberOfCommerceApi = chamberOfCommerceApi;
            _advertPrinter = advertPrinter;
            _dealService = dealService;
            _dateTimeResolver = dateTimeResolver;
        }

        public OrderProcessor For(string district)
        {
            if (district.ToLower() == District.Cambridge)
            {
                return new CambridgeProcessor(_chamberOfCommerceApi, _advertPrinter, _dateTimeResolver);
            }
            else if (district.ToLower() == District.Middleton)
            {
                return new MiddletonProcessor(_advertPrinter);
            }
            else if (district.ToLower() == District.Country)
            {
                return new CountyProcessor(_dealService, _chamberOfCommerceApi, _advertPrinter);
            }
            else if (district.ToLower() == District.Downtown)
            {
                return new DowntownProcessor(_advertPrinter);
            }

            throw new NotImplementedException($"No processor for {district}");
        }
    }
}
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;

namespace Rectoring.Services {
    
    public class DealService : IDealService
    {
        private const decimal PmRate = 0.1M;
        private const decimal AmRate = 0.05M;
        public decimal GenerateDeal(DateTime dateTime) => IsAfterNoon(dateTime) ? PmRate : AmRate;

        public string GetRandomLocalBusiness()
        {
            var localBusiness = LocalBusinessHelper.AllBusinesses;
            var random = new Random();
            var idx = random.Next(localBusiness.Count);
            return localBusiness.ToList()[idx];
        }
        private static bool IsAfterNoon(DateTime dateTime) => dateTime.Hour > 12 && dateTime.Hour < 24;
    }
}
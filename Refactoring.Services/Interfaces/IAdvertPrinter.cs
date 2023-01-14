using Refactoring.Web.DomainModels;

namespace Refactoring.Web.Services.Interfaces
{
    public interface IAdvertPrinter
    {
        void PrintAdvert(Advert advert, bool IsDefaultAdvert);
    }
}
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services.Helpers;

public class DateTimeResolverHelper : IDateTimeResolver
{
    public bool IsItTuesday()
    {
        return DateTime.Now.DayOfWeek == DayOfWeek.Tuesday;
    }
}
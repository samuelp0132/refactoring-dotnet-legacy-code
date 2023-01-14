using System.Collections.Generic;

namespace Refactoring.Web.Services.Helpers
{
    public static class District
    {
            public static string Cambridge => "Cambridge";
            public static string Downtown => "Downtown";
            public static string Country => "Country";
            public static string Middleton => "Middleton";
            
            private static int CambridgeId => 42;
            private static int DowntownId => 11;
            private static int CountryId => 23;
            private static int MiddletonId => 18;

            public static IEnumerable<string> All => new List<string>
            {
                Cambridge, Downtown, Country, Middleton
            };

            public static string GetDistrictNumberByName(string name)
            {
                var districtLookup = new Dictionary<string, int>() {
                    {Downtown,  DowntownId},
                    {Country,   CountryId},
                    {Middleton, MiddletonId},
                    {Cambridge, CambridgeId}
                };

                return districtLookup[name.ToLower()].ToString();
            }
    }
}
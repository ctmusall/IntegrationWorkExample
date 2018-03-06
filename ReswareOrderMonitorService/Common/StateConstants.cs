using System.Collections.Generic;

namespace ReswareOrderMonitorService.Common
{
    internal static class StateConstants
    {
        internal const string Alabama = "AL";
        internal const string Arizona = "AZ";
        internal const string California = "CA";
        internal const string Colorado = "CO";
        internal const string Florida = "FL";
        internal const string Idaho = "ID";
        internal const string Illinois = "IL";
        internal const string Indiana = "IN";
        internal const string Kansas = "KS";
        internal const string Kentucky = "KY";
        internal const string Louisiana = "LA";
        internal const string Maryland = "MD";
        internal const string Michigan = "MI";
        internal const string Minnesota = "MN";
        internal const string Mississippi = "MS";
        internal const string Missouri = "MO";
        internal const string Montana = "MT";
        internal const string NewJersey = "NJ";
        internal const string NewMexico = "NM";
        internal const string Ohio = "OH";
        internal const string Oklahoma = "OK";
        internal const string Oregon = "OR";
        internal const string RhodeIsland = "RI";
        internal const string Tennesee = "TN";
        internal const string Texas = "TX";
        internal const string Virginia = "VA";
        internal const string Washington = "WA";
        internal const string Wisconsin = "WI";
        internal const string Delaware = "DE";
        internal const string Georgia = "GA";
        internal const string Massachusetts = "MA";
        internal const string NorthCarolina = "NC";
        internal const string SouthCarolina = "SC";
        internal const string NewYork = "NY";
        internal const string Vermont = "VT";
        internal const string WestVirginia = "WV";
        internal const string Connecticut = "CN";
        internal const string DistrictOfColumbia = "DC";
        internal const string Maine = "ME";
        internal const string Pennsylvania = "PA";
        internal const string NewHampshire = "NH";
        internal const string Alaska = "AK";
        internal const string Arkansas = "AR";
        internal const string Hawaii = "HI";
        internal const string Iowa = "IA";
        internal const string Nebraska = "NE";
        internal const string Nevada = "NV";
        internal const string NorthDakota = "ND";
        internal const string SouthDakota = "SD";
        internal const string Utah = "UT";
        internal const string Wyoming = "WY";

        internal static ICollection<string> OneHourReviewStates = new List<string>
        {
            Alabama,
            Arizona,
            California,
            Colorado,
            Connecticut,
            Delaware,
            Florida,
            Idaho,
            Illinois,
            Indiana,
            Kansas,
            Kentucky,
            Louisiana,
            Maryland,
            Michigan,
            Minnesota,
            Mississippi,
            Missouri,
            Montana,
            NewJersey,
            NewMexico,
            Ohio,
            Oklahoma,
            Oregon,
            RhodeIsland,
            Tennesee,
            Texas,
            Virginia,
            Washington,
            Wisconsin
        };

        internal static ICollection<string> InternalPreparationStates = new List<string>
        {
            DistrictOfColumbia,
            Georgia,
            Maine,
            Massachusetts,
            NewYork,
            Pennsylvania,
            NewHampshire,
            NorthCarolina,
            SouthCarolina,
            Vermont,
            WestVirginia
        };

        internal static ICollection<string> ExternalPreparationStates = new List<string>
        {
            Alaska,
            Arkansas,
            Hawaii,
            Iowa,
            Nebraska,
            Nevada,
            NorthDakota,
            SouthDakota,
            Utah,
            Wyoming
        };
    }
}

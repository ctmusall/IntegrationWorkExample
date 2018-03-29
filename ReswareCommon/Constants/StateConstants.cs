using System.Collections.Generic;

namespace ReswareCommon.Constants
{
    public static class StateConstants
    {
        public const string Alabama = "AL";
        public const string Arizona = "AZ";
        public const string California = "CA";
        public const string Colorado = "CO";
        public const string Florida = "FL";
        public const string Idaho = "ID";
        public const string Illinois = "IL";
        public const string Indiana = "IN";
        public const string Kansas = "KS";
        public const string Kentucky = "KY";
        public const string Louisiana = "LA";
        public const string Maryland = "MD";
        public const string Michigan = "MI";
        public const string Minnesota = "MN";
        public const string Mississippi = "MS";
        public const string Missouri = "MO";
        public const string Montana = "MT";
        public const string NewJersey = "NJ";
        public const string NewMexico = "NM";
        public const string Ohio = "OH";
        public const string Oklahoma = "OK";
        public const string Oregon = "OR";
        public const string RhodeIsland = "RI";
        public const string Tennesee = "TN";
        public const string Texas = "TX";
        public const string Virginia = "VA";
        public const string Washington = "WA";
        public const string Wisconsin = "WI";
        public const string Delaware = "DE";
        public const string Georgia = "GA";
        public const string Massachusetts = "MA";
        public const string NorthCarolina = "NC";
        public const string SouthCarolina = "SC";
        public const string NewYork = "NY";
        public const string Vermont = "VT";
        public const string WestVirginia = "WV";
        public const string Connecticut = "CN";
        public const string DistrictOfColumbia = "DC";
        public const string Maine = "ME";
        public const string Pennsylvania = "PA";
        public const string NewHampshire = "NH";
        public const string Alaska = "AK";
        public const string Arkansas = "AR";
        public const string Hawaii = "HI";
        public const string Iowa = "IA";
        public const string Nebraska = "NE";
        public const string Nevada = "NV";
        public const string NorthDakota = "ND";
        public const string SouthDakota = "SD";
        public const string Utah = "UT";
        public const string Wyoming = "WY";

        public static ICollection<string> OneHourReviewStates = new List<string>
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

        public static ICollection<string> InternalPreparationStates = new List<string>
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

        public static ICollection<string> ExternalPreparationStates = new List<string>
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

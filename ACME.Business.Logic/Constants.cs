
using System.Collections.Generic;

namespace ACME.Business.Logic
{
    public class Constants
    {
        public static class ErrorMessages
        {
            public const string FILE_NOT_FOUND_TRY_AGAIN = "File not found try again.";
        }

        public static Dictionary<string, int> DAY_ABBRE = new()
        {
            { "MO",0 },
             { "TU",1 },
              { "WE",2 },
               { "TH",3 },
                { "FR",4 },
                  { "SA",5 },
                    { "SU",6 },


        };
    }
}

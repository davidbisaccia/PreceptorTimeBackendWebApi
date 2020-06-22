using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.Converters
{
    public static class DateConverter
    {
        public static string Convert(DateTime dt)
        {
            return dt.ToUniversalTime().ToString("s");
        }
    }
}

using AKQA.Application.Infrastructure;
using Humanizer;
using System.Globalization;

namespace AKQA.Application.Extensions
{
    //
    // Summary:
    //     3501.ToWords() -> "three thousand five hundred and one"
    //
    // Parameters:
    //   number:
    //     Number to be turned to words
    //
    //   culture:
    //     Culture to use. If null, current thread's UI culture is used.
    public static class NumberToWordsExtension
    {
        public static string ToWords(this decimal number, CultureInfo culture = null)
        {
            string formattedNumber = number.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = formattedNumber.ToString().Split('.');
            long intPart = long.Parse(parts[0]);
            long fractionPart = long.Parse(parts[1]);

            //Assumption: If 'en' culture, currency is US Dollars
            if(culture != null && culture.ToString() == "en")
            {
                string dollarValue = string.Empty;
                string centValue = string.Empty;
                
                if (intPart > 0)
                    dollarValue = intPart.ToWords(culture);
                if (fractionPart > 0)
                    centValue = fractionPart.ToWords(culture);

                if (intPart > 0 && fractionPart > 0)
                    return $"{dollarValue.ToUpper()} {Constants.DOLLARS} AND {centValue.ToUpper()} {Constants.CENTS}";
                else if(intPart <= 0 && fractionPart > 0)
                    return $"{centValue.ToUpper()} {Constants.CENTS}";
                else if (intPart > 0 && fractionPart <= 0)
                    return $"{dollarValue.ToUpper()} {Constants.DOLLARS}";
            }
            return string.Empty;
        }
    }
}

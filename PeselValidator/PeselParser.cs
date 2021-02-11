using System;
using System.Globalization;

namespace PeselValidator
{
    public class PeselParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>
        private bool ValidatePesel(string pesel)
        {
            if (!string.IsNullOrEmpty(pesel))
            {
                if (pesel.Length == 11)
                {
                    return int.TryParse(pesel, out int result);
                }
            }
            return false;
        }

        private int ExtractPeselDigit(int index, string pesel)
        {
            if (index >= 0 && index < pesel.Length)
            {
                char c = pesel[index];
                int i = (int) (pesel[index] - '0');
                if (i <0 || i > 9)
                    throw new ArgumentOutOfRangeException(nameof(pesel));
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public int GetBirthYear(string pesel)
        {
            int year = 10 * ExtractPeselDigit(0, pesel) + ExtractPeselDigit(1, pesel);
            int month = 10 * ExtractPeselDigit(2, pesel) + ExtractPeselDigit(3, pesel);

            if (month > 0 && month < 13)
                return year + 1900;

            if (month > 20 && month < 33)
                return year + 2000;

            if (month > 40 && month < 53)
                return year + 2100;

            if (month > 60 && month < 73)
                return year + 2200;

            return year;
        }

        public int GetBirthMonth(string pesel)
        {
            int month = 10 * ExtractPeselDigit(2, pesel) + ExtractPeselDigit(3, pesel);

            if (month > 20 && month < 33)
                return month - 20;

            if (month > 40 && month < 53)
                return month - 40;

            if (month > 60 && month < 73)
                return month - 60;

            return month;
        }

        public int GetBirthDay(string pesel)
        {
            return 10 * ExtractPeselDigit(4, pesel) + ExtractPeselDigit(5, pesel);
        }
    }
}

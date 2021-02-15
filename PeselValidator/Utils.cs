using System;

namespace PeselValidator
{
    public class Utils
    {
        private static readonly int[] Weights = {1, 3, 7, 9, 1, 3, 7, 9, 1, 3};

        public int CountCheckSum(string pesel)
        {
            int sum = 0;
            if (pesel.Length == PeselStruct.PeselSize)
            {
                PeselStruct peselObj = new PeselStruct(pesel);

                for (int i = 0; i < Weights.Length; i++)
                {
                    sum += Weights[i] * peselObj[i];
                }
            }

            int checkDigit = sum % 10;
            return checkDigit == 0 ? 0 : 10 - checkDigit;
        }

        public bool IsPeselValid(string pesel)
        {
            // Check if is number and 11 digit long
            if (pesel.Length == PeselStruct.PeselSize)
            {
                PeselStruct peselObj = new PeselStruct(pesel);
                
                // Check birth date
                if (DateValidator.IsDateValid(peselObj.Year, peselObj.Month, peselObj.Day))
                {
                    // Count weighted sum: 1 3 7 9 1 3 7 9 1 3
                    int lastDigit = CountCheckSum(pesel);
                    // 11th digit is check sum digit
                    return (lastDigit == peselObj[10]);
                }
            }
            return false;
        }

        /// <summary>
        /// Should return a number in range 0..9 when appropriate input.
        /// </summary>
        /// <param name="digit">A character representing digit.</param>
        /// <returns>Poistive number unpon success.</returns>
        public static int CharToDigit(char digit)
        {
            if (int.TryParse(digit.ToString(), out int result))
            {
                return result;
            }
            throw new ArgumentOutOfRangeException(nameof(digit));
        }

        public static int ExtractPeselDigit(int index, string pesel)
        {
            if (index >= 0 && index < pesel.Length)
            {
                int result = (int)(pesel[index] - '0');
                if (result < 0 || result > 9)
                    throw new ArgumentOutOfRangeException(nameof(pesel));
                return result;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public static int GetPeselBirthYear(int year, int month)
        {
            if (month > 0 && month < 13)
                return year + 1900;

            if (month > 20 && month < 33)
                return year + 2000;

            if (month > 40 && month < 53)
                return year + 2100;

            if (month > 60 && month < 73)
                return year + 2200;

            if (month > 80 && month < 93)
                return year + 1800;

            return year;
        }

        public static int GetPeselBirthMonth(int month)
        {
            if (month > 20 && month < 33)
                return month - 20;

            if (month > 40 && month < 53)
                return month - 40;

            if (month > 60 && month < 73)
                return month - 60;

            if (month > 80 && month < 93)
                return month - 80;

            return month;
        }

    }
}

using System;

namespace PeselValidator
{
    public class PeselStruct
    {
        public const int PeselSize = 11;

        public PeselStruct(string pesel)
        {
            if (string.IsNullOrEmpty((this.Pesel)))
                throw new NullReferenceException(nameof(pesel));

            if (pesel.Length != PeselSize)
                throw new ArgumentOutOfRangeException(nameof(pesel));

            if (!int.TryParse(pesel, out int result))
                throw new ArgumentException("The PESEL should be a number", nameof(pesel));

            this.Pesel = pesel;
        }

        public string Pesel { get; private set; }

        public int this[int index] => DigitAt(index);

        public int Day => BirthDay();

        public int Month => BirthMonth();

        public int Year => BirthYear();

        public int DigitAt(int index)
        {
            if (!string.IsNullOrEmpty((this.Pesel)))
            {
                if (index >= 0 && index < this.Pesel.Length)
                {
                    int result = (int) (this.Pesel[index] - '0');
                    if (result < 0 || result > 9)
                        throw new ArgumentOutOfRangeException(nameof(this.Pesel));
                    return result;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public int BirthDay()
        {
            return 10 * DigitAt(4) + DigitAt(5);
        }

        public int BirthMonth()
        {
            int month = 10 * DigitAt(2) + DigitAt(3);
            return Utils.GetPeselBirthMonth(month);
        }

        public int BirthYear()
        {
            int year = 10 * DigitAt(0) + DigitAt(1);
            int month = 10 * DigitAt(2) + DigitAt(3);
            return Utils.GetPeselBirthYear(year, month);
        }
    }
}

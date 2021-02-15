namespace PeselValidator
{
    public enum Gender
    {
        Female,
        Male,
    }

    public class PeselModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Number { get; set; }
        public int CheckSum { get; set; }

        public Gender GetSex()
        {
            return this.Number % 2 == 0 ? Gender.Female : Gender.Male;
        }

        public override string ToString()
        {
            return $"{Year:D2}{Month:D2}{Day:D2}{Number:D4}{CheckSum:D1}";
        }
    }
}

using System;
using System.Globalization;

namespace PeselValidator
{
    public class PeselParser
    {
        private Action<int, PeselModel>[] parserActions = null;

        public PeselParser()
        {
            this.parserActions = new Action<int, PeselModel>[]
            {
                (int digit, PeselModel model) => {model.Year = 10 * digit;},
                (int digit, PeselModel model) => {model.Year += digit;},
                (int digit, PeselModel model) => {model.Month = 10 * digit;},
                (int digit, PeselModel model) => {model.Month +=  digit;},
                (int digit, PeselModel model) => {model.Day = 10 * digit;},
                (int digit, PeselModel model) => {model.Day +=  digit;},
                (int digit, PeselModel model) => {model.Number = 1000 * digit;},
                (int digit, PeselModel model) => {model.Number += 100 * digit;},
                (int digit, PeselModel model) => {model.Number += 10 * digit;},
                (int digit, PeselModel model) => {model.Number +=  digit;},
                (int digit, PeselModel model) => {model.CheckSum =  digit;},
            };
        }

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

        public PeselModel ParsePesel(string pesel)
        {
            PeselModel model = new PeselModel();

            if (ValidatePesel(pesel))
            {
                for (int i = 0; i < pesel.Length; i++)
                {
                    if (i < parserActions.Length)
                    {
                        this.parserActions[i](Utils.CharToDigit(pesel[i]), model);
                    }
                }
            }

            return model;
        }



    }
}

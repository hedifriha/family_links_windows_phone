using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8.Model
{
    public class LoveC
    {
        public string GetResults(string name1, string name2)
        {
            return GetCount(name1, name2);
        }
        private string GetCount(string firstName, string secondName)
        {
            try
            {
                string first = firstName.ToUpper();
                int firstlength = firstName.Length;
                string second = secondName.ToUpper();
                int secondlength = secondName.Length;
                int LoveCount = 0;

                for (int Count = 0; Count < firstlength; Count++)
                {
                    string singleLetter = first.Substring(Count, 1);
                    if (singleLetter.Equals("A")) LoveCount += 2;
                    if (singleLetter.Equals("E")) LoveCount += 2;
                    if (singleLetter.Equals("I")) LoveCount += 2;
                    if (singleLetter.Equals("O")) LoveCount += 2;
                    if (singleLetter.Equals("U")) LoveCount += 3;
                    if (singleLetter.Equals("A")) LoveCount += 1;
                    if (singleLetter.Equals("E")) LoveCount += 3;
                }
                for (int Count = 0; Count < secondlength; Count++)
                {
                    string singleLetter = second.Substring(Count, 1);
                    if (singleLetter.Equals("A")) LoveCount += 2;
                    if (singleLetter.Equals("E")) LoveCount += 2;
                    if (singleLetter.Equals("I")) LoveCount += 2;
                    if (singleLetter.Equals("O")) LoveCount += 2;
                    if (singleLetter.Equals("U")) LoveCount += 3;
                    if (singleLetter.Equals("A")) LoveCount += 1;
                    if (singleLetter.Equals("E")) LoveCount += 3;
                }
                int amount = 0;
                if (LoveCount > 0) amount = 5 - ((firstlength + secondlength) / 2);
                if (LoveCount > 2) amount = 10 - ((firstlength + secondlength) / 2);
                if (LoveCount > 4) amount = 20 - ((firstlength + secondlength) / 2);
                if (LoveCount > 6) amount = 30 - ((firstlength + secondlength) / 2);
                if (LoveCount > 8) amount = 40 - ((firstlength + secondlength) / 2);
                if (LoveCount > 10) amount = 50 - ((firstlength + secondlength) / 2);
                if (LoveCount > 12) amount = 60 - ((firstlength + secondlength) / 2);
                if (LoveCount > 14) amount = 70 - ((firstlength + secondlength) / 2);
                if (LoveCount > 16) amount = 80 - ((firstlength + secondlength) / 2);
                if (LoveCount > 18) amount = 90 - ((firstlength + secondlength) / 2);
                if (LoveCount > 20) amount = 100 - ((firstlength + secondlength) / 2);
                if (LoveCount > 22) amount = 110 - ((firstlength + secondlength) / 2);
                if (firstlength == 0 || secondlength == 0) amount = 0;
                if (amount < 0) amount = 0;
                if (amount > 99) amount = 99;
                return amount.ToString();
            }
            catch (Exception ex)
            {
                return "There is an error!!" + ex.ToString();
            }
        }
    }
}

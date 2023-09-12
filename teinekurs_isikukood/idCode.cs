using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace teinekurs_isikukood
{
    public class IdCode
    {
        private readonly string _idCode;

        public IdCode(string idCode)
        {
            _idCode = idCode;
        }

        private bool IsValidLength()
        {
            return _idCode.Length == 11;
        }

        private bool ContainsOnlyNumbers()
        {
            // return _idCode.All(Char.IsDigit);

            for (int i = 0; i < _idCode.Length; i++)
            {
                if (!Char.IsDigit(_idCode[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private int GetGenderNumber()
        {
            return Convert.ToInt32(_idCode.Substring(0, 1));
        }

        private bool IsValidGenderNumber()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber > 0 && genderNumber < 7;
        }

        private int Get2DigitYear()
        {
            return Convert.ToInt32(_idCode.Substring(1, 2));
        }

        public int GetFullYear()
        {
            int genderNumber = GetGenderNumber();
            // 1, 2 => 18xx
            // 3, 4 => 19xx
            // 5, 6 => 20xx
            return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
        }

        private int GetMonth()
        {
            return Convert.ToInt32(_idCode.Substring(3, 2));
        }

        private bool IsValidMonth()
        {
            int month = GetMonth();
            return month > 0 && month < 13;
        }

        private static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }
        private int GetDay()
        {
            return Convert.ToInt32(_idCode.Substring(5, 2));
        }

        private bool IsValidDay()
        {
            int day = GetDay();
            int month = GetMonth();
            int maxDays = 31;
            if (new List<int> { 4, 6, 9, 11 }.Contains(month))
            {
                maxDays = 30;
            }
            if (month == 2)
            {
                if (IsLeapYear(GetFullYear()))
                {
                    maxDays = 29;
                }
                else
                {
                    maxDays = 28;
                }
            }
            return 0 < day && day <= maxDays;
        }

        private int CalculateControlNumberWithWeights(int[] weights)
        {
            int total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
            }
            return total;
        }

        private bool IsValidControlNumber()
        {
            int controlNumber = Convert.ToInt32(_idCode[^1..]);
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(weights);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // second round
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(weights2);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // third round, control number has to be 0
            return controlNumber == 0;
        }

        public bool IsValid()
        {
            return IsValidLength() && ContainsOnlyNumbers()
                && IsValidGenderNumber() && IsValidMonth()
                && IsValidDay()
                && IsValidControlNumber();
        }

        public DateOnly GetBirthDate()
        {
            int day = GetDay();
            int month = GetMonth();
            int year = GetFullYear();
            return new DateOnly(year, month, day);
        }
        public string Sunnikoht()
        {
            List<char> ikoodList = new List<char>(_idCode);
            string tahed8910 = ikoodList[7].ToString() + ikoodList[8].ToString() + ikoodList[9].ToString();
            int t = int.Parse(tahed8910);
            string haigla;

            if (1 < t && t < 10)
            {
                haigla = "Pärnu haigla ";
            }
            else if (11 < t && t < 19)
            {
                haigla = "Haapsalu haigla";
            }
            else if (21 < t && t < 220)
            {
                haigla = "Rakvere haigla";
            }
            else
            {
                haigla = "ohio";
            }
            //Console.WriteLine(haigla);
            return haigla;


            //    public int GetSunnikoht(object haigla)
            //{
            //    return Convert.ToInt32(haigla);

        }
        public int vozrast()
        {
            int day = GetDay();
            int month = GetMonth();
            int year = GetFullYear();
            int nowmonth = DateTime.Now.Month;
            int nowyear = DateTime.Now.Year;
            int nowday= DateTime.Now.Day;

            int age = nowyear - year;
            //esli denroshdenie eshe ne nastupil po mesecu, to vozrast na odin god mladshe
            if (nowmonth < month)
            {
                age--;
            }
            //esli denroshdenie eshe ne nastupila po dnju i mesecu, to vozrast na odin god mladshe
            else if (nowmonth == month)
            {
                if (nowday < day)
                {
                    age--;
                }
            }
            //Console.WriteLine(age);

            return age;
        }
        
        
            

        
        //public int GetSunnikoht(object haigla)
        //{
        //    Console.WriteLine(haigla);
        //    return Convert.ToInt32(haigla);
        //}
    }
}

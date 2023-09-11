using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace teinekurs_isikukood
{
    public class omamoodel
    {
        public string Sunnikoht(string ikood)
        {
            List<char> ikoodList = new List<char>(ikood);
            string tahed8910 = ikoodList[7].ToString() + ikoodList[8].ToString() + ikoodList[9].ToString();
            int t = int.Parse(tahed8910);
            string haigla;

            if (1 < t && t < 10)
            {
                haigla = "kuegogsoig b";
            }
            else if (11 < t && t < 19)
            {
                haigla = "tartu fsdfjsf";
            }
            else if (21 < t && t < 220)
            {
                haigla = "jdfjgdfgdfgdfgd";
            }
            else
            {
                haigla = "ohio";
            }
            return haigla;

        }
        public int GetSunnikoht(object haigla)
        {
            return Convert.ToInt32(haigla);
        }



















        //def sunnipaev(ikood:str)->str:

        //aasta=ikood[1]+ikood[2]
        //kuu= ikood[3]+ikood[4]
        //paev=ikood[5]+ikood[6]

        //return paev+"." + kuu + "." + aasta



    }
}

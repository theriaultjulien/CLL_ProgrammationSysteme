using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversions
{
    public static class CBase
    {
        //Valeurs des charactères Hexadécimaux
        public static string HexValues = "0123456789ABCDEF";

        //Méthodes pour faire les conversions
        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string BinaireToDecimal(string sBinaire)
        {
            string output;
            int nbDecimal = 0;
            int exposant = 0;
            for (int i = sBinaire.Length - 1; i >= 0; i--)
            {
                if (sBinaire[i] == '1')
                    nbDecimal += (int)Math.Pow(2, exposant);
                exposant++;
            }
            output = nbDecimal.ToString();
            return output;
        }

        public static string DecimalToBinaire(int nbDecimal)
        {
            string output = "";
            while (nbDecimal != 0)
            {
                output += (nbDecimal % 2).ToString();
                nbDecimal /= 2;
            }
            return ReverseString(output);
        }

        public static string HexaToDecimal(string sHexa)
        {
            string reversedHexa = ReverseString(sHexa).ToUpper(); //Reverse la string et rend les lettres majuscules pour calculer la valeur de chaque charactère
            string output = "";
            int nbDecimal = 0;

            for (int i = sHexa.Length - 1; i > -1; i--)
            {
                nbDecimal += CBase.HexValues.IndexOf(reversedHexa[i]) * (int)Math.Pow(16, i);
            }
            output = nbDecimal.ToString();

            return output;
        }

        public static string DecimalToHexa(int nbDecimal)
        {
            string output = "";
            while (nbDecimal != 0)
            {
                output += CBase.HexValues[(nbDecimal % 16)];
                nbDecimal /= 16;
            }
            return ReverseString(output);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conversions
{
    public partial class frfmConversions : Form
    {
        public frfmConversions()
        {
            InitializeComponent();
        }

        private void btnBinaireDecimal_Click(object sender, EventArgs e)
        {
            //Lire les données 
            string sBinaire = txtBinaireADec.Text;
            int nbDecimal = 0;
            int i = 0;
            int exposant = 0;

            //Valider les données
            if (sBinaire.Length > 8)
            {
                MessageBox.Show("Le nombre binaire doit être sur 8 bits ou moins");
                return;
            }
            while ((i < sBinaire.Length) && (sBinaire[i]=='0' || sBinaire[i] == '1'))
            {
                i++;
            }
            if (i < sBinaire.Length)
            {
                MessageBox.Show("Un nombre binaire peut seulement être composé de 1 ou de 0");
                return;
            }

            //Traitement
            for (i = sBinaire.Length - 1; i >= 0; i--)
            {
                if (sBinaire[i] == '1')
                    nbDecimal += (int)Math.Pow(2, exposant);
                exposant ++;
            }

            // Afficher résultat
            txtDecimalDeBin.Text = nbDecimal.ToString();
        }

        private void btnDecBin_Click(object sender, EventArgs e)
        {
            string sDecimal = txtDecimalABin.Text;
            string sBinaire = "";
            int nbDecimal;
            bool valide;

            //Valider données
            valide = int.TryParse(sDecimal, out nbDecimal);
            if (!valide)
            {
                MessageBox.Show("Le nombre entré est invalide");
                return;
            }

            //Traitement
                //Fonction pour reverse la string qui va sortir
            string Reverse(string s)
            {
                char[] charArray = s.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            while (nbDecimal != 0)
            {
                sBinaire += (nbDecimal % 2).ToString();
                nbDecimal /= 2;
            }

            //Afficher résultat
            txtBinaireDeDec.Text = Reverse(sBinaire);
        }

        private void btnHexaDec_Click(object sender, EventArgs e)
        {
            string sHexa = txtHexaADec.Text;
            int nbDecimal;
            bool valide;

            //Valider données
            valide = sHexa.All("0123456789abcdefABCDEF".Contains);
            if (!valide)
            {
                MessageBox.Show("Le nombre entré est invalide");
                return;
            }

            //Traitement
            nbDecimal = Convert.ToInt32(sHexa, 16);


            //Afficher
            txtDecimalDeHexa.Text = nbDecimal.ToString();

        }

        private void btnDecHexa_Click(object sender, EventArgs e)
        {

        }
    }
}

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

        //Méthodes publiques pour éviter la répétition de code pour les validations

        public bool isBinaire(string sBinaire)
        {
            int i = 0;
            if (sBinaire.Length > 8)
            {
                MessageBox.Show("Le nombre binaire doit être sur 8 bits ou moins");
                return false;
            }
            while ((i < sBinaire.Length) && (sBinaire[i] == '0' || sBinaire[i] == '1'))
            {
                i++;
            }
            if (i < sBinaire.Length)
            {
                MessageBox.Show("Un nombre binaire peut seulement être composé de 1 ou de 0");
                return false;
            }
            return true;
        }

        public bool isDecimal(string sDecimal, out int nbDecimal)
        {
            nbDecimal = 0;
            if (!(int.TryParse(sDecimal, out int iDecimal)))
            {
                MessageBox.Show("Le nombre entré est invalide");
                return false;
            }

            if (iDecimal < 0 || iDecimal > 255)
            {
                MessageBox.Show("Le nombre entré doit être entre 0 et 255");
                return false;
            }
            nbDecimal = iDecimal;
            return true;
        }

        public bool isHexa(string sHexa)
        {
            if (sHexa.ToUpper().All(CBase.HexValues.Contains) && sHexa.Length <= 2) return true;
            else MessageBox.Show("L'entrée Hexadécimale est invalide ou elle contient plus de 2 caractères");
            return false;
        }


        private void btnBinaireDecimal_Click(object sender, EventArgs e)
        {
            //Lire les données 
            string sBinaire = txtBinaireADec.Text;
            bool valide;

            //Valider les données
            valide = isBinaire(sBinaire);
            if (!valide) return;

            // Afficher résultat
            txtDecimalDeBin.Text = CBase.BinaireToDecimal(sBinaire);
        }

        private void btnDecBin_Click(object sender, EventArgs e)
        {
            string sDecimal = txtDecimalABin.Text;
            bool valide;

            //Valider données
            valide = isDecimal(sDecimal, out int nbDecimal);
            if (!valide) return;

            //Afficher résultat
            txtBinaireDeDec.Text = CBase.DecimalToBinaire(nbDecimal);
        }

        private void btnHexaDec_Click(object sender, EventArgs e)
        {
            string sHexa = txtHexaADec.Text;
            bool valide;

            //Valider données
            valide = isHexa(sHexa);
            if (!valide) return;

            //Afficher
            txtDecimalDeHexa.Text = CBase.HexaToDecimal(sHexa);

        }

        private void btnDecHexa_Click(object sender, EventArgs e)
        {
            string sDecimal = txtDecimalAHexa.Text;
            bool valide;

            //Valider
            valide = isDecimal(sDecimal, out int nbDecimal);
            if (!valide) return;

            //Afficher
            txtHexaDeDec.Text = CBase.DecimalToHexa(nbDecimal);
        }

        private void btnBinHexa_Click(object sender, EventArgs e)
        {
            //Lire les données 
            string sBinaire = txtBinAHexa.Text;
            bool valide;

            //Valider les données
            valide = isBinaire(sBinaire);
            if (!valide) return;

            //convert binary to dec then convert the dec to hexa
            //Afficher
            textBox10.Text = CBase.DecimalToHexa(Convert.ToInt32(CBase.BinaireToDecimal(sBinaire)));
        }

        private void btnHexaBin_Click(object sender, EventArgs e)
        {
            string sHexa = txtHexaABin.Text;
            bool valide;

            //Valider données
            valide = isHexa(sHexa);
            if (!valide) return;

            // Convertir le Hex en Dec et ensuite le Dec en binaire avec les fonctions déjà créées
            //Afficher
            txtBinDeHexa.Text = CBase.DecimalToBinaire(Convert.ToInt32(CBase.HexaToDecimal(sHexa)));

        }
    }
}

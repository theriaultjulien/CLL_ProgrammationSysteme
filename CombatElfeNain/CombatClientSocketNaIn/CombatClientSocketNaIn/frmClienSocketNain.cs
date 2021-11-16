using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatClientSocketNaIn.Classes;


namespace CombatClientSocketNaIn
{
    public partial class frmClienSocketNain : Form
    {
        Random m_r;
        Elfe m_elfe;
        Nain m_nain;
        public frmClienSocketNain()
        {
            InitializeComponent();
            m_r = new Random();
            Reset();
            btnReset.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        void Reset()
        {
            m_nain = new Nain(m_r.Next(10, 20), m_r.Next(2, 6), m_r.Next(0, 3));
            picNain.Image = m_nain.Avatar;
            AfficheStatNain();

            m_elfe = new Elfe(1, 0, 0);
            picElfe.Image = m_elfe.Avatar;
            AfficheStatElfe();
        }
        void AfficheStatNain()
        {
            lblVieNain.Text = "Vie: " + m_nain.Vie.ToString();
            lblForceNain.Text = "Force: " + m_nain.Force.ToString();
            lblArmeNain.Text = "Arme: " + m_nain.Arme.ToString();

            this.Update();
        }
        void AfficheStatElfe()
        {
            lblVieElfe.Text = "Vie: " + m_elfe.Vie.ToString();
            lblForceElfe.Text = "Force: " + m_elfe.Force.ToString();
            lblSortElfe.Text = "Sort: " + m_elfe.Sort.ToString();

            this.Update();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnFrappe.Enabled = true;
            Reset();
        }

        private void btnFrappe_Click(object sender, EventArgs e)
        {
	    //Déclaration variable
            Socket client;
            string reponse = "aucune";
            int nbOctetReception;
            byte[] tByteReception = new byte[50];
            ASCIIEncoding textByte = new ASCIIEncoding();
            byte[] tByteEnvoie;
            string[] infoCombattants;
            int vieElfe, vieNain, forceElfe, forceNain, sortElfe, armeNain;

            btnFrappe.Enabled = false;
            try
            {
                client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                client.Connect(IPAddress.Parse("127.0.0.1"), 8888);
                if (!client.Connected) MessageBox.Show("Assurez-vous que le serveur est démarrré et en attente d'un client");

                if (client.Connected)
                {
                    //envoie données nain
                    string envoie = m_nain.Vie.ToString() + ";" + m_nain.Force.ToString() + ";" + Array.IndexOf(m_nain.tArme, m_nain.Arme).ToString() + ";";
                    MessageBox.Show("Client: \r\nTransmet... " + envoie);
                    tByteEnvoie = textByte.GetBytes(envoie);
                    client.Send(tByteEnvoie);
                    Thread.Sleep(500);

                    //reception
                    MessageBox.Show("Client: réception de données serveur");
                    nbOctetReception = client.Receive(tByteReception);
                    reponse = Encoding.ASCII.GetString(tByteReception);
                    MessageBox.Show("\r\nReception... " + reponse);

                    //Split le string pour afficher les nouvelles stats nain/elfe
                    infoCombattants = reponse.Split(';');
                    vieNain = Convert.ToInt32(infoCombattants[0]);
                    forceNain = Convert.ToInt32(infoCombattants[1]);
                    armeNain = Convert.ToInt32(infoCombattants[2]);

                    vieElfe = Convert.ToInt32(infoCombattants[3]);
                    forceElfe = Convert.ToInt32(infoCombattants[4]);
                    sortElfe = Convert.ToInt32(infoCombattants[5]);

                    m_nain = new Nain(vieNain, forceNain, armeNain);
                    m_elfe = new Elfe(vieElfe, forceElfe, sortElfe);

                    AfficheStatNain();
                    AfficheStatElfe();

                    if (m_nain.Vie <= 0)
                    {
                        MessageBox.Show("Le gagnant est: ELFE!");
                        btnFrappe.Enabled = false;
                        btnReset.Enabled = true;
                    }else if (m_elfe.Vie <= 0)
                    {
                        MessageBox.Show("Le gagnant est: NAIN!");
                        btnFrappe.Enabled = false;
                        btnReset.Enabled = true;
                    }
                }
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnFrappe.Enabled = true;
        }
    }
}

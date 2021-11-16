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
using CombatServeurSocketElfe.Classes;

namespace CombatServeurSocketElfe
{
    public partial class frmServeurSocketElfe : Form
    {
        Random m_r;
        Nain m_nain;
        Elfe m_elfe;
        TcpListener m_ServerListener;
        Socket m_client;
        Thread m_thCombat;

        public frmServeurSocketElfe()
        {
            InitializeComponent();
            m_r = new Random();
            
            btnReset.Enabled = false;
            //Démarre un serveur de socket (TcpListener)
            m_ServerListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            m_ServerListener.Start();
            lstReception.Items.Add("Serveur démarré !");
            lstReception.Items.Add("PRESSER : << attendre un client >>");
            lstReception.Update();
            Reset();
            Control.CheckForIllegalCrossThreadCalls = false;

        }
        void Reset()
        {
            m_nain = new Nain(1, 0, 0);
            picNain.Image = m_nain.Avatar;
            AfficheStatNain();

            m_elfe = new Elfe(m_r.Next(10, 20), m_r.Next(2, 6), m_r.Next(2, 6));
            picElfe.Image = m_elfe.Avatar;
            AfficheStatElfe();
 
            lstReception.Items.Clear();
        }

        void AfficheStatNain()
        {
            lblVieNain.Text = "Vie: " + m_nain.Vie.ToString();
            lblForceNain.Text = "Force: " + m_nain.Force.ToString();
            lblArmeNain.Text = "Arme: " + m_nain.Arme.ToString();
            
            this.Update(); // pour s'assurer de l'affichage via le thread
        }
        void AfficheStatElfe()
        {
            lblVieElfe.Text = "Vie: " + m_elfe.Vie.ToString();
            lblForceElfe.Text = "Force: " + m_elfe.Force.ToString();
            lblSortElfe.Text = "Sort: " + m_elfe.Sort.ToString();

            this.Update(); // pour s'assurer de l'affichage via le thread
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }     

        private void btnAttente_Click(object sender, EventArgs e)
        {
            // Combat par un thread
            ThreadStart codeThread = new ThreadStart(Combat);
            Thread thCombat = new Thread(codeThread);
            thCombat.Start();

            //gestion boutons
            btnAttente.Enabled = false;

        }
        public void Combat() 
        {
            // déclarations de variables locales 
            string reponseServeur = "aucune";
            string receptionClient = "rien";
            int nbOctetReception;
            int noArme = 0, vie = 0, force = 0;
            string[] infoNain;
            string arme;
            byte[] tByteReception = new byte[50];
            ASCIIEncoding textByte = new ASCIIEncoding();
            byte[] tByteEnvoie;

            try
            {
                while (m_nain.Vie > 0 && m_elfe.Vie > 0)
                {
                    // Attendre connexion client
                    m_client = m_ServerListener.AcceptSocket();
                    lstReception.Items.Add("Client branché!");
                    lstReception.Update();
                    Thread.Sleep(500);

                    // Recevoir les données client
                    nbOctetReception = m_client.Receive(tByteReception);
                    receptionClient = Encoding.ASCII.GetString(tByteReception);

                    lstReception.Items.Add("du client: " + receptionClient);
                    lstReception.Update();

                    // Split sur le ; pour récupérer les données du nain
                    infoNain = receptionClient.Split(';');
                    vie = Convert.ToInt32(infoNain[0]);
                    force = Convert.ToInt32(infoNain[1]);
                    noArme = Convert.ToInt32(infoNain[2]);
                    arme = m_nain.tArme[noArme];

                    m_nain = new Nain(vie, force, noArme);
                    m_nain.Arme = arme;
                    AfficheStatNain();

                    // Execute Frapper
                    MessageBox.Show("Serveur: Frapper l'elfe");
                    m_nain.Frapper(m_elfe);
                    AfficheStatElfe();

                    // Execute LancerSort
                    MessageBox.Show("Serveur: Lancer un sort au nain");
                    m_elfe.LancerSort(m_nain);
                    AfficheStatNain();
                    AfficheStatElfe();

                    reponseServeur = m_nain.Vie.ToString() + ";" + m_nain.Force.ToString() + ";" + Array.IndexOf(m_nain.tArme, m_nain.Arme).ToString() + ";" + m_elfe.Vie.ToString() + ";" + m_elfe.Force.ToString() + ";" + m_elfe.Sort.ToString() + ";";
                    lstReception.Items.Add(reponseServeur);
                    lstReception.Update();

                    tByteEnvoie = textByte.GetBytes(reponseServeur);
                    m_client.Send(tByteEnvoie);
                    Thread.Sleep(500);

                    //vérifier gagnant
                    if (m_nain.Vie <= 0 || m_elfe.Vie <= 0)
                    {
                        m_client.Close();
                    }
                }
                btnReset.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            // il faut avoir un objet elfe et un objet nain instanciés
            m_elfe.Vie = 0;
            m_nain.Vie = 0;
            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void frmServeurSocketElfe_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnFermer_Click(sender,e);
            try
            {
                m_ServerListener.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}

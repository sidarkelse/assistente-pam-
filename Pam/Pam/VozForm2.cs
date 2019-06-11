using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;


namespace Pam
{
    public partial class VozForm2 : Form
    {
        private SpeechSynthesizer sp = new SpeechSynthesizer();
        public VozForm2()
        {

            InitializeComponent();

            comboBox1.Items.Clear();
            foreach(InstalledVoice voice in  sp.GetInstalledVoices())
            {
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
            comboBox1.SelectedIndex = 0; 
        }
        
        // form carregado
        private void VozForm2_Load(object sender, EventArgs e)
        {



        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Speaker.Setvoice(comboBox1.SelectedItem.ToString());
            Speaker.speak("a voz foi alterada", "pronto", "feito", "Ótima escolha");
        }
    }
}

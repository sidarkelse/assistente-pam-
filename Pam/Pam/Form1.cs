using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Speech.Recognition; // adiocionar namespace 

  // para  sintesí é preciso o SpeechSDK5.1, windows 10 system.speech
namespace Pam
{
    public partial class Form1 : Form
    {
        //Forms 
        private VozForm2 selectvoice = null;

        private SpeechRecognitionEngine engine; //engine de reconhecimento
        private bool isPamisListening = true;

       
        

        private browser browser;

      

        public Form1()
        {
            InitializeComponent();
        }
        
        private void LoadSpeech()
        {
            try


            {
                engine = new SpeechRecognitionEngine(); // instância
                engine.SetInputToDefaultAudioDevice(); //Microfone



                Choices cNumeros = new Choices();

                for (int i = 0; i <= 100; i++)
                    cNumeros.Add(i.ToString());



                Choices c_commandsOfSystem = new Choices();
                c_commandsOfSystem.Add(GrammRuleS.WhatTimesIS.ToArray()); //WhatTimeIS
                c_commandsOfSystem.Add(GrammRuleS.whatDateIS.ToArray());  //WhatdateIS
                c_commandsOfSystem.Add(GrammRuleS.PamStartListening.ToArray()); // Pam start listening 
                c_commandsOfSystem.Add(GrammRuleS.PamStopListening.ToArray()); //Pam stop listening
                c_commandsOfSystem.Add(GrammRuleS.MinimizeWindow.ToArray()); //Minimizar janela
                c_commandsOfSystem.Add(GrammRuleS.normalWindow.ToArray()); // janela em tamanho normal 
                c_commandsOfSystem.Add(GrammRuleS.ChangeVoice.ToArray()); // mudar a voz , mudando a voz
                c_commandsOfSystem.Add(GrammRuleS.OpenProgram.ToArray()); // abrir navegador 



                //"pare de ouvir" -> "Pam"


                GrammarBuilder gb_commandsOfSystem = new GrammarBuilder();
                gb_commandsOfSystem.Append(c_commandsOfSystem);

                Grammar g_commandsOfSystem = new Grammar(gb_commandsOfSystem);
                g_commandsOfSystem.Name = "sys";

                GrammarBuilder gbNumeros = new GrammarBuilder();
                gbNumeros.Append(cNumeros); //5 vezes
                gbNumeros.Append(new Choices("vezes", "mais ", "menos", "por", "para"));
                gbNumeros.Append(cNumeros);

                Grammar gNumeros = new Grammar(gbNumeros);
                gNumeros.Name = "calc";

                engine.LoadGrammar(g_commandsOfSystem); //carregar gramatica
                engine.LoadGrammar(gNumeros);

                // carregar a gramática   
                //engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(Audiolevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);


                engine.RecognizeAsync(RecognizeMode.Multiple); // Iniciar o Reconhecimento 


                 Speaker.Speak(" Olá Sid, No Que Posso Ajudar.");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu no LoadSpeech(): " + ex.Message);



            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            LoadSpeech();
            Speaker.Speak("Espero que você esteja bem");


        }


        //Metodo que é chamado quando algo é reocnhecido
        private void Rec(object s, SpeechRecognizedEventArgs e)

        {
            string speech = e.Result.Text; // string reconhecida
            float conf = e.Result.Confidence;

            string date = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string log_filename = "log//" + date + ".text";

            StreamWriter sw = File.AppendText(log_filename);

            if (File.Exists(log_filename))
                sw.WriteLine(speech);
            else
            {
                sw.WriteLine(speech);

            }

            sw.Close();

            if (conf > 0.35f)
            {
                this.label1.ForeColor = Color.LawnGreen;

                this.label1.Text = "reconhecido:" + speech;

                if (GrammRuleS.PamStopListening.Any(x => x == speech))
                {
                    isPamisListening = false;

                }
                else if (GrammRuleS.PamStartListening.Any(x => x == speech))
                {
                    isPamisListening = true;
                    Speaker.speak("Olá", "Olá, tudo bem", "Oi", "como vai", "estou aqui");


                }

                                

            }
            { 
                    if (isPamisListening == true)
                    {
                        switch (e.Result.Grammar.Name)
                        {
                            case "sys":
                                // se o speech  ==  "que horas são " 
                                if (GrammRuleS.WhatTimesIS.Any(x => x == speech))
                                {
                                    Runner.whatTimeIS();
                                }
                                else if (GrammRuleS.whatDateIS.Any(x => x == speech))
                                {
                                    Runner.WhatdateIS();


                                }
                                else if (GrammRuleS.MinimizeWindow.Any(x => x == speech))
                                {
                                    MinimizeWindow();
                                }
                                else if (GrammRuleS.normalWindow.Any(x => x == speech))
                                {
                                    Normalwindow();
                                }
                                else if (GrammRuleS.ChangeVoice.Any(x => x == speech))

                                {
                                    if (selectvoice == null || selectvoice.IsDisposed == true)
                                        selectvoice = new VozForm2();
                                    selectvoice.Show();

                                }
                                else if (GrammRuleS.OpenProgram.Any(x => x == speech))
                                {
                                    switch (speech)
                                    {
                                        case "Navegador":
                                            browser = new browser();
                                            browser.Show();

                                            break;
                                    }
                                }


                                break;
                            case "calc":

                                Speaker.Speak(calcresolve.solve(speech));
                                break;
                        }

                    }




                }


            }

            private void Audiolevel(object s, AudioLevelUpdatedEventArgs e)
            {
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = e.AudioLevel;
            }

            private void rej(object s, SpeechRecognitionRejectedEventArgs e)
            {
                this.label1.ForeColor = Color.Red;
            }
            //minimizar janela
            private void MinimizeWindow()
            {
                if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Minimized;
                    Speaker.speak("minimizando a janela", "como quiser", "tudo bem", "como quiser, vou fazer isso");
                }
                else
                {
                    Speaker.speak("ja esta Minimizada", "a janela ja esta minimizada", "ja fiz isso");
                }

            }
            private void Normalwindow()

            {
                if (this.WindowState == FormWindowState.Minimized)

                {



                    this.WindowState = FormWindowState.Normal;
                    Speaker.speak("Maximizando a janela", "como quiser", "vou fazer isso", "tudo bem", "irei fazer isso");
                }
                else
                {


                    Speaker.speak("janela ja está em tamanho normal", "Já fiz isso", "isso já foi feito", "tudo bem");

                }
            }

        }
    }       
      

    

    
     
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis; // namescape


namespace Pam
{
    public class Speaker
    {
        private static SpeechSynthesizer sp = new SpeechSynthesizer();
        public static void Speak(string text)
        {
            //caso ele esteja  falando
            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }
        public static void speak(params string[] texts)
        {
            Random rnd = new Random();
            Speak(texts[rnd.Next(0, texts.Length)]);
        }
        // Alterar a voz
        public static void Setvoice(string voice)
        {

            try
            {
                sp.SelectVoice(voice);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("error em  speaker: " + ex.Message);
            }

            }
        }
    }

    

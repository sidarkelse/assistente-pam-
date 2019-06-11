using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis; //namesepace

namespace Pam
{

    public class Speaker

    {
        private static SpeechSynthesizer sp = new SpeechSynthesizer();
        public static void Speak(string text)
        {
            //caso esteja falando 
            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }
    }
           
}

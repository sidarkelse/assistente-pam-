using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pam
{
    public class Runner
    {
        // fala que horas são
        public static void whatTimeIS ()
        {
            Speaker.Speak(DateTime.Now.ToLongTimeString());

        }
        public static void WhatdateIS()
        {
            Speaker.Speak(DateTime.Now.ToShortDateString());
        }
       

        
    }
}

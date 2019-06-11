using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pam
{
    public class GrammRuleS
    {
        public static IList<string> WhatTimesIS = new List<string>()
        {
             "Que horas são",
            "Me diga as horas",
            "Horas",
            "Poderia me dizer que horas são?",
            "me informe as horas",
            "diga as horas"

            };
        public static IList<string> whatDateIS = new List<string>()
    {
         "Data de hoje",
         "Qual é a data de hoje",
         "que dia é hoje",
         "Você sabe me dizer a data de hoje"


    };
        public static IList<string> PamStartListening = new List<string>()
        {
            "Pam",
            "Pam esta me ouvindo?",
            "pam você esta aí ?",
            "Olá Pam",
            "pode me ouvir",
            "Oi Pam",
            "eae pam",
            "como vc esta Pam"
        };
        public static IList<string> MinimizeWindow = new List<string>()
        {
            "Minimizar janela",
            "MInimize a janela",
            "Pam Minimize a jenale",
            "Diminua a janela"

        };
        public static IList<string> PamStopListening = new List<string>()
        {
            "Pare de ouvir",
            "Pare de me ouvir",
            "não me escute",
            "não me ouça agora"
        };
        public static IList<string> normalWindow = new List<string>()
        {
            "janela em tamanho normal",
            "tamanho normal",
            "deixe a janela em tamanho normal",
            "tamanho normal",
            "Maximizar janela"

        };
        public static IList<string> ChangeVoice = new List<string>()
        {
            "Altere a voz",
            "Alterar a voz ",
            "Mude a voz"
        };

        public static IList<string> OpenProgram = new List<string>()
        {
            "Navegador",
            "Abrindo Navegador",
            "Abra o navegador"


        };





    }
}
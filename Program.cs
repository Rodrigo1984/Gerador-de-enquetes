using System;
using System.Collections.Generic;

namespace Gerador_de_enquetes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Inicia a execução do programa.
            SurveyUI ui = new SurveyUI();
            ui.Start();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFlareResolver
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ativa estilos visuais para a aplicação
            Application.EnableVisualStyles();

            // Define a renderização de texto compatível
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicia a aplicação com o formulário principal
            Application.Run(new Form1());
        }
    }
}

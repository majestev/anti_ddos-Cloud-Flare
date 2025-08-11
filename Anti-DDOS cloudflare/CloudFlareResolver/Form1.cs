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

namespace CloudFlareResolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog a = new OpenFileDialog();
            if (a.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Solução: " + resolve(File.ReadAllText(a.FileName)));
            }
        }

        /// <summary>
        /// Ignora a detecção de bot-anti-ddos do Cloudflare
        ///
        /// Criado por CruZ
        /// </summary>
        /// <param name="html">Conteúdo HTML</param>
        /// <returns>jschl-answer</returns>
        public static int resolve(string html)
        {
            if (html.Contains("s,t,o,p,b,r,e,a,k,i,n,g,f"))
            {
                string domain = html.Remove(0, html.IndexOf("</span> ") + "</span> ".Length);
                domain = domain.Remove(domain.IndexOf("<") - 1);
                html = html.Remove(0, html.IndexOf("s,t,o,p,b,r,e,a,k,i,n,g,f, ") + "s,t,o,p,b,r,e,a,k,i,n,g,f, ".Length);
                html = html.Remove(html.IndexOf(".action"));
                string var = html.Remove(html.IndexOf("="));
                string var2 = html.Remove(0, html.IndexOf("\"") + 1);
                var2 = var2.Remove(var2.IndexOf("\""));
                int count = 0;
                html = html.Remove(0, html.IndexOf(var2) + var2.Length + 2);
                string expresion = html.Remove(html.IndexOf("}"));
                count = expresionResolver(expresion);
                html = html.Remove(0, html.IndexOf("challenge-form") + "challenge-form".Length + 3);
                html = html.Remove(html.IndexOf(";a.value"));
                html = html.Remove(0, html.IndexOf(";") + 1);
                string[] expresions = html.Split(new string[] { ";" }, StringSplitOptions.None);
                for (int i = 0; i < expresions.Length; i++)
                {
                    string operador = expresions[i].Replace(var + "." + var2, "");
                    operador = operador.Remove(1);
                    if (operador == "*")
                        count *= expresionResolver(expresions[i].Remove(0, expresions[i].IndexOf("=") + 1));
                    else if (operador == "+")
                        count += expresionResolver(expresions[i].Remove(0, expresions[i].IndexOf("=") + 1));
                    else if (operador == "-")
                        count -= expresionResolver(expresions[i].Remove(0, expresions[i].IndexOf("=") + 1));
                    else if (operador == "/")
                        count /= expresionResolver(expresions[i].Remove(0, expresions[i].IndexOf("=") + 1));
                    else if (operador == "%")
                        count %= expresionResolver(expresions[i].Remove(0, expresions[i].IndexOf("=") + 1));
                    else
                        throw new Exception("Operador inválido.");
                }
                return count + domain.Length;
            }
            else
            {
                throw new Exception("Não é uma proteção anti-ddos do Cloudflare.");
            }
        }

        private static int expresionResolver(string expresion)
        {
            string op;
            if (expresion.Contains("("))
            {
                op = expresion.Remove(1);
                expresion = expresion.Remove(0, 1);
            }
            else
                op = "+";

            // Substitui padrões do Cloudflare para valores numéricos
            expresion = expresion.Replace(@"!![]", "1").Replace(@"!+[]", "1").Replace("(1", "(+1");

            if (expresion[0] == '1')
                expresion = "+" + expresion;

            if (expresion.Contains("[]"))
            {
                string n1 = (expresion.Remove(expresion.IndexOf("[")).Split(new string[] { "+1" }, StringSplitOptions.None).Length - 1).ToString();
                string n2 = (expresion.Remove(0, expresion.IndexOf("]") + 1).Split(new string[] { "+1" }, StringSplitOptions.None).Length - 1).ToString();

                return (op == "+" ? 1 : -1) * Convert.ToInt32((n1 + n2));
            }

            return (op == "+" ? 1 : -1) * expresion.Split(new string[] { "+1" }, StringSplitOptions.None).Length - 1;
        }
    }
}

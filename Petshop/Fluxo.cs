using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Petshop
{
    internal class Fluxo
    {
        public static void Executar()
        {
            string line;
            try
            {

                StreamReader sr = new("C:\\Projetos\\Petshop\\Entidade\\Cadastro.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exceção " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executando finalmente o bloco.");
            }
        }
    }
}

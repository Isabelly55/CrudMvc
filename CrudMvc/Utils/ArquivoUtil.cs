using System;
using System.IO;

namespace CrudMvc.Utils
{
    public class ArquivoUtil
    {
        public static void GravarArquivo(String caminho, String texto)
        {
            try
            {
                StreamWriter sw = new(@caminho, false);
                sw.WriteLine(texto);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block");
            }
        }

        public static String RecuperarArquivo(String caminho)
        {
            string arquivo = caminho;

            String line = "";
            if (File.Exists(arquivo))
            {
                try
                {
                    using StreamReader sr = new(arquivo);
                    String linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        line += linha;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("O arquivo " + arquivo + "não foi localizado!");
            }
            return line;
        }
    }
}

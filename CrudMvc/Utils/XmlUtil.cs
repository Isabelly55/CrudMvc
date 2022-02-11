using System;
using System.IO;
using System.Xml.Serialization;

namespace CrudMvc.Utils
{
    public class XmlUtil
    {

        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using StreamReader sr = new(xml);
                XmlSerializer xmldes = new(type);
                return xmldes.Deserialize(sr);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Serializer(object obj)
        {
            MemoryStream Stream = new();
            XmlSerializer xml = new(obj.GetType());
            try
            {
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        public static void Serializer(object ListaEquipamentos, String caminho)
        {
            try
            {
                XmlSerializer serializer = new(ListaEquipamentos.GetType());

                TextWriter filestream = new StreamWriter(@caminho);

                serializer.Serialize(filestream, ListaEquipamentos);

                filestream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" " + ex);
            }
        }
    }
}

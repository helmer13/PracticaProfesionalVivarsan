using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Logica
{
   public class Utiles
    {
        public static string toXML(object objeto)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;


            try
            {
                XmlSerializer serializer = new XmlSerializer(objeto.GetType());
                tw = new XmlTextWriter(sw);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(tw, objeto, ns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }


            var resultado = sw.ToString().Remove(0, 39);

            return resultado;
        }

        /// <summary>
        /// Combiente la fecha, si de la bd viene null, queda null y si no es de tipo date
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime? ConvertirFecha(string fecha)
        {
            try
            {

                if (fecha == string.Empty)
                {
                    return null;
                }
                else
                {
                    return DateTime.Parse(fecha);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}

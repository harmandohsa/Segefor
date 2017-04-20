using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SEGEFOR.Clases
{
    public class Cl_Xml
    {
        public XmlDocument CrearDocumentoXML(string pNodoRaiz)
        {
            XmlDocument iResultado = new XmlDocument();

            XmlDeclaration iDeclaracion = iResultado.CreateXmlDeclaration("1.0", null, null);
            iResultado.AppendChild(iDeclaracion);

            XmlElement iRaiz = iResultado.CreateElement(pNodoRaiz);
            iResultado.AppendChild(iRaiz);

            return iResultado;
        }

        public XmlAttribute AgregarAtributo(String pEtiqueta, Object pValor, XmlNode pContenedor)
        {
            XmlAttribute iResultado = pContenedor.OwnerDocument.CreateAttribute(pEtiqueta);
            iResultado.Value = pValor.ToString();
            pContenedor.Attributes.Append(iResultado);

            return iResultado;
        }
    }
}
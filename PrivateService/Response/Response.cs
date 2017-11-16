using System;
using System.Xml.Serialization;

namespace PrivatService.Response
{
    [Serializable]
    //[DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false,ElementName = "response")]
    public class Response
    {
        [XmlElement("merchant")]
        public Merchant Merchant { get; set; }

        [XmlElement("data")]
        public Data Data { get; set; }

        [XmlAttribute("version")]
        public decimal Version { get; set; }
        
    }
}
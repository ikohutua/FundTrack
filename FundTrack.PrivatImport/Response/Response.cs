using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
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
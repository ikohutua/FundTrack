using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Data
    {
        [XmlElement("oper")]
        public string Oper { get; set; }

        [XmlElement("info")]
        public Info Info { get; set; }
    }
}
using System;
using System.Xml.Serialization;

namespace PrivatService.Response
{
    [Serializable]
    //[DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Data
    {
        [XmlElement("oper")]
        public string Oper { get; set; }

        [XmlElement("info")]
        public Info Info { get; set; }
    }
}
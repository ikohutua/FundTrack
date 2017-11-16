using System;
using System.Xml.Serialization;

namespace PrivatService.Response
{
    [Serializable]
    //[DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Info
    {
        [XmlElement("statements")]
        public Statements Statements { get; set; }
    }
}
using System;
using System.Xml.Serialization;

namespace PrivatService.Response
{
    [Serializable]
    //[DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Merchant
    {
        [XmlElement("id")]
        public uint Id { get; set; }

        [XmlElement("signature")]
        public string Signature { get; set; }

    }
}
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Merchant
    {
        [XmlElement("id")]
        public uint Id { get; set; }

        [XmlElement("signature")]
        public string Signature { get; set; }

    }
}
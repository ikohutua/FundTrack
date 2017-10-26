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
        private uint _id;
        private string _signature;

        [XmlElement("id")]
        public uint Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [XmlElement("signature")]
        public string Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                _signature = value;
            }
        }
    }
}
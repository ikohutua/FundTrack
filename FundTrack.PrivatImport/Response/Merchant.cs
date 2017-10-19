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

        public uint id
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

        public string signature
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
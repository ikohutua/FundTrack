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
        private Merchant _merchant;
        private Data _data;
        private decimal _version;

        public Merchant merchant
        {
            get
            {
                return _merchant;
            }
            set
            {
                _merchant = value;
            }
        }

        public Data data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        [XmlAttribute]
        public decimal version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
    }
}
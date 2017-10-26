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

        [XmlElement("merchant")]
        public Merchant Merchant
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

        [XmlElement("data")]
        public Data Data
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

        [XmlAttribute("version")]
        public decimal Version
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
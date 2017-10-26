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
        private string _oper;
        private Info _info;

        [XmlElement("oper")]
        public string Oper
        {
            get
            {
                return _oper;
            }
            set
            {
                _oper = value;
            }
        }

        [XmlElement("info")]
        public Info Info
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
            }
        }
    }
}
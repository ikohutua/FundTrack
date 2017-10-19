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

        public string oper
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

        public Info info
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
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Statements
    {
        private Statement[] _statement;
        private string _status;
        private decimal _credit;
        private decimal _debet;

        [XmlElement("statement")]
        public Statement[] Statement
        {
            get
            {
                return _statement;
            }
            set
            {
                _statement = value;
            }
        }

        [XmlAttribute("status")]
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        [XmlAttribute("credit")]
        public decimal Credit
        {
            get
            {
                return _credit;
            }
            set
            {
                _credit = value;
            }
        }

        [XmlAttribute("debet")]
        public decimal Debet
        {
            get
            {
                return _debet;
            }
            set
            {
                _debet = value;
            }
        }
    }
}
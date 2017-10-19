using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Statement
    {
        private ulong _card;
        private uint _appcode;
        private DateTime _trandate;
        private DateTime _trantime;
        private string _amount;
        private string _cardamount;
        private string _rest;
        private string _terminal;
        private string _description;

        [XmlAttribute]
        public ulong card
        {
            get
            {
                return _card;
            }
            set
            {
                _card = value;
            }
        }

        [XmlAttribute]
        public uint appcode
        {
            get
            {
                return _appcode;
            }
            set
            {
                _appcode = value;
            }
        }

        [XmlAttribute(DataType = "date")]
        public DateTime trandate
        {
            get
            {
                return _trandate;
            }
            set
            {
                _trandate = value;
            }
        }

        [XmlAttribute(DataType = "time")]
        public DateTime trantime
        {
            get
            {
                return _trantime;
            }
            set
            {
                _trantime = value;
            }
        }

        [XmlAttribute]
        public string amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        [XmlAttribute]
        public string cardamount
        {
            get
            {
                return _cardamount;
            }
            set
            {
                _cardamount = value;
            }
        }

        [XmlAttribute]
        public string rest
        {
            get
            {
                return _rest;
            }
            set
            {
                _rest = value;
            }
        }

        [XmlAttribute]
        public string terminal
        {
            get
            {
                return _terminal;
            }
            set
            {
                _terminal = value;
            }
        }

        [XmlAttribute]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }
}
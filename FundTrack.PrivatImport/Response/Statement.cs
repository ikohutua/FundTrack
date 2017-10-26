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

        [XmlAttribute("card")]
        public ulong Card
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

        [XmlAttribute("appcode")]
        public uint Appcode
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

        [XmlAttribute("trandate", DataType = "date")]
        public DateTime Trandate
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

        [XmlAttribute("trantime", DataType = "time")]
        public DateTime Trantime
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

        [XmlAttribute("amount")]
        public string Amount
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

        [XmlAttribute("cardamount")]
        public string Cardamount
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

        [XmlAttribute("rest")]
        public string Rest
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

        [XmlAttribute("terminal")]
        public string Terminal
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

        [XmlAttribute("description")]
        public string Description
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
using System;
using System.Xml.Serialization;

namespace PrivatService.Response
{
    [Serializable]
   // [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Statement
    {
        [XmlAttribute("card")]
        public ulong Card { get; set; }

        [XmlAttribute("appcode")]
        public uint Appcode { get; set; }

        [XmlAttribute("trandate", DataType = "date")]
        public DateTime Trandate { get; set; }

        [XmlAttribute("trantime", DataType = "time")]
        public DateTime Trantime { get; set; }

        [XmlAttribute("amount")]
        public string Amount { get; set; }

        [XmlAttribute("cardamount")]
        public string Cardamount { get; set; }

        [XmlAttribute("rest")]
        public string Rest { get; set; }

        [XmlAttribute("terminal")]
        public string Terminal { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
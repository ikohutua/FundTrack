﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Statements
    {
        [XmlElement("statement")]
        public Statement[] Statement { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }

        [XmlAttribute("credit")]
        public decimal Credit { get; set; }

        [XmlAttribute("debet")]
        public decimal Debet { get; set; }
    }
}
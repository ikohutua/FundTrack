using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Info
    {
        private Statements _statements;

        [XmlElement("statements")]
        public Statements Statements
        {
            get
            {
                return _statements;
            }
            set
            {
                _statements = value;
            }
        }
    }
}
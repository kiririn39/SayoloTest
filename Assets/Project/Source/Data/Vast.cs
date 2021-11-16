using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Project.Source.Data
{
    [Serializable, XmlRoot("VAST")]
    public class Vast
    {
        public Ad Ad { get; set; }
    }

    public class Ad
    {
        public InLine InLine { get; set; }
    }

    public class InLine
    {
        public string Error { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = nameof(Creative))]
        public List<Creative> Creatives { get; set; }
    }

    public class Creative
    {
        public Linear Linear { get; set; }
    }

    public class Linear
    {
        [XmlIgnore]
        public TimeSpan DurationSpan => TimeSpan.Parse(Duration);
        
        public string Duration { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "MediaFile")]
        public List<string> MediaFiles { get; set; }
    }
}
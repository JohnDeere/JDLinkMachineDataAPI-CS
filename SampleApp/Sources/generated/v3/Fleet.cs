using System.Collections.Generic;
using System.Xml.Serialization;

namespace SampleApp.Sources.democlient
{
    [XmlRoot(ElementName = "Links", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Links
    {
        [XmlElement(ElementName = "rel", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Rel { get; set; }
        [XmlElement(ElementName = "href", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Href { get; set; }
    }

    [XmlRoot(ElementName = "Link", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Link
    {
        [XmlElement(ElementName = "rel", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string rel { get; set; }
        [XmlElement(ElementName = "uri", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string uri { get; set; }
    }


    [XmlRoot(ElementName = "EquipmentHeader", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class EquipmentHeader
    {
        [XmlElement(ElementName = "OEMName", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string OEMName { get; set; }
        [XmlElement(ElementName = "Model", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Model { get; set; }
        [XmlElement(ElementName = "EquipmentID", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string EquipmentID { get; set; }
        [XmlElement(ElementName = "SerialNumber", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string SerialNumber { get; set; }
        [XmlElement(ElementName = "PIN", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string PIN { get; set; }
    }

    [XmlRoot(ElementName = "Equipment", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Equipment
    {
        [XmlElement(ElementName = "EquipmentHeader", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public EquipmentHeader EquipmentHeader { get; set; }
        [XmlElement(ElementName = "Location", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public Location Location { get; set; }
        [XmlElement(ElementName = "CumulativeIdleHours", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public CumulativeIdleHours CumulativeIdleHours { get; set; }
        [XmlElement(ElementName = "CumulativeLoadCount", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public CumulativeLoadCount CumulativeLoadCount { get; set; }
        [XmlElement(ElementName = "CumulativeOperatingHours", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public CumulativeOperatingHours CumulativeOperatingHours { get; set; }
        [XmlElement(ElementName = "CumulativePayloadTotals", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public CumulativePayloadTotals CumulativePayloadTotals { get; set; }
        [XmlElement(ElementName = "Distance", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public Distance Distance { get; set; }
        [XmlElement(ElementName = "FuelUsed", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public FuelUsed FuelUsed { get; set; }
        [XmlElement(ElementName = "DEFRemaining", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public DEFRemaining DEFRemaining { get; set; }
        [XmlElement(ElementName = "FuelRemaining", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public FuelRemaining FuelRemaining { get; set; }
    }

    [XmlRoot(ElementName = "Location", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Location
    {
        [XmlElement(ElementName = "Latitude", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "Longitude", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Longitude { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "CumulativeIdleHours", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class CumulativeIdleHours
    {
        [XmlElement(ElementName = "Hour", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Hour { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "CumulativeLoadCount", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class CumulativeLoadCount
    {
        [XmlElement(ElementName = "Count", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Count { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "CumulativeOperatingHours", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class CumulativeOperatingHours
    {
        [XmlElement(ElementName = "Hour", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Hour { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "CumulativePayloadTotals", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class CumulativePayloadTotals
    {
        [XmlElement(ElementName = "PayloadUnits", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string PayloadUnits { get; set; }
        [XmlElement(ElementName = "Payload", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Payload { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "Distance", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Distance
    {
        [XmlElement(ElementName = "OdometerUnits", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string OdometerUnits { get; set; }
        [XmlElement(ElementName = "Odometer", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Odometer { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "FuelUsed", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class FuelUsed
    {
        [XmlElement(ElementName = "FuelUnits", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string FuelUnits { get; set; }
        [XmlElement(ElementName = "FuelConsumed", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string FuelConsumed { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "DEFRemaining", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class DEFRemaining
    {
        [XmlElement(ElementName = "Percent", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Percent { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "FuelRemaining", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class FuelRemaining
    {
        [XmlElement(ElementName = "Percent", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public string Percent { get; set; }
        [XmlAttribute(AttributeName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "Fleet", Namespace = "http://standards.iso.org/iso/15143/-3")]
    public class Fleet
    {
        [XmlElement(ElementName = "Links", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public List<Links> Links { get; set; }
        [XmlElement(ElementName = "Equipment", Namespace = "http://standards.iso.org/iso/15143/-3")]
        public List<Equipment> Equipment { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "snapshotTime")]
        public string SnapshotTime { get; set; }
    }

}

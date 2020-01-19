using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace persistedXml.Models
{
    [Serializable]
    [XmlRoot("product")]
    public class ProductImportMetaData
    {
        [XmlElement("id")]
        public int id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public Nullable<decimal> Price { get; set; }

        [XmlElement("quantity")]
        public Nullable<int> Quantity { get; set; }
    }

    [MetadataType(typeof(ProductImportMetaData))]
    public partial class Product {
    }
}
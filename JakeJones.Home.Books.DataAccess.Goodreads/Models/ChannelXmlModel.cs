using System.Collections.Generic;
using System.Xml.Serialization;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Models
{
	[XmlRoot("channel")]
	public class ChannelXmlModel
	{
		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("item")]
		public ItemXmlModel[] Items { get; set; }
	}
}

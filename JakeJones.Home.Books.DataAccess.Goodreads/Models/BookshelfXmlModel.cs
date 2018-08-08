using System.Xml.Serialization;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Models
{
	[XmlRoot("rss")]
	public class BookshelfXmlModel
	{
		[XmlElement("channel")]
		public ChannelXmlModel Channel { get; set; }
	}
}

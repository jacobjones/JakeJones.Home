using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Serialization;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Models
{
	public class ItemXmlModel
	{
		[XmlElement("author_name")]
		public string Author { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("book_large_image_url")]
		public string Image { get; set; }

		[XmlElement("link")]
		public string Link { get; set; }

		[XmlIgnore]
		public DateTime? Read => !string.IsNullOrWhiteSpace(UserReadAt) ? DateTime.Parse(UserReadAt) : (DateTime?) null;

		[XmlElement("user_read_at")]
		public string UserReadAt { get; set; }
	}
}

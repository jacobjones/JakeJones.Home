using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using JakeJones.Home.Blog.Builders;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Resolvers;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace JakeJones.Home.Blog.Implementation.Builders
{
	internal class BlogRssFeedBuilder : IBlogRssFeedBuilder
	{
		private readonly IBlogManager _blogManager;
		private readonly IBlogUrlResolver _blogUrlResolver;

		public BlogRssFeedBuilder(IBlogManager blogManager, IBlogUrlResolver blogUrlResolver)
		{
			_blogManager = blogManager;
			_blogUrlResolver = blogUrlResolver;
		}

		public async Task<string> Build()
		{
			// Get the last 10 posts
			// TODO: Move to config
			var posts = await _blogManager.Get(10);

			var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);

			using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Async = true, Indent = true }))
			{
				var feedWriter = new RssFeedWriter(xmlWriter);

				await feedWriter.WriteTitle("Jake Jones: Blog");
				await feedWriter.WriteLanguage(new CultureInfo("en-US"));

				foreach (var post in posts)
				{
					await feedWriter.Write(BuildSyndicationItem(post));
				}
				
				await xmlWriter.FlushAsync();
			}

			return stringWriter.ToString();
		}

		private ISyndicationItem BuildSyndicationItem(IPost post)
		{
			var absoluteUrl = _blogUrlResolver.GetUrl(post, true);

			var item = new SyndicationItem
			{
				Title = post.Title,
				Description = post.Excerpt,
				Id = absoluteUrl
			};

			if (post.PublishDate.HasValue)
			{
				item.Published = post.PublishDate.Value;
			}

			item.AddLink(new SyndicationLink(new Uri(absoluteUrl)));
			item.AddContributor(new SyndicationPerson("Jake Jones", "jacob.jones@valtech.com"));

			return item;
		}

		class StringWriterWithEncoding : StringWriter
		{
			public StringWriterWithEncoding(Encoding encoding)
			{
				Encoding = encoding;
			}

			public override Encoding Encoding { get; }
		}
	}
}
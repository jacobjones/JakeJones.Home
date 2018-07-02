using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace JakeJones.Home.Blog.Models
{
	public class Post : IPost
	{
		public Post()
		{
		}

		public Post(string title, string excerpt, string content)
		{
			Title = title;
			Segment = CreateSegment(title);
			Excerpt = excerpt;
			Content = content;
		}

		public Post(string title, string segment, string excerpt, string content)
		{
			Title = title;
			Segment = segment;
			Excerpt = excerpt;
			Content = content;
		}

		[Required]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Segment { get; set; }

		[Required]
		public string Excerpt { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime? PublishDate { get; set; }

		public DateTime LastModified { get; set; }

		public bool IsPublished { get; set; }

		public IList<string> Tags { get; set; } = new List<string>();

		// TODO: Comments
		//public IList<Comment> Comments { get; } = new List<Comment>();


		//public bool AreCommentsOpen(int commentsCloseAfterDays)
		//{
		//	return PublishDate.AddDays(commentsCloseAfterDays) >= DateTime.UtcNow;
		//}

		private static string CreateSegment(string segment)
		{
			segment = segment.ToLowerInvariant().Replace(" ", "-");
			segment = RemoveDiacritics(segment);
			segment = RemoveReservedUrlCharacters(segment);

			return segment.ToLowerInvariant();
		}

		private static string RemoveReservedUrlCharacters(string text)
		{
			var reservedCharacters = new[]
			{
				"!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\",
				"^", "_", "'", "{", "}", "|", "~", "`", "+"
			};

			foreach (var chr in reservedCharacters)
			{
				text = text.Replace(chr, "");
			}

			return text;
		}

		private static string RemoveDiacritics(string text)
		{
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}
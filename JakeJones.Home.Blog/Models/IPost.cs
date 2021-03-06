﻿using System;
using System.Collections.Generic;

namespace JakeJones.Home.Blog.Models
{
	public interface IPost
	{
		int Id { get; set; }

		string Title { get; set; }

		string Segment { get; set; }

		string Excerpt { get; set; }

		string Content { get; set; }

		DateTimeOffset? PublishDate { get; set; }

		DateTimeOffset LastModified { get; set; }

		bool IsPublished { get; set; }

		IList<string> Tags { get; set; }
	}
}
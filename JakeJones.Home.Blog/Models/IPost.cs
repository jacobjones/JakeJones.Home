﻿using System;
using System.Collections.Generic;

namespace JakeJones.Home.Blog.Models
{
	public interface IPost
	{
		string Id { get; set; }

		string Title { get; set; }

		string Slug { get; set; }

		string Excerpt { get; set; }

		string Content { get; set; }

		DateTime PublishDate { get; set; }

		DateTime LastModified { get; set; }

		bool IsPublished { get; set; }

		IList<string> Categories { get; set; }
	}
}

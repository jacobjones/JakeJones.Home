using AutoMapper;
using System;

namespace JakeJones.Home.Blog.Implementation
{
	public class NullableDateTimeOffsetConverter : ITypeConverter<DateTimeOffset?, DateTime?>
	{
		public DateTime? Convert(DateTimeOffset? source, DateTime? destination, ResolutionContext context)
		{
			if (source.HasValue)
			{
				return source.Value.DateTime;
			}

			return default;
		}
	}
}

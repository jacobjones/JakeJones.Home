using AutoMapper;
using System;

namespace JakeJones.Home.Blog.Implementation
{
	public class DateTimeOffsetConverter : ITypeConverter<DateTimeOffset, DateTime>
	{
		public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
		{
			return source.DateTime;
		}
	}
}

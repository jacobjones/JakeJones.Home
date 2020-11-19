using System;
using Newtonsoft.Json.Converters;

namespace JakeJones.Home.Core.Infrastructure.Json
{
	public class ConcreteTypeConverter<T> : CustomCreationConverter<T> where T : class, new()
	{
		public override T Create(Type objectType)
		{
			return new T();
		}

		public override bool CanConvert(Type objectType)
		{
			return false;
		}

		public override bool CanRead => true;

		public override bool CanWrite => false;
	}
}
namespace JakeJones.Home.Core.Implementation.Configuration
{
	public interface IHoneypotOptions
	{
		string FieldNamePrefix { get; }

		bool DisableAutocomplete { get; }
	}
}
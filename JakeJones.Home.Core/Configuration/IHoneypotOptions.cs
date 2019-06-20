namespace JakeJones.Home.Core.Configuration
{
	public interface IHoneypotOptions
	{
		string FieldNamePrefix { get; }

		bool DisableAutocomplete { get; }
	}
}
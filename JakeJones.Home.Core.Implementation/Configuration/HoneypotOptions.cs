namespace JakeJones.Home.Core.Implementation.Configuration
{
	internal class HoneypotOptions : IHoneypotOptions
	{
		public string FieldNamePrefix { get; } = "hpf_";
		public bool DisableAutocomplete { get; } = true;
	}
}
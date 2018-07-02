namespace JakeJones.Home.Core.Generators
{
	/// <summary>
	/// Provides functionality to generate URL segments.
	/// </summary>
	public interface ISegmentGenerator
	{
		/// <summary>
		/// Get a URL segment for a given name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>A URL friendly segment.</returns>
		string Get(string name);
	}
}

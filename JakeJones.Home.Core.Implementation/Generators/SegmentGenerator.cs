using System.Globalization;
using System.Text;
using JakeJones.Home.Core.Generators;

namespace JakeJones.Home.Core.Implementation.Generators
{
	internal class SegmentGenerator : ISegmentGenerator
	{
		public string Get(string name)
		{
			return string.IsNullOrEmpty(name) ? null : GetFriendlyUrl(name);
		}

		/// <summary>
		/// Creates the actual friendly URL.
		/// </summary>
		/// <remarks>
		/// Based on: https://www.johanbostrom.se/blog/how-to-create-a-url-and-seo-friendly-string-in-csharp-text-to-slug-generator
		/// </remarks>
		/// <param name="text">The text.</param>
		/// <param name="maxLength">The max length.</param>
		/// <returns>A URL friendly string.</returns>
		private static string GetFriendlyUrl(string text, int maxLength = 0)
		{
			var normalized = text.ToLowerInvariant().Normalize(NormalizationForm.FormD);

			var stringBuilder = new StringBuilder(normalized.Length);
			var prevDash = false;

			foreach (var c in normalized)
			{
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
					// Check if the character is a letter or a digit if the character is a
					// international character remap it to an ascii valid character
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.DecimalDigitNumber:
						// Only take ASCII characters
						if (c < 128)
						{
							stringBuilder.Append(c);
							prevDash = false;
						}
						break;
					// Check if the character is to be replaced by a hyphen but only if the last character wasn't
					case UnicodeCategory.SpaceSeparator:
					case UnicodeCategory.ConnectorPunctuation:
					case UnicodeCategory.DashPunctuation:
					case UnicodeCategory.OtherPunctuation:
					case UnicodeCategory.MathSymbol:
						if (!prevDash)
						{
							stringBuilder.Append('-');
							prevDash = true;
						}
						break;
				}

				// If we are at max length, stop parsing
				if (maxLength > 0 && stringBuilder.Length >= maxLength)
				{
					break;
				}
			}

			// Trim excess hyphens
			var result = stringBuilder.ToString().Trim('-');

			// Remove any excess character to meet maxlength criteria
			return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
		}
	}
}

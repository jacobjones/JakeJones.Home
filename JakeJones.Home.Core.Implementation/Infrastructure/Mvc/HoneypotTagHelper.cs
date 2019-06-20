using JakeJones.Home.Core.Configuration;
using JakeJones.Home.Core.Implementation.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace JakeJones.Home.Core.Implementation.Infrastructure.Mvc
{
	public class HoneypotTagHelper : TagHelper
	{
		private readonly IHoneypotOptions _honeypotOptions;

		public HoneypotTagHelper(IHoneypotOptions honeypotOptions)
		{
			_honeypotOptions = honeypotOptions;
		}

		public string Name { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			base.Process(context, output);

			output.TagName = "div";

			string name = $"{_honeypotOptions.FieldNamePrefix}{Name}";

			var input = new TagBuilder("input");
			input.MergeAttribute("name", name);
			input.MergeAttribute("id", name);
			input.MergeAttribute("type", "text");

			if (_honeypotOptions.DisableAutocomplete)
			{
				input.MergeAttribute("autocomplete", "off");
			}

			output.Content.AppendHtml(input);
		}
	}
}

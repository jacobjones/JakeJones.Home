namespace JakeJones.Home.Website.Models
{
	public class PaginationViewModel
	{
		public PaginationViewModel(string nextUrl, bool showNext, string prevUrl, bool showPrev)
		{
			NextUrl = nextUrl;
			ShowNext = showNext;
			PrevUrl = prevUrl;
			ShowPrev = showPrev;
			NextText =  "Next &raquo;";
			NextText = "&laquo; Previous";
		}

		public PaginationViewModel(string nextUrl, string nextText, bool showNext, string prevUrl, string prevText, bool showPrev)
		{
			NextUrl = nextUrl;
			NextText = nextText;
			ShowNext = showNext;
			PrevUrl = prevUrl;
			PrevText = prevText;
			ShowPrev = showPrev;
		}

		public string NextUrl { get; set; }

		public string NextText { get; set; }

		public bool ShowNext { get; set; }

		public string PrevUrl { get; set; }

		public string PrevText { get; set; }

		public bool ShowPrev { get; set; }
	}
}
using GamesLibrary.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GamesLibrary.TagHelpers
{
	public class PaginationTagHelper : TagHelper
	{
		public Func<int, string> PageUrl { get; set; }
		public PageInfo PageInfo { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";

			StringBuilder result = new StringBuilder();


			int coef = 2;
			int left = PageInfo.PageNumber - coef;
			int right = PageInfo.PageNumber + coef + 1;


			for (int i = 1; i <= PageInfo.TotalPages; i++)
			{
				if (i == 1 || i == PageInfo.TotalPages || i >= left && i < right)
				{

					TagBuilder tag = new TagBuilder("a");
					tag.MergeAttribute("href", PageUrl(i));
					tag.InnerHtml = i.ToString();
					if (i == PageInfo.PageNumber)
					{
						tag.AddCssClass("selected");
						tag.AddCssClass("btn-primary");
					}
					tag.AddCssClass("btn btn-default");
					result.Append(tag.ToString());

				}
			}



			output.Content.SetHtmlContent(result.ToString());
			output.TagMode = TagMode.StartTagAndEndTag;



		}

		//public HtmlString PageLinks(this HtmlString pageLinks, PageInfo pageInfo, Func<int, string> pageUrl)
		//{
		//    StringBuilder result = new StringBuilder();
		//    for (int i = 1; i < pageInfo.TotalPages; i++)
		//    {
		//        TagBuilder tag = new TagBuilder("a");
		//        tag.MergeAttribute("href", pageUrl(i));
		//        tag.InnerHtml = i.ToString();
		//        if(i == pageInfo.PageNumber)
		//        {
		//            tag.AddCssClass("selected");
		//            tag.AddCssClass("btn-primary");
		//        }
		//        tag.AddCssClass("btn btn-default");
		//        result.Append(tag.ToString());
		//    }
		//    return new HtmlString(result.ToString());
		//}
	}
}

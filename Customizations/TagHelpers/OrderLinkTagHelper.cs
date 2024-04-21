using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorsoNetCore.Customizations.TagHelpers
{
    public class OrderLinkTagHelper : AnchorTagHelper
    {
        public string OrderBy { get; set; }
        public PaginationModel Model { get; set; }

        public OrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            

            output.TagName = "a";

            RouteValues["OrderBy"] = OrderBy;
            RouteValues["IsAscending"] = (OrderBy == Model.OrderBy && Model.IsAscending.HasValue ? !Model.IsAscending.Value : true).ToString();

            base.Process(context, output);

            if (Model.OrderBy == OrderBy)
            {
                var orderDirection = Model.IsAscending.HasValue && Model.IsAscending.Value ? "up" : "down";
                output.PostContent.SetHtmlContent($"<i class=\"fa-solid fa-sort-{orderDirection}\"></i>");
            }
        }
    }
}
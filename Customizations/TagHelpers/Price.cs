using CorsoNetCore.Models.DataTypes;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorsoNetCore.Customizations.TagHelpers
{
    public class Price : TagHelper
    {
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml($"<span>{CurrentPrice}</span>");

            if(!CurrentPrice.Equals(FullPrice)){
                output.Content.AppendHtml($"<br/ ><s>{FullPrice}</s>");
            }
        }
    }
}
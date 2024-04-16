using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorsoNetCore.Customizations.TagHelpers
{
    public class Rating : TagHelper
    {
        public double Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            for (var i = 1; i <= 5; i++)
            {
                if (Value >= i)
                {
                    output.Content.AppendHtml($"<i class=\"fas fa-star\"></i>");
                }
                else if (Value > i - 0.5)
                {
                    output.Content.AppendHtml($"<i class=\"fas fa-star-half-alt\"></i>");
                }
                else
                {
                    output.Content.AppendHtml($"<i class=\"far fa-star\"></i>");
                }
            }
        }
    }
}
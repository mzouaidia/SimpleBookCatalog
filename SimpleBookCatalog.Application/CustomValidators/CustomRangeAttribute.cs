using System.ComponentModel.DataAnnotations;

namespace SimpleBookCatalog.Application.CustomValidators
{
    internal class CustomRangeAttribute() : RangeAttribute(typeof(DateTime),
        "1950-01-01",
        DateTime.Now.AddDays(7).ToShortDateString());
}

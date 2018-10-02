using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraniteHouse.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items select new SelectListItem
            {
                Text = item.GetPropertyValue("Name"),
                Value = item.GetPropertyValue("ID"),
                Selected = item.GetPropertyValue("ID").Equals(selectedValue.ToString()),
            };
        }
    }
}
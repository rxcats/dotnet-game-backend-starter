using System;
using System.Text;

namespace RxCats.Core.Extensions;

public static class ToStringExtensions
{
    public static string GetPropertiesAsString(this object obj)
    {
        var sb = new StringBuilder();

        sb.Append("{ ");

        foreach (var property in obj.GetType().GetProperties())
        {
            sb.Append(property.Name);
            sb.Append('=');
            if (property.GetIndexParameters().Length > 0)
            {
                Console.Error.WriteLine($"Indexed Property({property.Name}) cannot be used.");
                sb.Append(string.Empty);
            }
            else
            {
                sb.Append(property.GetValue(obj, null));
            }

            sb.Append(", ");
        }

        return sb.ToString().TrimEnd(',', ' ') + " }";
    }
}
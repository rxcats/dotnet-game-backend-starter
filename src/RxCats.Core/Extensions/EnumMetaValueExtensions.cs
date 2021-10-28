namespace RxCats.Core.Extensions;

public static class EnumMetaValueExtensions
{
    public static string GetMetaValue(this Enum val)
    {
        var type = val.GetType();

        var fieldInfo = type.GetField(val.ToString());

        if (fieldInfo == null) return string.Empty;

        var attributes = fieldInfo.GetCustomAttributes(typeof(EnumMetaValueAttribute), false) as EnumMetaValueAttribute[];

        return attributes?.Length > 0 ? attributes[0].Value : string.Empty;
    }
}
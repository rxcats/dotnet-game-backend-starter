namespace RxCats.Core.Extensions;

[AttributeUsage(AttributeTargets.Field)]
[Serializable]
public class EnumMetaValueAttribute : Attribute
{
    public string Value { get; }

    public EnumMetaValueAttribute(string str)
    {
        Value = str;
    }
}
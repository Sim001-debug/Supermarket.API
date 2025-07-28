using System.ComponentModel;

namespace Supermarket.API.Extensions
{
    //extension method to extract descriptions,

    public static class EnumExtensions
    {
        public static string ToDescriptionString<TEnum> (this TEnum @enum) where TEnum : Enum
        {
            var type = @enum.GetType();
            var specificEnum = type.GetMember(@enum.ToString());
            var attribute = specificEnum[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attribute.Length > 0
                ? ((DescriptionAttribute)attribute[0]).Description
                : @enum.ToString();
        }
    }
}

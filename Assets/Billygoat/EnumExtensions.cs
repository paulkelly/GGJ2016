using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace Billygoat.Extensions
{
    public static class EnumExtensions
    {
        public static bool HasAnyFlag(this Enum flags, Enum flag)
        {
            if (Enum.GetUnderlyingType(flags.GetType()) == typeof(ulong))
            {
                ulong keysVal = Convert.ToUInt64(flags);
                ulong flagVal = Convert.ToUInt64(flag);
                return (keysVal & flagVal) != 0;
            }
            else
            {
                long keysVal = Convert.ToInt64(flags);
                long flagVal = Convert.ToInt64(flag);
                return (keysVal & flagVal) != 0;
            }
        }

        public static bool HasFlag(this Enum flags, Enum flag)
        {
            if (Enum.GetUnderlyingType(flags.GetType()) == typeof(ulong))
            {
                ulong keysVal = Convert.ToUInt64(flags);
                ulong flagVal = Convert.ToUInt64(flag);
                return (keysVal & flagVal) == flagVal;
            }
            else
            {
                long keysVal = Convert.ToInt64(flags);
                long flagVal = Convert.ToInt64(flag);
                return (keysVal & flagVal) == flagVal;
            }
        }

        public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)((TEnum)EnumValue));
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Aya.DataBinding
{
    public static partial class GUIMenu
    {
        public static void DrawPropertyMenu(Type filterPropertyType, Type targetType, string propertyName, SerializedProperty property)
        {
            var menu = CreateSearchableDropdownMenu(nameof(MemberTypes.Property),
                GetTargetPropertyList(filterPropertyType, targetType),
                memberInfo =>
                {
                    var displayName = memberInfo.MemberType + " / " + memberInfo.Name;
                    return displayName;
                },
                memberInfo => EditorIcon.GetIcon("ScriptableObject Icon"),
                memberInfo =>
                {
                    property.stringValue = memberInfo == null ? "" : memberInfo.Name;
                    property.serializedObject.ApplyModifiedProperties();
                });

            DrawSearchableDropdownMenu(propertyName,
                menu,
                () => string.IsNullOrEmpty(property.stringValue),
                () => property.stringValue);
        }

        public static List<MemberInfo> GetTargetPropertyList(Type filterPropertyType, Type targetType)
        {
            var result = new List<MemberInfo>();
            var flags = TypeCaches.DefaultBindingFlags;
            var properties = targetType.GetProperties(flags);
            foreach (var propertyInfo in properties)
            {
                if (filterPropertyType != null && propertyInfo.PropertyType != filterPropertyType) continue;
                result.Add(propertyInfo);
            }

            var fields = targetType.GetFields(flags);
            foreach (var fieldInfo in fields)
            {
                if (filterPropertyType != null && fieldInfo.FieldType != filterPropertyType) continue;
                result.Add(fieldInfo);
            }

            return result;
        }
    }
}
#endif
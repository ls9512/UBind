#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static class EditorIcon
    {
        #region Icon Method

        public static Texture2D CreateIcon(Type type, int size = 24)
        {
            var srcIcon = GetIcon(type);
            if (srcIcon == null) return default;
            var icon = CreateIconWithSrc(srcIcon, size);
            return icon;
        }

        public static Texture2D CreateIcon(string name, int size = 24)
        {
            var srcIcon = GetIcon(name);
            if (srcIcon == null) return default;
            var icon = CreateIconWithSrc(srcIcon, size);
            return icon;
        }

        public static Texture2D GetIcon(Type type)
        {
            if (type != null) return EditorGUIUtility.ObjectContent(null, type).image as Texture2D;
            return default;
        }

        public static Texture2D GetIcon(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var icon = EditorGUIUtility.FindTexture(name);
                if (icon == null) icon = AssetDatabase.GetCachedIcon(name) as Texture2D;
                if (icon == null) icon = EditorGUIUtility.IconContent(name).image as Texture2D;
                return icon;
            }

            return default;
        }

        public static Texture2D CreateIconWithSrc(Texture2D srcIcon, int size = 24)
        {
            // Copy Built-in texture with RenderTexture
            var tempRenderTexture = RenderTexture.GetTemporary(size, size, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(srcIcon, tempRenderTexture);
            var previousRenderTexture = RenderTexture.active;
            RenderTexture.active = tempRenderTexture;
            var icon = new Texture2D(size, size);
            icon.ReadPixels(new Rect(0, 0, tempRenderTexture.width, tempRenderTexture.height), 0, 0);
            icon.Apply();
            RenderTexture.ReleaseTemporary(tempRenderTexture);
            RenderTexture.active = previousRenderTexture;
            return icon;
        }

        #endregion
    }

}
#endif
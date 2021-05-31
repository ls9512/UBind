#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    public static class EditorStyle
    {
        public static string NoneStr = "<None>";
        public static Color ErrorColor = new Color(1f, 0.5f, 0.5f);
        public static Color SplitLineColor = new Color(0f, 0f, 0f, 0.5f);

        public static GUIStyle RichLabel
        {
            get
            {
                if (_richLabel == null)
                {
                    _richLabel = EditorStyles.label;
                    _richLabel.richText = true;
                }

                return _richLabel;
            }
        }

        private static GUIStyle _richLabel;
    }
}
#endif
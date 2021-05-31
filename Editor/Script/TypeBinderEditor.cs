#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    [CustomEditor(typeof(TypeBinder)), CanEditMultipleObjects]
    public class TypeBinderEditor : BaseBinderEditor
    {
        public TypeBinder TypeBinder => target as TypeBinder;

        protected SerializedProperty ContextKeyProperty;
        protected SerializedProperty DataKeyProperty;
        protected SerializedProperty DirectionProperty;
        protected SerializedProperty UpdateTypeProperty;

        protected SerializedProperty AssemblyProperty;
        protected SerializedProperty TypeProperty;
        protected SerializedProperty MapProperty;

        public virtual void OnEnable()
        {
            ContextKeyProperty = serializedObject.FindProperty("Context");
            DataKeyProperty = serializedObject.FindProperty("Key");
            DirectionProperty = serializedObject.FindProperty("Direction");
            UpdateTypeProperty = serializedObject.FindProperty("UpdateType");

            AssemblyProperty = serializedObject.FindProperty("Assembly");
            TypeProperty = serializedObject.FindProperty("Type");
            MapProperty = serializedObject.FindProperty("Map");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawContextKey(ContextKeyProperty);
            DrawDataKey(DataKeyProperty);
            DrawDirection(DirectionProperty);

            EditorUtil.AssemblyMenu("Assembly", AssemblyProperty);
            EditorUtil.TypeMenu("Type", TypeProperty, AssemblyProperty.stringValue);

            var currentType = TypeCaches.GetTypeByName(AssemblyProperty.stringValue, TypeProperty.stringValue);
            if (currentType != null)
            {
                DrawTypeMapList(currentType);
            }
            else
            {
                if (TypeBinder.Map.Count > 0)
                {
                    TypeBinder.Map.Clear();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawTypeMapList(Type currentType)
        {
            var foldOutTitle = "Map [" + TypeBinder.Map.Count + "]";
            using (GUIFoldOut.Create(this, foldOutTitle))
            {
                if (!GUIFoldOut.GetState(this, foldOutTitle)) return;
                for (var i = 0; i < TypeBinder.Map.Count; i++)
                {
                    var map = TypeBinder.Map[i];
                    using (GUIGroup.Create())
                    {
                        using (GUIHorizontal.Create())
                        {
                            using (GUIVertical.Create())
                            {
                                var itemProperty = MapProperty.GetArrayElementAtIndex(i);
                                var propertyProperty = itemProperty.FindPropertyRelative("Property");
                                var componentProperty = itemProperty.FindPropertyRelative("Target");
                                var targetPropertyProperty = itemProperty.FindPropertyRelative("TargetProperty");

                                EditorUtil.PropertyTreeMenu("Source Prop", currentType, propertyProperty);
                                DrawTargetAndProperty<Component>("Target", componentProperty, TypeBinder.transform, "Target Prop", targetPropertyProperty);
                            }

                            var btnDelete = GUILayout.Button("×", GUILayout.Width(EditorGUIUtility.singleLineHeight));
                            if (btnDelete)
                            {
                                TypeBinder.Map.Remove(map);
                            }
                        }
                    }

                    EditorUtil.ColorLine(EditorStyle.SplitLineColor, 2);
                }

                if (GUILayout.Button("＋"))
                {
                    var map = new TypeBindMap();
                    TypeBinder.Map.Add(map);
                }
            }
        }
    }
}
#endif
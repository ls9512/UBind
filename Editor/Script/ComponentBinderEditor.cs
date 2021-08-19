#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.DataBinding
{
    [CustomEditor(typeof(ComponentBinder<,,>)), CanEditMultipleObjects]
    public abstract class ComponentBinderEditor<TComponent, TValue, TDataBinder> : BaseBinderEditor
        where TDataBinder : DataBinder<TComponent, TValue>, new()
        where TComponent : Component
    {
        public ComponentBinder<TComponent, TValue, TDataBinder> ComponentBinder => target as ComponentBinder<TComponent, TValue, TDataBinder>;

        protected SerializedProperty ContextKeyProperty;
        protected SerializedProperty DataKeyProperty;
        protected SerializedProperty DirectionProperty;
        protected SerializedProperty UpdateTypeProperty;
        protected SerializedProperty TargetProperty;

        public virtual void OnEnable()
        {
            ContextKeyProperty = serializedObject.FindProperty(nameof(ComponentBinder.Context));
            DataKeyProperty = serializedObject.FindProperty(nameof(ComponentBinder.Key));
            DirectionProperty = serializedObject.FindProperty(nameof(ComponentBinder.Direction));
            UpdateTypeProperty = serializedObject.FindProperty(nameof(ComponentBinder.UpdateType));
            TargetProperty = serializedObject.FindProperty(nameof(ComponentBinder.Target));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawContextKey(ContextKeyProperty);
            DrawDataKey(DataKeyProperty);
            DrawDirection(DirectionProperty);

            // Update
            if (ComponentBinder.IsSource && ComponentBinder.NeedUpdate)
            {
                EditorGUILayout.PropertyField(UpdateTypeProperty);
            }

            DrawTarget<TComponent>(TargetProperty, ComponentBinder.transform);

            DrawBody();

            serializedObject.ApplyModifiedProperties();
        }

        
        public virtual void DrawBody()
        {

        }
    }
}
#endif
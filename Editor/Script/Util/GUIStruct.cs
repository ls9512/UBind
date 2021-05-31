#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.DataBinding
{
    internal struct GUIVertical : IDisposable
    {
        public static GUIVertical Create(params GUILayoutOption[] options)
        {
            return new GUIVertical(options);
        }

        public static GUIVertical Create(GUIStyle style, params GUILayoutOption[] options)
        {
            return new GUIVertical(style, options);
        }

        private GUIVertical(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
        }

        private GUIVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }

    internal struct GUIHorizontal : IDisposable
    {
        public static GUIHorizontal Create(params GUILayoutOption[] options)
        {
            return new GUIHorizontal(options);
        }

        public static GUIHorizontal Create(GUIStyle style, params GUILayoutOption[] options)
        {
            return new GUIHorizontal(style, options);
        }

        private GUIHorizontal(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
        }

        private GUIHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
        }

        public void Dispose()
        {
            GUILayout.EndHorizontal();
        }
    }

    internal struct ColorScope : IDisposable
    {
        public Color OriginalColor;

        public ColorScope(Color color, Func<bool> predicate = null)
        {
            OriginalColor = GUI.color;
            if (predicate == null || predicate())
            {
                GUI.color = color;
            }
        }

        public void Dispose()
        {
            GUI.color = OriginalColor;
        }
    }

    internal struct GUIGroup : IDisposable
    {
        public static GUIGroup Create(params GUILayoutOption[] options)
        {
            return new GUIGroup(options);
        }

        private GUIGroup(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }

    internal struct GUIFoldOut : IDisposable
    {
        public static Dictionary<Object, Dictionary<string, bool>> StateCacheDic = new Dictionary<Object, Dictionary<string, bool>>();

        public static GUIFoldOut Create(Object target, string title, bool defaultState = true, params GUILayoutOption[] options)
        {
            return new GUIFoldOut(target, title, defaultState, options);
        }

        public static GUIFoldOut Create(Object target, string title, params GUILayoutOption[] options)
        {
            return new GUIFoldOut(target, title, true, options);
        }

        private GUIFoldOut(Object target, string title, bool defaultState = true, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, options);
            var rect = EditorGUILayout.GetControlRect();
            var state = GetState(target, title, defaultState);
            var currentState = GUI.Toggle(rect, state, GUIContent.none, EditorStyles.foldout);
            if (currentState != state)
            {
                SetState(target, title, currentState);
            }

            rect.xMin += rect.height;
            EditorGUI.LabelField(rect, title, EditorStyles.boldLabel);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }

        public static bool GetState(Object target, string title, bool defaultState = true)
        {
            var stateDic = GetStateDic(target);
            if (!stateDic.TryGetValue(title, out var result))
            {
                stateDic.Add(title, defaultState);
            }

            return result;
        }

        public static void SetState(Object target, string title, bool value)
        {
            var stateDic = GetStateDic(target);
            if (!stateDic.TryGetValue(title, out var result))
            {
                stateDic.Add(title, value);
            }
            else
            {
                stateDic[title] = value;
            }
        }

        public static Dictionary<string, bool> GetStateDic(Object target)
        {
            if (!StateCacheDic.TryGetValue(target, out var stateDic))
            {
                stateDic = new Dictionary<string, bool>();
                StateCacheDic.Add(target, stateDic);
            }

            return stateDic;
        }
    }
}
#endif
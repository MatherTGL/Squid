#if !UNITY_EDITOR
#else

using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace ByteCobra.Logging.Editor
{
    [CustomEditor(typeof(LogConfiguration))]
    [Serializable]
    public class LogSettingsInspector : CustomInspector
    {
        private LogConfiguration _target => (LogConfiguration)target;

        public override VisualElement CreateInspectorGUI()
        {
            var root = base.CreateInspectorGUI();

            VisualElement uxml = root;
            root.Q<Toggle>("stacktrace-toggle").value = _target.CaptureStackTrace;

            var dateTimeField = root.Q<TextField>("TimeFormat");
            dateTimeField.value = _target.DateFormat;
            dateTimeField.RegisterValueChangedCallback(evt => _target.DateFormat = evt.newValue);
            return root;
        }

        private VisualElement CreateDefaultInspector()
        {
            var root = new VisualElement();
            var editor = CreateEditor(target);
            var inspectorIMGUI = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
            root = new VisualElement();
            root.Add(inspectorIMGUI);
            return root;
        }
    }
}

#endif
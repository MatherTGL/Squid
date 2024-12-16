using UnityEditor;
using UnityEngine.UIElements;

namespace ByteCobra.Logging
{
    public static class UIUtils
    {
        public static VisualElement CreateUnityEvent(SerializedObject rootObject)
        {
            return new IMGUIContainer(() =>
            {
                rootObject?.Update();
                EditorGUI.BeginChangeCheck();
                if (EditorGUI.EndChangeCheck())
                {
                    rootObject?.ApplyModifiedProperties();
                }
            });
        }
    }
}
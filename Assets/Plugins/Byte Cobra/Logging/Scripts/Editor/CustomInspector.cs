#if !UNITY_EDITOR
#else

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ByteCobra.Logging.Editor
{
    public abstract class CustomInspector : UnityEditor.Editor
    {
        [SerializeField]
        protected VisualTreeAsset baseVisualTree;

        protected VisualElement VisualElement;
        protected GameObject GameObject => ((MonoBehaviour)target).gameObject;

        private string ContactButton => "ContactButton";
        private string Logo => "Logo";
        private string OnlineDocsButton => "OnlineDocsButton";

        private string ContactURL = "https://assetstore.unity.com/publishers/48876";

        public override VisualElement CreateInspectorGUI()
        {
            var tc = baseVisualTree.CloneTree();
            VisualElement = tc;

            Button contactButton = VisualElement.Query<Button>(ContactButton);
            if (contactButton != null)
            {
                SetButtonLink(contactButton, ContactURL);
            }
            return tc;
        }

        protected void SetLogo(Texture2D logo)
        {
            IMGUIContainer logoContainer = VisualElement.Query<IMGUIContainer>(Logo);
            if (logoContainer != null)
            {
                logoContainer.style.backgroundImage = new StyleBackground(logo);
            }
        }

        protected void SetButtonLink(Button button, string url)
        {
            Debug.Assert(button != null);
            button.clicked += () =>
            {
                System.Diagnostics.Process.Start(url);
            };
        }

        protected void SetOnlineDocumentationLink(string url)
        {
            Button onlineDocsButton = VisualElement.Query<Button>(OnlineDocsButton);
            if (onlineDocsButton != null)
            {
                SetButtonLink(onlineDocsButton, url);
            }
        }
    }
}

#endif
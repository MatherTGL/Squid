using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ByteCobra.Logging.Editor
{
    [CustomEditor(typeof(FilterData))]
    public class FilterEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset rowTree;
        [SerializeField] private VisualTreeAsset mainTree;

        private FilterData filterData;
        private VisualElement rowsContainer;

        public override VisualElement CreateInspectorGUI()
        {
            filterData = (FilterData)target;
            var root = mainTree.CloneTree();
            rowsContainer = root.Q<VisualElement>("rows-container");  // Assuming you have an element with id "rows-container" in your mainTree to hold the rows

            root.Q<Button>("add-button").clicked += () => AddNewRow();

            SyncRowsWithFilterData();

            return root;
        }

        private void SyncRowsWithFilterData()
        {
            rowsContainer.Clear();

            for (int i = 0; i < filterData.Filters.Count; i++)
            {
                int index = i;  // Capture the current index for the delegate
                var row = rowTree.CloneTree();
                var textField = row.Q<TextField>("value");
                var enumField = row.Q<EnumField>("level");
                enumField.Init(filterData.Filters[i].Level);  // Initialize the EnumField with the correct enum type

                var deleteButton = row.Q<Button>("delete-button");

                textField.value = filterData.Filters[i].Value;
                enumField.value = filterData.Filters[i].Level;

                textField.RegisterValueChangedCallback(evt =>
                {
                    filterData.Filters[index].Value = evt.newValue;
                    EditorUtility.SetDirty(filterData);
                });

                enumField.RegisterValueChangedCallback(evt =>
                {
                    filterData.Filters[index].Level = (LogLevel)evt.newValue;
                    EditorUtility.SetDirty(filterData);
                });

                deleteButton.clicked += () => DeleteRow(index);
                rowsContainer.Add(row);
            }
        }

        private void AddNewRow()
        {
            filterData.Filters.Add(new FilterData.FilterEntry());
            EditorUtility.SetDirty(filterData);
            SyncRowsWithFilterData();
        }

        private void DeleteRow(int index)
        {
            filterData.Filters.RemoveAt(index);
            EditorUtility.SetDirty(filterData);
            SyncRowsWithFilterData();
        }
    }
}
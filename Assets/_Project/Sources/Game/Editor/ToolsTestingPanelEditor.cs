using UnityEditor;
using UnityEngine;
using Sources.Gameplay.Runtime.Panels;

namespace Sources.Game.Editor
{
    [CustomEditor(typeof(ToolsTestingPanel))]
    public class ToolsTestingPanelEditor : UnityEditor.Editor
    {
        private SerializedProperty _cardPrefab;
        private SerializedProperty _cardData;
        private SerializedProperty _cardParent;

        private bool _cardSetupGroupState = true;

        private void OnEnable()
        {
            _cardPrefab = serializedObject.FindProperty("_cardPrefab");
            _cardData = serializedObject.FindProperty("_cardData");
            _cardParent = serializedObject.FindProperty("_cardParent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _cardSetupGroupState = EditorGUILayout.BeginFoldoutHeaderGroup(_cardSetupGroupState, "Card Setup");

            if(_cardSetupGroupState)
            {
                EditorGUILayout.PropertyField(_cardPrefab);
                EditorGUILayout.PropertyField(_cardData);
                EditorGUILayout.PropertyField(_cardParent);

                if (_cardPrefab.objectReferenceValue != null && _cardData.objectReferenceValue != null && _cardParent.objectReferenceValue != null && EditorApplication.isPlaying)
                {
                    if(GUILayout.Button("Spawn Card"))
                    {
                        ToolsTestingPanel toolsTestingPanel = (ToolsTestingPanel)target;

                        toolsTestingPanel.CreateCard();
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
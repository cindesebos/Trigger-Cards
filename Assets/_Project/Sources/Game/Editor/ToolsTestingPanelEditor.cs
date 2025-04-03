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
        private SerializedProperty _enemyPrefab;
        private SerializedProperty _enemySpawnPoint;

        private bool _cardSetupGroupState = true;
        private bool _enemySetupGroupState = true;

        private void OnEnable()
        {
            _cardPrefab = serializedObject.FindProperty("_cardPrefab");
            _cardData = serializedObject.FindProperty("_cardData");
            _cardParent = serializedObject.FindProperty("_cardParent");
            _enemyPrefab = serializedObject.FindProperty("_enemyPrefab");
            _enemySpawnPoint = serializedObject.FindProperty("_enemySpawnPoint");
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

            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();

            _enemySetupGroupState = EditorGUILayout.BeginFoldoutHeaderGroup(_enemySetupGroupState, "Enemy Setup");

            if(_enemySetupGroupState)
            {
                EditorGUILayout.PropertyField(_enemyPrefab);
                EditorGUILayout.PropertyField(_enemySpawnPoint);

                if(EditorApplication.isPlaying && _enemyPrefab.objectReferenceValue != null && _enemySpawnPoint.objectReferenceValue != null)
                {
                    if(GUILayout.Button("Spawn Enemy"))
                    {
                        ToolsTestingPanel toolsTestingPanel = (ToolsTestingPanel)target;

                        toolsTestingPanel.SpawnEnemy();
                    }
                }
            }

            if(EditorApplication.isPlaying)
            {
                if(GUILayout.Button("Show Card Selection Panel"))
                {
                    ToolsTestingPanel toolsTestingPanel = (ToolsTestingPanel)target;
    
                    toolsTestingPanel.ShowCardSelectionPanel();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
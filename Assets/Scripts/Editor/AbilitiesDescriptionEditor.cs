using GameCore.Abilities;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AbilityDescription))]
    [CanEditMultipleObjects]
    public class AbilitiesDescriptionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SciptableObjectInspector();
        }
    
        private void SciptableObjectInspector()
        {
            var abilityIcone = serializedObject.FindProperty("_abilitySprite");
            var abilityName = serializedObject.FindProperty("_abilityName");
            var description = serializedObject.FindProperty("_abilityDescription");

            EditorGUILayout.LabelField("Ability icone:");
            abilityIcone.objectReferenceValue = EditorGUILayout.ObjectField(abilityIcone.objectReferenceValue, typeof(Sprite),
                false, GUILayout.Width(64), GUILayout.Height(64));

            EditorGUILayout.LabelField("Ability name:");
            abilityName.stringValue =
                EditorGUILayout.TextField(abilityName.stringValue, GUILayout.Width(250), GUILayout.Height(20));

            EditorGUILayout.LabelField("Ability description:");
            description.stringValue =
                EditorGUILayout.TextArea(description.stringValue, GUILayout.Width(250), GUILayout.Height(50));
            serializedObject.ApplyModifiedProperties();
        }
    }
}


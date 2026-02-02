using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace IndiePixel
{
    [CustomEditor(typeof(IP_Keyboard_Input))]
    public class IP_KeyboardHeli_InputEditor : Editor
    {
        #region Variables
        IP_Keyboard_Input targetInput;
        #endregion


        #region Builtin Methods
        private void OnEnable()
        {
            targetInput = (IP_Keyboard_Input)target;

        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            DrawDebugUI();
            Repaint();
        }

        #endregion

        #region Custom Methods
        void DrawDebugUI() {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUI.indentLevel++;
            //EditorGUILayout.LabelField("Debug Info", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Throttle : " + targetInput.ThrottleInput.ToString("0.00"),EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Collective : " + targetInput.CollectiveInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Cyclic : " + targetInput.CyclicInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pedal : " + targetInput.PedalInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LookAt2Dv1))]
public class LookAt2Dv1Editor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nThe gameobject that has this component attached will instantly rotate" +
            " to make its x or y axis look towards the assigned target or towards mouse world" +
            " position if a public bool is checked. The direction can beinverted by checking a bool." +
            " Also there is an option to disable local update if a linked control is needed.\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}

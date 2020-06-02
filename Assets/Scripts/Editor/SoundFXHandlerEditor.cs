using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundFXHandler))]
public class SoundFXHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nTo easily play soundFXs on awake or to link to other classes to quickly" +
            " play sfx using the provided public variables\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}

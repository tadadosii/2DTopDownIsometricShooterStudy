using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nTo easily access to a bunch of useful Audio actions and simply have 2 audiosources " +
            "on the scene one for the music and the other one for the sound fxs\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}

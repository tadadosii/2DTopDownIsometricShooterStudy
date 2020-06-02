using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MusicHandler))]
public class MusicHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\n To quickly add to a gameobject and simply set it to play a clip on awake\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
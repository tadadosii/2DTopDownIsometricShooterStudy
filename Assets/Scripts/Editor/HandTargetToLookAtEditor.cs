using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandTargetToLookAt))]
public class HandTargetToLookAtEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nUses MouseInput direction player -> mouse world pos to move the local position of a " +
            "gameobject that's used as a look at target by the hands positioned in the weapon handguard\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
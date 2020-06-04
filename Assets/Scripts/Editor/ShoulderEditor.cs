using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shoulder))]
public class ShoulderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nThis class uses MouseInput direction player -> mouse world " +
            "pos and other variables like rate, min offset angle and max offset angle " +
            "to determine an offset angle that will be added to the shoulder as an extra " +
            "rotation. This is an important part of the behaviour that controls the " +
            "rotation of the shoulders, with the correct values both shoulders match " +
            "their rotation in relation to the weapon that's being held by the character.\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}

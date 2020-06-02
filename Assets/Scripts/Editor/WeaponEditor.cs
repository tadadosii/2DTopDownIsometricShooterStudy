using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 14, wordWrap = true, fontStyle = FontStyle.Bold };
        GUILayout.Label("\nSpawn projectiles, Fire Basic Projectiles, Fire Charged Projectiles and " +
            "a timer to stop the player from shooting like crazy (fireRate)\n", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}

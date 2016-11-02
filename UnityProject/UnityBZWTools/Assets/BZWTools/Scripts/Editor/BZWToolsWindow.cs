using UnityEngine;
using UnityEditor;
using System.Collections;

using BZFlag.IO;

public class BZWToolsWindow : EditorWindow
{
    // Add menu item named "My Window" to the Window menu
    [MenuItem("BZWTools/Windows/Show BZWTools")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(BZWToolsWindow));
    }

    public void OnGUI()
    {
        GUILayout.Label("Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Box"))
            AddBox();
        if (GUILayout.Button("Add Pyramid"))
            AddBox();
        if (GUILayout.Button("Add Teleporter"))
            AddBox();
        if (GUILayout.Button("Add Link"))
            AddBox();
    }

    public static GameObject GetRoot()
    {
        BZWWorld root = GameObject.FindObjectOfType<BZWWorld>();
        if (root != null)
            return root.gameObject;

        return FromBZW.CreateNewBZWRoot(new Map());
    }

    public void AddBox()
    {

    }
}

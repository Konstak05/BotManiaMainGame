using UnityEditor;
using UnityEngine;

public class Tools : EditorWindow
{
    [MenuItem("Project Tools/Tools")]
    static void GetMe() => GetWindow<Tools>();

    void OnGUI()
    {
        if (GUILayout.Button("Remove all coliders"))
        {
            Collider[] cols = FindObjectsOfType<Collider>();
            foreach (Collider col in cols)
            {
                DestroyImmediate(col);
            }
        }
        if (GUILayout.Button("Remove all rigidbody"))
        {
            Rigidbody[] rbs = FindObjectsOfType<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                DestroyImmediate(rb);
            }
        }
    }
}

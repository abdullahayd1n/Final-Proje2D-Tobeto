<<<<<<< Updated upstream
#if UNITY_EDITOR
=======
#if UN�TY_EDITOR
>>>>>>> Stashed changes

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Renderer))] // Renderer s�n�f�n� hedef al�yoruz.

public class RendererSortingLayersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Renderer renderer = target as Renderer;

        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();

        string name = EditorGUILayout.TextField("Sorting Layer Name", renderer.sortingLayerName);

        if (EditorGUI.EndChangeCheck())
        {
            renderer.sortingLayerName = name;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();

        int order = EditorGUILayout.IntField("Sorting Order", renderer.sortingOrder);

        if (EditorGUI.EndChangeCheck())
        {
            renderer.sortingOrder = order;
        }

<<<<<<< Updated upstream
		EditorGUILayout.EndHorizontal();
	}
=======
        EditorGUILayout.EndHorizontal();
    }
>>>>>>> Stashed changes
}
#endif
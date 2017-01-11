using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PGDatabaseManager))]
public class PGDatabaseManagerEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		PGDatabaseManager manager = (PGDatabaseManager)target;

		if (GUILayout.Button ("LoadFromFile")) {
			manager.Load();
		}


		if (GUILayout.Button ("SaveToFile")) {
			manager.Save();
		}
	}

}

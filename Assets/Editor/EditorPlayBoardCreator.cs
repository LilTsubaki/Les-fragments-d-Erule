using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayBoardCreator))]
public class EditorPlayBoardCreator : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		PlayBoardCreator creator = (PlayBoardCreator)target;

		if (GUILayout.Button ("Create Hexagon")) {
			creator.BuildHexagon ();
		}

		if (GUILayout.Button ("Remove Hexagon")) {
			creator.RemoveHexagon ();
		}

		if (GUILayout.Button ("Create Obstacle")) {
			creator.BuildObstacle ();
		}
		if (GUILayout.Button ("Remove Obstacle")) {
			creator.RemoveObstacle ();
		}

	}

}


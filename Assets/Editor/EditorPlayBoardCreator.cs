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

        if (GUILayout.Button("Create Spawn Hexagon"))
        {
            creator.BuildSpawnHexagon();
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

		if (GUILayout.Button ("Create Underground")) {
			creator.BuildUnderground ();
		}
		if (GUILayout.Button ("Remove Underground")) {
			creator.RemoveUnderground ();
		}

        if (GUILayout.Button("Save Board"))
        {
            creator.SaveBoard();
            
        }
        if (GUILayout.Button("Load Board"))
        {
            creator.LoadBoard();
         
        }
    }

}


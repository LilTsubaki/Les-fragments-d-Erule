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

        // Boosts
        /*if (GUILayout.Button("Apply boost Air"))
        {
            creator.ApplyBoost(Hexagon.Boost.Air);
        }

        if (GUILayout.Button("Apply boost Earth"))
        {
            creator.ApplyBoost(Hexagon.Boost.Earth);
        }

        if (GUILayout.Button("Apply boost Fire"))
        {
            creator.ApplyBoost(Hexagon.Boost.Fire);
        }

        if (GUILayout.Button("Apply boost Metal"))
        {
            creator.ApplyBoost(Hexagon.Boost.Metal);
        }

        if (GUILayout.Button("Apply boost Water"))
        {
            creator.ApplyBoost(Hexagon.Boost.Water);
        }

        if (GUILayout.Button("Apply boost Wood"))
        {
            creator.ApplyBoost(Hexagon.Boost.Wood);
        }*/

        if(GUILayout.Button("Remove boost"))
        {
            creator.RemoveBoost();
        }

        if (GUILayout.Button ("Create Underground")) {
			creator.BuildUnderground ();
		}

		if (GUILayout.Button ("Remove Underground")) {
			creator.RemoveUnderground ();
		}

        if (GUILayout.Button("Create Shard"))
        {
            creator.BuildShard();
        }

        if (GUILayout.Button("Remove Shard"))
        {
            creator.RemoveShard();
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


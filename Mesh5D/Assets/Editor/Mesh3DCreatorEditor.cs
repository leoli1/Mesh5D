/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mesh3DCreator))]
public class Mesh3DCreatorEditor : Editor {

	private Mesh3DCreator instance;

	void OnEnable(){
		instance = target as Mesh3DCreator;
	}
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();
		instance.EditorUpdate ();
	}

	void OnSceneGUI(){
		Handles.BeginGUI();

		GUILayout.BeginArea (new Rect (5, 5, 500, 500));

		GUILayout.BeginHorizontal();
		GUILayout.Label("XY Rotation:");
		instance.mesh4D.rotation.XYRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.XYRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("XZ Rotation:");
		instance.mesh4D.rotation.XZRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.XZRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("XW Rotation:");
		instance.mesh4D.rotation.XWRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.XWRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("YZ Rotation:");
		instance.mesh4D.rotation.YZRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.YZRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("YW Rotation:");
		instance.mesh4D.rotation.YWRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.YWRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("ZW Rotation:");
		instance.mesh4D.rotation.ZWRotation = GUILayout.HorizontalSlider (instance.mesh4D.rotation.ZWRotation, -7, 7);
		GUILayout.EndHorizontal();

		GUILayout.EndArea();

		Handles.EndGUI();
	}
}
*/
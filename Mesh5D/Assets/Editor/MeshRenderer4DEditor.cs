using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshRenderer4D))]
public class MeshRenderer4DEditor : Editor {

	private MeshRenderer4D instance;

	void OnEnable(){
		instance = target as MeshRenderer4D;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();
		if (GUILayout.Button ("Create Mesh")) {
			instance.CreateMesh ();
		}
		if (instance.autoUpdateProjectedMesh) {
			instance.UpdateMesh ();
		}
	}
}

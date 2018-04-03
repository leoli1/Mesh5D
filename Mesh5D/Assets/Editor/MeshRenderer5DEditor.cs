using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshRenderer5D))]
public class MeshRenderer5DEditor : Editor {

	private MeshRenderer5D instance;

	void OnEnable(){
		instance = target as MeshRenderer5D;
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

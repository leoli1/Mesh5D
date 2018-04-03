using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Camera5D))]
public class Camer5DEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		foreach (MeshRenderer5D r in GameObject.FindObjectsOfType<MeshRenderer5D>()) {
			r.UpdateMesh ();
		}
	}
}

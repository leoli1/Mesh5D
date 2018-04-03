using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Camera4D))]
public class Camer4DEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		foreach (MeshRenderer4D r in GameObject.FindObjectsOfType<MeshRenderer4D>()) {
			r.UpdateMesh ();
		}
	}
}

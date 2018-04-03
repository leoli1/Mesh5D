using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform5D))]
public class Transform5DEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Reset")) {
			(target as Transform5D).Reset ();
		}
	}
}

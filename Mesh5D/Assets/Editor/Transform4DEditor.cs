using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform4D))]
public class Transform4DEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Reset")) {
			(target as Transform4D).Reset ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotationAnimation5D))]
public class RotationAnimation5DEditor : Editor {

	private RotationAnimation5D instance;

	void OnEnable(){
		instance = target as RotationAnimation5D;
	}
	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Start Animation")) {
			instance.StartAnimation ();
		}
		if (GUILayout.Button ("Stop Animation")) {
			instance.StopAnimation ();
		}
	}
}

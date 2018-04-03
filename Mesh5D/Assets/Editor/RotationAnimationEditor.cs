using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotationAnimation))]
public class RotationAnimationEditor : Editor {

	private RotationAnimation instance;

	void OnEnable(){
		instance = target as RotationAnimation;
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

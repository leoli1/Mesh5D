using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HyperPrism))]
public class HyperPrismEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Generate")) {
			(target as HyperPrism).Generate ();
		}
	}
}

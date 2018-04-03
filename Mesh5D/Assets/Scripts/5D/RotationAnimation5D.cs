using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("5D Objects/RotationAnimation5D")]
public class RotationAnimation5D : MonoBehaviour {

	public Transform5D targetObject;

	public Rotation5D rotationDirection;

	public float speed = 1;

	[HideInInspector]
	public bool isAnimating = false;

	public void StartAnimation(){
		if (targetObject == null) {
			targetObject = GetComponent<Transform5D> ();
			if (targetObject == null)
				return;
		}
		StartCoroutine ("Animate");
		isAnimating = true;
	}
	public void StopAnimation(){
		isAnimating = false;
		StopCoroutine ("Animate");
	}

	IEnumerator Animate(){
		while (true) {
			targetObject.rotation += rotationDirection * speed * Time.deltaTime;
			yield return null;
		}
	}
}

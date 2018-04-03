using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("4D Objects/RotationAnimation")]
public class RotationAnimation : MonoBehaviour {

	public Transform4D targetObject;

	public Rotation4D rotationDirection;

	public float speed = 1;

	[HideInInspector]
	public bool isAnimating = false;

	private Coroutine animationCoroutine;

	public void StartAnimation(){
		if (targetObject == null) {
			targetObject = GetComponent<Transform4D> ();
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
			if (Mathf.Abs (targetObject.rotation.XYRotation - 2 * Mathf.PI) < 0.05f)
				break;
			yield return null;
		}
	}
}

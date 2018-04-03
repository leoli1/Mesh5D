using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("5D Objects/MeshRenderer5D")]
[RequireComponent(typeof(Transform5D))]
public class MeshRenderer5D : MonoBehaviour {

	public Transform5D transform5D;

	public Mesh5D mesh5D;

	public bool autoUpdateProjectedMesh = true;

	private bool meshCreated = false;

	private GameObject mesh4D;

	public void CreateMesh(){
		if (mesh5D == null)
			return;

		if (transform5D == null) {
			transform5D = GetComponent<Transform5D> ();
			if (transform5D == null)
				return;
		}
		foreach (Transform trans in transform.GetComponentsInChildren<Transform>()) {
			if (trans != transform && trans != null)
				DestroyImmediate (trans.gameObject);
		}
		mesh4D = Transform4D.CreateEmpty ();
		mesh4D.transform.SetParent (transform);

		MeshRenderer4D renderer = mesh4D.GetComponent<MeshRenderer4D> ();

		Vector4[] projectedVertices = getProjectedVertices ();
		renderer.mesh4D.vertices = projectedVertices;
		renderer.mesh4D.edges = mesh5D.edges;

		renderer.CreateMesh ();
		meshCreated = true;
	}

	public void UpdateMesh(){
		if (mesh5D == null)
			return;

		if (mesh4D == null) {
			CreateMesh ();
			return;
		}

		Vector4[] projectedVertices = getProjectedVertices ();

		MeshRenderer4D renderer = mesh4D.GetComponent<MeshRenderer4D> ();
		renderer.mesh4D.vertices = projectedVertices;

		renderer.UpdateMesh ();
	}

	private Vector4[] getProjectedVertices(){
		Vector4[] projectedVertices = new Vector4[mesh5D.vertices.Length];
		for (int i = 0; i < projectedVertices.Length; i++) {
			projectedVertices [i] = Camera5D.mainCamera.GetProjectedPoint (transform5D.getWorldPosition (mesh5D.vertices[i]));
		}
		return projectedVertices;
	}

	void Update(){
		UpdateMesh ();
	}
}

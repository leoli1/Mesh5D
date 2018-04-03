using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("4D Objects/MeshRenderer4D")]
[RequireComponent(typeof(Transform4D))]
public class MeshRenderer4D : MonoBehaviour {

	public Transform4D transform4D;

	public Mesh4D mesh4D;

	public bool autoUpdateProjectedMesh = true;

	public GameObject vertexObject;
	public GameObject edgeObject;

	private bool meshCreated = false;

	private List<GameObject> vertexObjects;
	private List<GameObject> edgeObjects;

	void Enable(){
		transform4D = GetComponent<Transform4D> ();
		CreateMesh ();
	}

	public void CreateMesh(){
		if (mesh4D == null)
			return;
		if (vertexObject == null)
			vertexObject = Camera4D.mainCamera.standardVertexGameObject;
		if (edgeObject == null)
			edgeObject = Camera4D.mainCamera.standardEdgeGameObject;
		
		if (transform4D == null) {
			transform4D = GetComponent<Transform4D> ();
			if (transform4D == null)
				return;
		}
		foreach (Transform trans in transform.GetComponentsInChildren<Transform>()) {
			if (trans != transform)
				DestroyImmediate (trans.gameObject);
		}

		vertexObjects = new List<GameObject> ();
		edgeObjects = new List<GameObject> ();

		Vector3[] projectedVertices = getProjectedVertices ();
		foreach (Vector3 vertex in projectedVertices) {
			GameObject v = GameObject.Instantiate(vertexObject, transform);
			v.transform.localPosition = vertex;
			vertexObjects.Add (v);
		}

		for (int i = 0; i<mesh4D.edges.Length; i+= 2){
			GameObject edge = GameObject.Instantiate (edgeObject, transform);
			LineRenderer lr = edge.GetComponent<LineRenderer> ();
			lr.startColor = Color.blue;
			lr.endColor = Color.blue;
			lr.SetPositions (new Vector3[] {
				projectedVertices [mesh4D.edges [i]],
				projectedVertices [mesh4D.edges [i + 1]],
			});
			edgeObjects.Add (edge);
		}
		meshCreated = true;
	}
	public void UpdateMesh(){
		if (mesh4D == null)
			return;
		Vector3[] projectedVertices = getProjectedVertices ();
		if (vertexObjects == null || projectedVertices.Length != vertexObjects.Count || meshCreated == false) {
			CreateMesh ();
			return;
		}

		for (int i = 0; i < projectedVertices.Length; i++) {
			if (vertexObjects [i] == null) {
				CreateMesh ();
				return;
			}
			vertexObjects [i].transform.localPosition = projectedVertices [i];
		}

		for (int i = 0; i < mesh4D.edges.Length; i += 2) {
			LineRenderer lr = edgeObjects [i / 2].GetComponent<LineRenderer>();
			lr.SetPositions (new Vector3[] {
				projectedVertices [mesh4D.edges [i]],
				projectedVertices [mesh4D.edges [i + 1]]
			});
		}
	}

	private Vector3[] getProjectedVertices(){
		Vector3[] projectedVertices = new Vector3[mesh4D.vertices.Length];
		for (int i = 0; i < projectedVertices.Length; i++) {
			projectedVertices [i] = Camera4D.mainCamera.GetProjectedPoint (transform4D.getWorldPosition (mesh4D.vertices[i]));
			//print (mesh4D.vertices [i] + " : " + projectedVertices [i]);
		}
		return projectedVertices;
	}
	void Update(){
		UpdateMesh ();
	}
}

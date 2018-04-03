using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ProjectionType{
	Orthographic,
	Perspective
}

[ExecuteInEditMode]
public class Camera4D : MonoBehaviour {
	private static Camera4D mainCameraTemp;
	public static Camera4D mainCamera{
		get{ 
			if (mainCameraTemp != null)
				return mainCameraTemp;
			Camera4D cam = GameObject.FindObjectOfType<Camera4D> ();
			mainCameraTemp = cam;
			return cam;
		}
	}

	public Vector4 projectionPoint = new Vector4(0,0,0,10);
	public float projectionSpacePointW = 5;

	public ProjectionType projectionType = ProjectionType.Orthographic;

	public GameObject standardVertexGameObject;
	public GameObject standardEdgeGameObject;

	void Start(){
		mainCameraTemp = this;
	}

	public Vector3 GetProjectedPoint(Vector4 point){
		switch (projectionType) {
		case ProjectionType.Orthographic:
			return OrthographicProjection (point);
		case ProjectionType.Perspective:
			return PerspectiveProjection (point);
		}
		return Vector3.zero;
	}

	Vector3 OrthographicProjection(Vector4 point){
		if (point [3] == projectionPoint [3]) {
			return new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}
		Vector4 S = point;
		Vector4 D = new Vector4 (0, 0, 0, projectionPoint.w - S.w);
		Vector4 PS = new Vector4 (0, 0, 0, projectionSpacePointW);

		// S[W] + k * D[W] = PS[W]
		float k = (PS[3] - S [3]) / D [3];

		Vector4 newPoint = S+k*D;

		return new Vector3(newPoint.x, newPoint.y, newPoint.z);
	}

	Vector3 PerspectiveProjection(Vector4 point){
		if (point [3] == projectionPoint [3]) {
			return new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}
		Vector4 S = point;
		Vector4 D = projectionPoint- S;
		Vector4 PS = new Vector4 (0, 0, 0, projectionSpacePointW);

		// S[W] + k * D[W] = PS[W]
		float k = (PS[3] - S [3]) / D [3];
		Vector4 newPoint = S + k * D;
		return new Vector3 (newPoint.x, newPoint.y, newPoint.z);
	}
	#if UNITY_EDITOR
	[MenuItem("GameObject/Camera4D", false, 10)]
	public static void CreateCamera(){
		GameObject go = new GameObject ("Camera4D");
		go.AddComponent<Camera4D> ();
	}
	#endif
}


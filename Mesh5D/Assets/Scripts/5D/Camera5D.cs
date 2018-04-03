using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Camera5D : MonoBehaviour {
	private static Camera5D mainCameraTemp;
	public static Camera5D mainCamera{
		get{ 
			if (mainCameraTemp != null)
				return mainCameraTemp;
			Camera5D cam = GameObject.FindObjectOfType<Camera5D> ();
			mainCameraTemp = cam;
			return cam;
		}
	}

	public Vector5 projectionPoint = new Vector5(new float[]{0,0,0,0,10});
	public float projectionSpacePointV = 5;

	public ProjectionType projectionType = ProjectionType.Perspective;

	void Start () {
		mainCameraTemp = this;
	}

	public Vector4 GetProjectedPoint(Vector5 point){
		switch (projectionType) {
		case ProjectionType.Orthographic:
			return OrthographicProjection (point);
		case ProjectionType.Perspective:
			return PerspectiveProjection (point);
		default:
			return Vector4.zero;
		}
	}
	Vector4 OrthographicProjection(Vector5 point){
		if (point [4] == projectionPoint [4]) {
			return new Vector4 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}
		Vector5 S = point;
		Vector5 D = new Vector5 (new float[]{ 0, 0, 0, 0, projectionPoint.v - S.v });
		Vector5 PS = new Vector5 (new float[]{0, 0, 0, 0, projectionSpacePointV});

		// S[W] + k * D[W] = PS[W]
		float k = (PS[4] - S [4]) / D [4];
		Vector5 newPoint = S + k * D;
		return new Vector4 (newPoint.x, newPoint.y, newPoint.z, newPoint.w);
	}
	Vector4 PerspectiveProjection(Vector5 point){
		if (point [4] == projectionPoint [4]) {
			return new Vector4 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}
		Vector5 S = point;
		Vector5 D = projectionPoint- S;
		Vector5 PS = new Vector5 (new float[]{0, 0, 0, 0, projectionSpacePointV});

		// S[W] + k * D[W] = PS[W]
		float k = (PS[4] - S [4]) / D [4];
		Vector5 newPoint = S + k * D;
		return new Vector4 (newPoint.x, newPoint.y, newPoint.z, newPoint.w);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#if UNITY_EDITOR
	[MenuItem("GameObject/Camera5D", false, 10)]
	public static void CreateCamera(){
		GameObject go = new GameObject ("Camera5D");
		go.AddComponent<Camera5D> ();
	}
	#endif
}

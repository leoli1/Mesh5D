using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;

[System.Serializable]
public struct Rotation4D{

	public float XYRotation;
	public float XZRotation;
	public float XWRotation;
	public float YZRotation;
	public float YWRotation;
	public float ZWRotation;

	public static int[] getRotationPlaneIndices(RotationPlane plane){
		switch (plane) {
		case RotationPlane.XY:
			return new int[]{ 0, 1 };
		case RotationPlane.XZ:
			return new int[]{ 0, 2 };
		case RotationPlane.XW:
			return new int[]{ 0, 3 };
		case RotationPlane.YZ:
			return new int[]{ 1, 2 };
		case RotationPlane.YW:
			return new int[]{ 1, 3 };
		case RotationPlane.ZW:
			return new int[]{ 2, 3 };
		default:
			return null;
		}
	}

	public static Rotation4D operator +(Rotation4D left, Rotation4D right){
		return new Rotation4D {
			XYRotation = left.XYRotation + right.XYRotation,
			XZRotation = left.XZRotation + right.XZRotation,
			XWRotation = left.XWRotation + right.XWRotation,
			YZRotation = left.YZRotation + right.YZRotation,
			YWRotation = left.YWRotation + right.YWRotation,
			ZWRotation = left.ZWRotation + right.ZWRotation
		};
	}
	public static Rotation4D operator *(Rotation4D left, float right){
		return new Rotation4D {
			XYRotation = left.XYRotation*right,
			XZRotation = left.XZRotation*right,
			XWRotation = left.XWRotation*right,
			YZRotation = left.YZRotation*right,
			YWRotation = left.YWRotation*right,
			ZWRotation = left.ZWRotation*right
		};
	}
}

[ExecuteInEditMode]
[AddComponentMenu("4D Objects/Transform4D")]
public class Transform4D : MonoBehaviour {

	public Vector4 position;

	public Rotation4D rotation;

	[Tooltip("Local Scale")]
	public Vector4 scale = Vector4.one;

	public Vector4 getWorldPosition(Vector4 localPosition){
		Vector4 worldPos = new Vector4 (localPosition.x * scale.x, localPosition.y * scale.y, localPosition.z * scale.z, localPosition.w * scale.w);
		worldPos = rotatePointAroundPlane (worldPos, rotation.XYRotation, RotationPlane.XY);
		worldPos = rotatePointAroundPlane (worldPos, rotation.XZRotation, RotationPlane.XZ);
		worldPos = rotatePointAroundPlane (worldPos, rotation.XWRotation, RotationPlane.XW);
		worldPos = rotatePointAroundPlane (worldPos, rotation.YZRotation, RotationPlane.YZ);
		worldPos = rotatePointAroundPlane (worldPos, rotation.YWRotation, RotationPlane.YW);
		worldPos = rotatePointAroundPlane (worldPos, rotation.ZWRotation, RotationPlane.ZW);
		return worldPos+position;
	}

	public Vector4 rotatePointAroundPlane(Vector4 point, float angle, RotationPlane plane){
		int[] planeCoordinates = Rotation4D.getRotationPlaneIndices(plane);
		int[] freeCoordinates = new int[2]; 

		int a = 0;
		for (int i = 0; i < 4; i++) {
			if (!planeCoordinates.Contains (i)) {
				freeCoordinates [a] = i;
				a++;
			}
		}
		float X = point[freeCoordinates [0]];
		float Y = point[freeCoordinates [1]];

		float newX = X * Mathf.Cos (angle) - Y * Mathf.Sin (angle);
		float newY = X * Mathf.Sin (angle) + Y * Mathf.Cos (angle);

		point [freeCoordinates [0]] = newX;
		point [freeCoordinates [1]] = newY;

		return point;
	}

	public void Reset(){
		position = new Vector4 ();
		rotation = new Rotation4D ();
		scale = Vector4.one;
	}

	#if UNITY_EDITOR
	[MenuItem("GameObject/4D Object/Empty", false, 0)]
	public static GameObject CreateEmpty(){
		GameObject go = new GameObject ();
		go.name = "4D Object";
		go.AddComponent<Transform4D> ();
		go.AddComponent<MeshRenderer4D> ();
		go.GetComponent<MeshRenderer4D> ().autoUpdateProjectedMesh = true;
		go.GetComponent<MeshRenderer4D> ().mesh4D = new Mesh4D ();
		Selection.activeGameObject = go;
		return go;
	}
	[MenuItem("GameObject/4D Object/HyperCube", false, 0)]
	public static void CreateHyperCube(){
		GameObject go = CreateEmpty ();
		go.name = "HyperCube";
		go.GetComponent<MeshRenderer4D> ().mesh4D = Mesh4D.HyperCube;
		go.GetComponent<MeshRenderer4D> ().CreateMesh ();
	}
	[MenuItem("GameObject/4D Object/Simplex4", false, 0)]
	public static void CreateSimplex4(){
		GameObject go = CreateEmpty ();
		go.name = "Simplex4";
		go.GetComponent<MeshRenderer4D> ().mesh4D = Mesh4D.Simplex4;
		go.GetComponent<MeshRenderer4D> ().CreateMesh ();
	}
	[MenuItem("GameObject/4D Object/HyperPrism", false, 0)]
	public static void CreateHyperPrism(){
		GameObject go = CreateEmpty ();
		go.name = "HyperPrism";
		go.AddComponent<HyperPrism> ();
		go.GetComponent<MeshRenderer4D> ().CreateMesh ();
	}
	#endif
}

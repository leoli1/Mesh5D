using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;

[System.Serializable]
public struct Rotation5D{
	public float XYZRotation;
	public float XYWRotation;
	public float XYVRotation;
	public float XZWRotation;
	public float XZVRotation;
	public float XWVRotation;

	public static int[] getRotationSpaceIndices(RotationSpace5D space){
		switch (space) {
		case RotationSpace5D.XYZ:
			return new int[]{ 0, 1, 2 };
		case RotationSpace5D.XYW:
			return new int[]{ 0, 1, 3 };
		case RotationSpace5D.XYV:
			return new int[]{ 0, 1, 4 };
		case RotationSpace5D.XZW:
			return new int[]{ 0, 2, 3 };
		case RotationSpace5D.XZV:
			return new int[]{ 0, 2, 4 };
		case RotationSpace5D.XWV:
			return new int[]{ 0, 3, 4 };
		default:
			return null;
		}
	}

	public static Rotation5D operator +(Rotation5D left, Rotation5D right){
		return new Rotation5D {
			XYZRotation = left.XYZRotation + right.XYZRotation,
			XYWRotation = left.XYWRotation + right.XYWRotation,
			XYVRotation = left.XYVRotation + right.XYVRotation,
			XZWRotation = left.XZWRotation + right.XZWRotation,
			XZVRotation = left.XZVRotation + right.XZVRotation,
			XWVRotation = left.XWVRotation + right.XWVRotation
		};
	}
	public static Rotation5D operator *(Rotation5D left, float right){
		return new Rotation5D {
			XYZRotation = left.XYZRotation * right,
			XYWRotation = left.XYWRotation * right,
			XYVRotation = left.XYVRotation * right,
			XZWRotation = left.XZWRotation * right,
			XZVRotation = left.XZVRotation * right,
			XWVRotation = left.XWVRotation * right
		};
	}
}

[ExecuteInEditMode]
[AddComponentMenu("5D Objects/Transform5D")]
public class Transform5D : MonoBehaviour {

	public Vector5 position = Vector5.zero;

	public Rotation5D rotation;

	[Tooltip("Local Scale")]
	public Vector5 scale = Vector5.one;

	public Vector5 getWorldPosition(Vector5 localPosition){
		Vector5 worldPos = localPosition * scale;
		worldPos = rotatePointAroundSpace (worldPos, rotation.XYZRotation, RotationSpace5D.XYZ);
		worldPos = rotatePointAroundSpace (worldPos, rotation.XYWRotation, RotationSpace5D.XYW);
		worldPos = rotatePointAroundSpace (worldPos, rotation.XYVRotation, RotationSpace5D.XYV);
		worldPos = rotatePointAroundSpace (worldPos, rotation.XZWRotation, RotationSpace5D.XZW);
		worldPos = rotatePointAroundSpace (worldPos, rotation.XZVRotation, RotationSpace5D.XZV);
		worldPos = rotatePointAroundSpace (worldPos, rotation.XWVRotation, RotationSpace5D.XWV);
		return worldPos + position;
	}
	
	public Vector5 rotatePointAroundSpace(Vector5 point, float angle, RotationSpace5D space){
		int[] spaceCoordinates = Rotation5D.getRotationSpaceIndices(space);
		int[] freeCoordinates = new int[2]; 

		int a = 0;
		for (int i = 0; i < 5; i++) {
			if (!spaceCoordinates.Contains (i)) {
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
		position = Vector5.zero;
		rotation = new Rotation5D ();
		scale = Vector5.one;
	}
	#if UNITY_EDITOR
	[MenuItem("GameObject/5D Object/Empty", false, 0)]
	public static GameObject CreateEmpty(){
		GameObject go = new GameObject ();
		go.name = "5D Object";
		go.AddComponent<Transform5D> ();
		go.AddComponent<MeshRenderer5D> ();
		go.GetComponent<MeshRenderer5D> ().autoUpdateProjectedMesh = true;

		return go;
	}
	[MenuItem("GameObject/5D Object/HyperCube5D", false, 0)]
	public static void CreateHyperCube(){
		GameObject go = CreateEmpty ();
		go.name = "HyperCube5D";
		go.GetComponent<MeshRenderer5D> ().mesh5D = Mesh5D.HyperCube5D;
		go.GetComponent<MeshRenderer5D> ().CreateMesh ();
	}
	#endif

}

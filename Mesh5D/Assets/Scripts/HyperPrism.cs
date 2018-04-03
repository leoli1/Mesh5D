using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer4D))]
[AddComponentMenu("4D Objects/HyperPrism")]
public class HyperPrism : MonoBehaviour {

	public PrimitiveType baseType;

	public int heightSegments = 4;
	public float height = 5;

	public void Generate(){
		GetComponent<MeshRenderer4D>().mesh4D = Mesh4D.HyperPrism (PrimitiveHelper.GetPrimitiveMesh (baseType).vertices, heightSegments, height);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationSpace5D{
	XYZ,
	XYW,
	XYV,
	XZW,
	XZV,
	XWV
}

[System.Serializable]
public class Mesh5D {
	public string name;

	public Vector5[] vertices;
	public int[] edges;


	public static Mesh5D HyperCube5D{
		get{ 
			Mesh5D mesh = new Mesh5D {
				name = "HyperCube5D"
			};
			mesh.vertices = new Vector5[32];
			int i = 0;
			foreach (Vector5 v5 in getCubeVertices(Vector5.zero, 0)) {
				mesh.vertices [i] = v5;
				i++;
			}
			for (i = 0; i < mesh.vertices.Length; i++) {
				mesh.vertices [i] = mesh.vertices [i] - 0.5f * Vector5.one;
			}
			mesh.edges = new int[(5*32)*2];

			i = 0;

			for (int v=0;v<32;v++){
				Vector5 vertex = mesh.vertices [v];
				for (int k = 0; k < 32; k++) {
					Debug.Log ((vertex - mesh.vertices [k]).Abs ());
					if ((vertex - mesh.vertices [k]).Abs () == 1) {
						mesh.edges[i] = v;
						mesh.edges[i+1] = k;
						i+=2;
					}
				}
			}
			return mesh;
		}
	}

	static IEnumerable<Vector5> getCubeVertices(Vector5 currentVertex, int depth){
		for (int i = 0; i <= 1; i++) {
			Vector5 newVertex = new Vector5(currentVertex.components);
			newVertex [depth] = i;
			if (depth == 4) {
				yield return newVertex;
			} else {
				
				foreach (Vector5 v5 in getCubeVertices (newVertex, depth + 1)) {
					yield return v5;
				}
			}
		}
	}
}

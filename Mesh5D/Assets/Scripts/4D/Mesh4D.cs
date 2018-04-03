using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
using System;

public enum PrimitiveObjects{
	HyperCube,
	HyperCubeFilled,
	HyperSphere,
	Simplex4,
	HyperCylinder,
	HyperTetraederPrism,
	ZetaFunction,
	SquareFunction,
	SinFunction
}


public enum RotationPlane{ // ROTATION AXIS not ROTATED PLANES
	XY,
	XZ,
	XW,
	YZ,
	YW,
	ZW,

}
	
[System.Serializable]

//[CreateAssetMenu(menuName="4DObjects/Mesh4D")]
public class Mesh4D{// : ScriptableObject{
	public string name;

	public Vector4[] vertices;
	public int[] edges;

	private void CreatePrimitive(PrimitiveObjects type){
		switch (type) {
		case PrimitiveObjects.HyperCube:
			//HyperCube ();
			break;
		}
	}

	/*public static Mesh4D getPrimitive(PrimitiveObjects type){
		switch (type) {
		case PrimitiveObjects.HyperCube:
			return HyperCube;
		case PrimitiveObjects.HyperCubeFilled:
			return HyperCubeFilled;
		case PrimitiveObjects.Simplex4:
			return Simplex4;
		case PrimitiveObjects.HyperSphere:
			return HyperSphere;
		case PrimitiveObjects.HyperCylinder:
			return HyperCylinder;
		case PrimitiveObjects.HyperTetraederPrism:
			return HyperTetraederPrism;
		case PrimitiveObjects.ZetaFunction:
			return ZetaFunction;
		case PrimitiveObjects.SquareFunction:
			return SquareFunction;
		case PrimitiveObjects.SinFunction:
			return SinFunction;
		default:
			return null;
		}
	}*/

	/*#if UNITY_EDITOR
	[MenuItem("Assets/Create/4DObjects/HyperCube")]
	public static void CreateHyperCube(){
		Mesh4D mesh = ScriptableObjectUtility.CreateAsset<Mesh4D> ("HyperCube");
		mesh.CreatePrimitive (PrimitiveObjects.HyperCube);
	}
	#endif
*/
	public static Mesh4D HyperCube{
		get{ 
			Mesh4D mesh = new Mesh4D {
				name = "Hypercube",
				vertices = new Vector4[] {
					new Vector4 (0, 0, 0, 0),//0

					new Vector4 (1, 0, 0, 0),//1
					new Vector4 (0, 1, 0, 0),//2
					new Vector4 (0, 0, 1, 0),//3
					new Vector4 (0, 0, 0, 1),//4

					new Vector4 (1, 1, 0, 0),//5
					new Vector4 (1, 0, 1, 0),//6
					new Vector4 (1, 0, 0, 1),//7
					new Vector4 (0, 1, 1, 0),//8
					new Vector4 (0, 1, 0, 1),//9
					new Vector4 (0, 0, 1, 1),//10

					new Vector4 (1, 1, 1, 0),//11
					new Vector4 (1, 1, 0, 1),//12
					new Vector4 (1, 0, 1, 1),//13
					new Vector4 (0, 1, 1, 1),//14

					new Vector4 (1, 1, 1, 1)//15
				},
				edges = new int[] {
					//little cube
					0, 1,
					0, 2,
					0, 3,

					1, 6,
					2, 8,
					3, 8,

					6, 11,
					8, 11,

					6, 3,

					5, 1,
					5, 2,
					5, 11,

					// big cube
					4, 7,
					4, 9,
					4, 10,
					7, 12,
					9, 12,
					14, 9,
					14, 10,
					15, 12,
					15, 13,
					15, 14,
					13, 7,
					13, 10,

					// big-little-cube connections

					1, 7,
					2, 9,
					3, 10,
					0, 4,
					11, 15,
					5, 12,
					6, 13,
					8, 14
				}
			};
			for (int i = 0; i < mesh.vertices.Length; i++) {
				mesh.vertices [i] = mesh.vertices [i] - 0.5f * Vector4.one;
			}
			mesh.Apply ();
			return mesh;
		}
	}

	public static Mesh4D HyperCubeFilled {
		get { 
			int xs = 8;
			int ys = 8;
			int zs = 8;
			int ws = 8;

			Vector4[] vertices = new Vector4[xs * ys * zs * ws];
			int i = 0;
			for (int x = 0; x < xs; x++) {
				for (int y = 0; y < ys; y++) {
					for (int z = 0; z < zs; z++) {
						for (int w = 0; w < ws; w++) {
							Vector4 point = new Vector4 (x / (xs - 1f) - 0.5f, y / (ys - 1f) - 0.5f, z / (zs - 1f) - 0.5f, w / (ws - 1f) - 0.5f);
							if (point.sqrMagnitude == 0)
								continue;
							vertices [i] = point / Mathf.Max (Mathf.Abs (point.x), Mathf.Abs (point.y), Mathf.Abs (point.z), Mathf.Abs (point.w));
							i++;
						}
					}
				}
			}
			Mesh4D mesh = new Mesh4D () {
				name = "HyperCubeFilled",
				vertices = vertices
			};

			mesh.Apply ();

			return mesh;
		}
	}

	public static Mesh4D Simplex4{
		get{ 
			float phi = (Mathf.Sqrt (5) + 1) * 0.5f;
			Mesh4D mesh = new Mesh4D () {
				name = "Simplex4",
				vertices = new Vector4[] {
					new Vector4 (2, 0, 0, 0),
					new Vector4 (0, 2, 0, 0),
					new Vector4 (0, 0, 2, 0),
					new Vector4 (0, 0, 0, 2),
					new Vector4 (phi, phi, phi, phi)
				},
				edges = new int[]{
					0,1,
					0,2,
					0,3,
					0,4,
					1,2,
					1,3,
					1,4,
					2,3,
					2,4,
					3,4
				}
			};
			for (int i = 0; i < mesh.vertices.Length; i++) {
				mesh.vertices [i] = mesh.vertices [i] - Vector4.one;
				mesh.vertices [i] = mesh.vertices [i] * 0.5f;
			}
			mesh.Apply ();

			return mesh;
		}
	}

	public static Mesh4D HyperSphere{
		get{ 
			int xs = 8;
			int ys = 8;
			int zs = 8;
			int ws = 8;

			Vector4[] vertices = new Vector4[xs * ys * zs * ws];
			int i = 0;
			for (int x = 0; x < xs; x++) {
				for (int y = 0; y < ys; y++) {
					for (int z = 0; z < zs; z++) {
						for (int w = 0; w < ws; w++) {
							Vector4 point = new Vector4 (x / (xs - 1f) - 0.5f, y / (ys - 1f) - 0.5f, z / (zs - 1f) - 0.5f, w / (ws - 1f) - 0.5f);
							if (point.sqrMagnitude == 0)
								continue;
							vertices [i] = point.normalized*0.5f;
							i++;
						}
					}
				}
			}
			Mesh4D mesh = new Mesh4D () {
				name = "HyperSphere",
				vertices = vertices
			};

			mesh.Apply ();

			return mesh;
		}
	}

	public static Mesh4D HyperCylinder{
		get{ 
			List<Vector3> sphere3D = new List<Vector3> ();
			int xs = 8;
			int ys = 8;
			int zs = 8;

			int i = 0;
			for (int x = 0; x < xs; x++) {
				for (int y = 0; y < ys; y++) {
					for (int z = 0; z < zs; z++) {
						Vector3 point = new Vector4 (x / (xs - 1f) - 0.5f, y / (ys - 1f) - 0.5f, z / (zs - 1f) - 0.5f);
						if (point.sqrMagnitude == 0)
							continue;
						if (point.sqrMagnitude > 0.25f)
							sphere3D.Add (point.normalized*0.5f);
						else
							sphere3D.Add (point);
						i++;
					}
				}
			}
			return HyperPrism (sphere3D.ToArray(), 6, 3);
		}
	}

	public static Mesh4D HyperTetraederPrism{
		get { 
			float phi = (Mathf.Sqrt (5) + 1) * 0.5f;
			Vector3[] tetraeder = new Vector3[] {
				new Vector3 (2, 0, 0),
				new Vector3 (0, 2, 0),
				new Vector3 (0, 0, 2),
				new Vector3 (phi, phi, phi)
			};
			return HyperPrism (tetraeder, 6, 1);
		}
	}
	static Complex Eta(Complex s){
		Complex z = 0;
		for (int i = 1; i < 75; i++) {
			z += Mathf.Pow (-1, i + 1) / Complex.Pow (i, s);
		}
		return z;
	}
	static Complex Zeta1(Complex s){
		if (s.Real <= 1)
			return new Complex (-1, 0);
		Complex z = new Complex ();
		for(int i=1;i<75;i++){
			z += Complex.Pow(i, -s);
		}
		return z;
	}
	static Complex Zeta2(Complex s){
		if (s == 1) {
			return new Complex (Mathf.Infinity, Mathf.Infinity);
		}
		Complex c = Eta (s) / (1 - Complex.Pow (2, 1 - s));
		if (Math.Abs (c.Real) < 0.1 && Math.Abs (c.Imaginary) < 0.1)
			Debug.Log (s + " : " + c);
		return c;
	}
	public static Mesh4D ZetaFunction{
		get{ 
			return FunctionGraphComplexToComplex (Zeta2, 0, 2, -2, 2, 60, 60);
		}
	}
	static Complex Square(Complex s){
		return Complex.Pow (s,2);
	}
	public static Mesh4D SquareFunction{
		get{ 
			return FunctionGraphComplexToComplex (Square, -4, 4, -4, 4, 20, 20);
		}
	}
	static Complex eToix(double x){
		return new Complex (Math.Cos (x), Math.Sin (x));
	}
	static Complex eToiz(Complex z){
		return eToix (z.Real) / Math.Pow (Math.E, z.Imaginary);
	}
	static Complex Sin(Complex z){
		return new Complex(((eToiz (z) - eToiz (-z)) / (new Complex (0, 2))).Real, 0);
	}

	public static Mesh4D SinFunction{
		get{ 
			return FunctionGraphComplexToComplex (Sin, -15, 15, -15, 15, 50, 50);
		}
	}

	public static Mesh4D HyperPrism(Vector3[] hyperBase, int heightSegments, float height){

		Vector4[] vertices = new Vector4[hyperBase.Length * heightSegments];
		int i = 0;
		foreach (Vector3 basePoint in hyperBase) {
			for (float w = 0; w < height; w += height / heightSegments) {
				vertices [i] = new Vector4 (basePoint.x, basePoint.y, basePoint.z, w);
				i++;
			}
		}

		Mesh4D mesh = new Mesh4D (){
			vertices = vertices
		};
		mesh.Apply ();

		return mesh;
	}
		

	public static Mesh4D FunctionGraphComplexToComplex(Func<Complex, Complex> func, float xMin, float xMax, float yMin, float yMax, int xPoints, int yPoints){
		Vector4[] vertices = new Vector4[xPoints * yPoints];
		int k = 0;
		int xs = 0;
		int ys = 0;
		for (float x = xMin; x < xMax; x += (xMax - xMin) / xPoints) {
			xs += 1;
			if (xs > xPoints)
				break;
			ys = 0;
			for (float y = yMin; y < yMax; y += (yMax - yMin) / yPoints) {
				ys += 1;
				if (ys > yPoints)
					break;
				Complex value = func (new Complex (x, y));
				vertices [k] = new Vector4 (x, y, (float)value.Real, (float)value.Imaginary);
				k++;
			}
		}
		List<int> edges = new List<int> ();
		int end = xPoints * yPoints;
		for (int i = 0; i < end-1; i++) {
			if ((i+1) % yPoints != 0) {
				edges.Add (i);
				edges.Add (i + 1);
			}
			if (i < end - yPoints) {
				edges.Add (i);
				edges.Add (i + yPoints);
			}
		}
		Mesh4D mesh = new Mesh4D () {
			vertices = vertices,
			edges = edges.ToArray()
		};
		mesh.Apply ();
		return mesh;
	}

	public static Mesh4D CoordinateSystemAxis{
		get{ 
			Mesh4D mesh = new Mesh4D {
				vertices = new Vector4[]{
					new Vector4(0,0,0,0),
					new Vector4(1,0,0,0),
					new Vector4(0,1,0,0),
					new Vector4(0,0,1,0),
					new Vector4(0,0,0,1)
				},
				edges = new int[]{
					0,1,
					0,2,
					0,3,
					0,4
				}
			};
			mesh.Apply ();
			return mesh;
		}
	}

	public Mesh4D(){
		if (this.edges == null)
			this.edges = new int[]{ };
	}
		


	

	public void Apply(){
		if (edges.Length % 2 != 0) {
			Debug.LogError ("Error: Edge-Array size must be a multiple of 2");
		}
	}
}
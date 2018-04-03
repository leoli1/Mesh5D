using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Vector5 {
	[SerializeField]
	private float[] _components;
	public float[] components{
		get{ 
			if (_components == null) 
				_components = new float[5];
			return _components;
		}
		set{ 
			if (_components == null)
				_components = new float[5];
			_components = (float[])value.Clone();
		}
	}
	public float x{
		get{ 
			return components [0];
		}
	}
	public float y{
		get{ 
			return components [1];
		}
	}
	public float z{
		get{ 
			return components [2];
		}
	}
	public float w{
		get{ 
			return components [3];
		}
	}
	public float v{
		get{ 
			return components [4];
		}
	}

	public static Vector5 zero{
		get{ 
			return new Vector5 (new float[]{0,0,0,0,0});
		}
	}
	public static Vector5 one{
		get{ 
			return new Vector5(new float[]{1,1,1,1,1});
		}
	}

	public Vector5(float[] comps){
		this._components = (float[])comps.Clone();
	}

	public static Vector5 operator +(Vector5 left, Vector5 right){
		Vector5 v5 = Vector5.zero;
		for (int i = 0; i<5;i++){
			v5.components [i] = left.components [i] + right.components [i];
		}
		return v5;
	}
	public static Vector5 operator -(Vector5 left, Vector5 right){
		return left + (right * -1);
	}

	public static Vector5 operator *(Vector5 left, float right){
		Vector5 v5 = Vector5.zero;
		for (int i = 0; i<5;i++){
			v5.components [i] = left.components [i] * right;
		}
		return v5;
	}
	public static Vector5 operator *(Vector5 left, Vector5 right){
		Vector5 v5 = new Vector5 ();
		for (int i = 0; i<5;i++){
			v5.components [i] = left.components [i] * right.components[i];
		}
		return v5;
	}
	public static Vector5 operator *(float left, Vector5 right){
		return right * left;
	}

	public static Vector5 operator /(Vector5 left, float right){
		return left * (1 / right);
	}

	public float this[int index]{
		get{ 
			if (_components == null)
				_components = new float[5];
			return components [index];
		}
		set{ 
			if (_components == null)
				_components = new float[5];
			components [index] = value;
		}
	}

	public override string ToString (){
		return "x= " + x + ", y= " + y + ", z= " + z + ", w= " + w + ", v= " + v;
	}

	public float Abs(){
		return Mathf.Sqrt (x * x + y * y + z * z + w * w + v * v);
	}

}

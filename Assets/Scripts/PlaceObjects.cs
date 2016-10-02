using UnityEngine;
using System.Collections;
using System;

public class PlaceObjects : MonoBehaviour {

	// Origin around which columns are placed
	public Vector3 placementOrigin;

	// Number of columns to place
	public int columnCount;

	/**
	 * Creates the columns in the game world and returns references to them.
	 * 
	 * Args:
	 * 		colRadius: distance between placementOrigin and columns
	 * 		colLength: side length of columns (square prisms)
	 * 		tag: prefix for column names
	 */
	public GameObject[] createColumns(float colRadius, float colLength, string tag) {
		float columnAngularWidth = 180.0f / (columnCount - 1);

		// Modify baseColumn to fit
		GameObject col = GameObject.CreatePrimitive(PrimitiveType.Cube);
		float scale = colLength/1;
		col.transform.localScale = new Vector3(scale, 1, scale);
		Destroy(col);
		
		// Add columns
		GameObject[] cols = new GameObject[columnCount];
		for (int i=0; i<columnCount; ++i) {
			cols[i] = Instantiate(col, placementOrigin, Quaternion.identity) as GameObject;
			cols[i].name = tag + i;

			float colAngle = (float) (i * columnAngularWidth * Math.PI / 180.0f);
			cols[i].transform.Translate(
					new Vector3( 
						(float)Math.Cos(Math.PI - colAngle) * colRadius, 
						0.0f,
						(float)Math.Sin(colAngle) * colRadius));
			cols[i].transform.Rotate(Vector3.up * (float) i * columnAngularWidth, Space.Self);
		}
		return cols;
	}

	void createFloor(float radius) {
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}

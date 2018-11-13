using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawRange : MonoBehaviour {
	[Range(0,50)]
	public int segments = 10;
	[SerializeField]LineRenderer line;

	void Start ()
	{
		line = GetComponent<LineRenderer>();
		line.SetVertexCount (segments + 1);
		line.useWorldSpace = false;
		line.enabled = false;
	}

	public void CreatePoints (float Radius)
	{
		line.enabled = true;
		float x;
		float z;

		float angle = 10f;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * Radius;
			z = Mathf.Cos (Mathf.Deg2Rad * angle) * Radius;

			line.SetPosition (i,new Vector3(x,0,z) );

			angle += (360f / segments);
		}
	}

	void OnDisable () {
		line.enabled = false;
	}
}

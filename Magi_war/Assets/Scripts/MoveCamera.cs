using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	public GameObject character;

	Vector3 pos;

	public float moveSpeed;
	public float lookSpeed;

	void Update () {
		if (!character) {
			pos.x += Input.GetAxis ("Mouse X") * lookSpeed;
			pos.y += Input.GetAxis ("Mouse Y") * lookSpeed;
			pos.y = Mathf.Clamp (pos.y, -90, 90);

			transform.localRotation = Quaternion.AngleAxis (pos.x, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis (pos.y, Vector3.left);

			transform.position += transform.forward * moveSpeed * Input.GetAxis ("Vertical");
			transform.position += transform.right * moveSpeed * Input.GetAxis ("Horizontal");
		} else {

			Vector3 temp = new Vector3 (character.transform.position.x, character.transform.position.y + 38f, character.transform.position.z + 19f);
			transform.position = temp;
		}
	}

}

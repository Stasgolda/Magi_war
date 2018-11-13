using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	public GameObject character;
	public float smoothing = 5f;

	Vector3 offset;

	void OnEnable () {
		offset = transform.position - character.transform.position;
		offset.x = 0f;
	}

	void FixedUpdate () {
        if (character != null)
        {
			Vector3 targetCamPos = character.transform.position + offset;

			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
        }
    }
}
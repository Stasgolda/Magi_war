using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShockUse : MonoBehaviour {

	[HideInInspector]public int skillDamage;
	[HideInInspector]public GameObject areaObj;

	public void Use (RaycastHit hit) {
		GameObject obj = Instantiate (areaObj, transform.position, transform.rotation);
		obj.transform.SetParent (gameObject.transform);
		float distance = Vector3.Distance (transform.position, hit.point);
		obj.GetComponent<ParticleSystemRenderer> ().lengthScale = distance/2f;
		PlayerHP health = hit.collider.GetComponent<PlayerHP> ();
		if (health) {
			health.takeDamage (skillDamage);
		}
		Destroy (obj, 3f);
	}
}

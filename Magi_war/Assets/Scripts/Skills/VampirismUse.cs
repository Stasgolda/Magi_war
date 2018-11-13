using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismUse : MonoBehaviour {

	[HideInInspector]public int skillDamage;
	[HideInInspector]public GameObject areaObj;

	public void Use (RaycastHit hit) {
		GameObject obj = Instantiate (areaObj, transform.position, transform.rotation);
		//obj.transform.SetParent (gameObject.transform);

		PlayerHP health = hit.collider.GetComponent<PlayerHP> ();
		if (health) {
			PlayerHP myHealth = gameObject.GetComponentInParent<PlayerHP> ();
			myHealth.takeHealth (skillDamage);
			health.takeDamage (skillDamage);
		}
		Destroy (obj, 3f);
	}
}

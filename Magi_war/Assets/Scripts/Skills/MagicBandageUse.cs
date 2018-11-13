using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBandageUse : MonoBehaviour {

	[HideInInspector]public int skillPower;
	[HideInInspector]public GameObject areaObj;

	public void Use () {
		PlayerHP health = gameObject.GetComponentInParent<PlayerHP> ();
		if (health) {
			health.takeHealth (skillPower);
			GameObject obj = Instantiate (areaObj, health.transform);
			obj.transform.localPosition = Vector3.zero;
			Destroy (obj, 2f);
		}
	}
}

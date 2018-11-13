using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitionUse : MonoBehaviour {

	[HideInInspector]public int skillDamage;
	[HideInInspector]public GameObject areaObj;

	private GameObject obj;
	private Transform pos;

	public void Use (RaycastHit hit) {
		pos = hit.transform;
		obj = Instantiate (areaObj, hit.point, Quaternion.identity);

		BoxCollider trigger =  obj.AddComponent<BoxCollider> ();
		trigger.isTrigger = true;

		obj.transform.localScale = new Vector3 (hit.transform.localScale.x, hit.transform.localScale.y, hit.transform.localScale.z);

		SkillHelper helper = obj.AddComponent<SkillHelper> ();
		helper.damage = skillDamage;

		Destroy (obj, 10f);
	}

	void Update () {
		if (obj) {
			Vector3 temp = new Vector3 (pos.position.x, pos.position.y, pos.position.z);
			obj.transform.position = temp;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMeteorUse : MonoBehaviour {

	[HideInInspector]public float timeToDestroy;
	[HideInInspector]public int skillDamage;
	[HideInInspector]public float skillRadius;
	[HideInInspector]public GameObject areaObj;

	private GameObject obj;

	public void Use (RaycastHit hit) {
		obj = Instantiate (areaObj, hit.point, Quaternion.identity);
		SkillHelper helper = obj.AddComponent<SkillHelper> ();
		helper.damage = skillDamage;

		BoxCollider trigger =  obj.AddComponent<BoxCollider> ();
		trigger.size = new Vector3 (skillRadius , skillRadius, skillRadius);
		trigger.isTrigger = true;

		Destroy (obj, timeToDestroy);
	}
}

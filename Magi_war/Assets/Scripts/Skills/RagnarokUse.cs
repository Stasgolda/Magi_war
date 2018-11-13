using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarokUse : MonoBehaviour {
	[HideInInspector]public int skillDamage;
	[HideInInspector]public float skillRadius;
	[HideInInspector]public GameObject areaObj;
	[HideInInspector]public float duration;

	private float timerSpawn;

	public void Use (RaycastHit hit) {
		GameObject obj = Instantiate (areaObj, hit.point, areaObj.transform.rotation);
		obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y + 4 ,obj.transform.position.z);
		GameObject trigger = GameObject.CreatePrimitive (PrimitiveType.Cube);
		trigger.GetComponent<Collider> ().isTrigger = true;
		trigger.transform.SetParent (obj.transform);
		trigger.transform.localPosition = new Vector3 (0, skillRadius/2, 0);
		trigger.transform.localScale = new Vector3 (skillRadius, skillRadius, skillRadius);
		trigger.GetComponent<MeshRenderer> ().enabled = false;
		SkillHelper helper = trigger.AddComponent<SkillHelper> ();
		helper.damage = skillDamage;
		Destroy (obj, duration);
	}
}

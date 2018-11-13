using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TornadoUse : MonoBehaviour {

	[HideInInspector]public float timeToDestroy;
	[HideInInspector]public int skillDamage;
	[HideInInspector]public float skillRadius;
	[HideInInspector]public GameObject areaObj;

	private GameObject obj;
	private Rigidbody rb;

	public void Use () {
		obj = Instantiate (areaObj, transform.position, transform.rotation);
		rb = obj.AddComponent<Rigidbody> ();
		rb.useGravity = false;
		GameObject trigger = GameObject.CreatePrimitive (PrimitiveType.Cube);
		trigger.GetComponent<Collider> ().isTrigger = true;
		trigger.transform.SetParent (obj.transform);
		trigger.transform.localPosition = new Vector3 (0, skillRadius/2, 0);
		trigger.transform.localScale = new Vector3 (skillRadius, skillRadius, skillRadius);
		trigger.GetComponent<MeshRenderer> ().enabled = false;
		SkillHelper helper = trigger.AddComponent<SkillHelper> ();
		helper.damage = skillDamage;
		rb.AddForce (obj.transform.forward * 100f);
		Destroy (obj, timeToDestroy);
	}
        
}
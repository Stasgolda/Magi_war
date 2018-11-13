using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHelper : MonoBehaviour {
	public int damage;
	[SerializeField]
	private float timer;

	void OnTriggerStay(Collider other) {
		if (timer > 1f) {
			if (other.tag == "Enemy") {
				PlayerHP health = other.GetComponent<PlayerHP> ();
				if (health) {
					health.takeDamage (damage);
					timer = 0f;
				}
			}
		}
		timer += Time.deltaTime;
	}
}

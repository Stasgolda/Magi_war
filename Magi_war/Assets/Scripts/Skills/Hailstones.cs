using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hailstones")]
public class Hailstones : TargetSkill {
	public int damage;
	public GameObject areaDamage; // площадь, игровой объект
	public float duration;
	public float radius;

	private HailstonesUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<HailstonesUse> ();
		ability.skillDamage = damage;
		ability.areaObj = areaDamage;
		ability.timeToDestroy = duration;
		ability.skillRadius = radius;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}
}

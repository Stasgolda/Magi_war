using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ragnarok")]
public class Ragnarok : TargetSkill {
	public int damage;
	public GameObject areaDamage; // площадь, игровой объект
	public float duration;
	public float radius;

	private RagnarokUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<RagnarokUse> ();
		ability.skillDamage = damage;
		ability.areaObj = areaDamage;
		ability.duration = duration;
		ability.skillRadius = radius;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}
}

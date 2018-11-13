using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Vampirism")]
public class Vampirism : TargetSkill {
	public int damage;
	public GameObject areaDamage; // площадь, игровой объект

	private VampirismUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<VampirismUse> ();
		ability.skillDamage = damage;
		ability.areaObj = areaDamage;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}

}

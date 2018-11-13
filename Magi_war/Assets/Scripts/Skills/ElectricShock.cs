using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ElectrickShock")]
public class ElectricShock : TargetSkill {
	
	public int damage;
	public GameObject areaDamage; // площадь, игровой объект

	private ElectricShockUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<ElectricShockUse> ();
		ability.skillDamage = damage;
		ability.areaObj = areaDamage;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Ignition")]
public class Ignition : TargetSkill {

	public int damage;
	public GameObject areaDamage; // площадь, игровой объект

	private IgnitionUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<IgnitionUse> ();
		ability.skillDamage = damage;
		ability.areaObj = areaDamage;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}	
}

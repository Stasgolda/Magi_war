using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Magic Bandage")]
public class MagicBandage : NonTargetSkill{

	public int power;
	public GameObject areaDamage; // площадь, игровой объект

	private MagicBandageUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<MagicBandageUse> ();
		ability.skillPower = power;
		ability.areaObj = areaDamage;
	}

	public override void UseAbility() {
		ability.Use ();
	}
}

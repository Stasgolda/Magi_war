using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Tornado")]
public class Tornado : TargetSkill {

	public float timeToDestroy;
	public int damage;
	public float radius;
	public GameObject areaDamage; // площадь, игровой объект

	private TornadoUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<TornadoUse> ();
		ability.skillDamage = damage;
		ability.skillRadius = radius;
		ability.areaObj = areaDamage;
		ability.timeToDestroy = timeToDestroy;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use ();
	}
}

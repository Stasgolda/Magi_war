﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Meteor")]
public class Meteor : TargetSkill {

	public float timeToDestroy;
	public int damage;
	public float radius;
	public GameObject areaDamage; // площадь, игровой объект

	private FireMeteorUse ability;

	public override void Initialize (GameObject obj) {
		ability = obj.GetComponent<FireMeteorUse> ();
		ability.skillDamage = damage;
		ability.skillRadius = radius;
		ability.areaObj = areaDamage;
		ability.timeToDestroy = timeToDestroy;
	}

	public override void UseAbility(RaycastHit hit) {
		ability.Use (hit);
	}
}

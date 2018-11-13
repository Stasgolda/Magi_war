using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSkill : Skill {
	
	public abstract void UseAbility(RaycastHit hit);
}

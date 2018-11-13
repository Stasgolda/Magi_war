using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject{

	public string sName = "New Skill";
	public Sprite sSprite;
	public AudioClip sSound;
	public float coolDown = 1f;
	public float range;

	public abstract void Initialize (GameObject obj);
}

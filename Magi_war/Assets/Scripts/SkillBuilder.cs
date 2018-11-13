using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//скрипт для передачи выбранных скиллов и выбранного режима игры(впадлу создавать новый скрипт или переименновывать этот)
public class SkillBuilder : MonoBehaviour {

	public static SkillBuilder sb;
	public List<Skill> abilities; // скилы
	public GameMode gameMode; //режим игры
	public int characterId;
    public int maxAmountSkill;

	void Start () {
		if (!sb) {
			sb = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void AddSkill (Skill item) {
		abilities.Add (item);
	}

	public void Clear () {
		abilities.Clear ();
	}
}

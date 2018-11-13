using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour {

	[SerializeField]private float currentXP;
	[SerializeField]private int currentLevel;
	[SerializeField]private float xpToNextLevel = 200;

	void Start () {
		Load ();
	}
		
	public void AddXP(float xp) {
		currentXP += xp;
		NextLevel ();
		Save ();
	}

	void NextLevel () {
		if (currentXP == xpToNextLevel) {
			currentXP = 0;
			currentLevel += 1;
			xpToNextLevel = 200;
			for (int i = 0; i < currentLevel; i++) {
				xpToNextLevel += xpToNextLevel * 0.5f;
			}
			print (xpToNextLevel);
		} else if (currentXP > xpToNextLevel) {
			currentXP -= xpToNextLevel;
			currentLevel += 1;
			xpToNextLevel = 200;
			for (int i = 0; i < currentLevel; i++) {
				xpToNextLevel += xpToNextLevel * 0.5f;
			}
			print (xpToNextLevel);
		}
		Save ();
	}

	public void Save () {
		PlayerPrefs.SetInt ("Level", currentLevel);
		PlayerPrefs.SetFloat ("CurrentXP", currentXP);
		PlayerPrefs.SetFloat ("NextLevelXP", xpToNextLevel);
	}

	public void Load () {
		if (PlayerPrefs.HasKey ("Level")) {
			currentLevel = PlayerPrefs.GetInt ("Level");
		} else {
			PlayerPrefs.SetInt ("Level", 1);
			currentLevel = PlayerPrefs.GetInt ("Level");
		}

		if (PlayerPrefs.HasKey ("CurrentXP")) {
			currentXP = PlayerPrefs.GetFloat ("CurrentXP");
		} else {
			PlayerPrefs.SetFloat ("CurrentXP", 0);
			currentXP = PlayerPrefs.GetFloat ("CurrentXP");
		}

		if (PlayerPrefs.HasKey ("NextLevelXP")) {
			xpToNextLevel = PlayerPrefs.GetFloat ("NextLevelXP");
		} else {
			PlayerPrefs.SetFloat ("NextLevelXP", 200);
			xpToNextLevel = PlayerPrefs.GetFloat ("NextLevelXP");
		}
	}
}

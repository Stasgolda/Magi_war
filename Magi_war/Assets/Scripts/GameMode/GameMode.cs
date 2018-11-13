using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : ScriptableObject {
    
	public string gmName;
	public Sprite gmSprite;
	public int amountKillToWin;
	public float gameTime;

    public abstract void GetReward ();
}

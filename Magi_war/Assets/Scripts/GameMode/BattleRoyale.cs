using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/BattleRoyale")]
public class BattleRoyale : GameMode {

    public override void GetReward ()
    {
        var crystalStrength = 500 + (0.7f * PlayerStats.Points);
        var diploma = 5 + PlayerStats.Points % 1000;

        PlayerStats.crystalStrength += crystalStrength;
        PlayerStats.diploma += diploma;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/DeathMatch")]
public class DeathMatch : GameMode {

    public override void GetReward ()
    {
        var crystalStrength = 300 + (0.7f * PlayerStats.Points);
        var diploma = 2 + PlayerStats.Points % 1000;

        PlayerStats.crystalStrength += crystalStrength;
        PlayerStats.diploma += diploma;
    }
}

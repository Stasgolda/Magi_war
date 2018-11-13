using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMode/MOBA")]
public class MOBA : GameMode {

    public override void GetReward ()
    {
        var crystalStrength = 100 + (0.7f * PlayerStats.Points);
        var diploma = PlayerStats.Points % 1000;

        PlayerStats.crystalStrength += crystalStrength;
        PlayerStats.diploma += diploma;
    }
}

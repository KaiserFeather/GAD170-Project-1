using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for converting a battle result into xp to be awarded to the player.
/// 
/// TODO:
///     Respond to battle outcome with xp calculation based on;
///         player win 
///         how strong the win was
///         stats/levels of the dancers involved
///     Award the calculated XP to the player stats
///     Raise the player level up event if needed
/// </summary>
public class XPHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnBattleConclude += GainXP;
    }

    private void OnDisable()
    {
        GameEvents.OnBattleConclude -= GainXP;
    }

    public void GainXP(BattleResultEventData data)
    {
        if (data.outcome > 0)
        {
            data.player.xp += (int)(100.0f * data.outcome);
            GameEvents.PlayerXPGain(data.player.xp);
        }
 
        int XPrequired = 1000 + (data.player.level * 100);

        if (data.player.xp >= XPrequired)
		{
            data.player.level += 1;
            GameEvents.PlayerLevelUp(data.player.level);
            data.player.xp = 0;
		}
    }
}

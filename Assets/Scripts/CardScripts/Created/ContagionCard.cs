using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ContagionCard
{
    GREED = 0,
    POWER = 1,
    VILE_SWORD = 2
}
static class ContagionCardMethods
{
    public static ContagionCard GetRandom()
    {
        return (ContagionCard)Random.Range(0, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : EnemyData
{

    static readonly string clamFolder = "Clam/";
    static readonly string clamClosed = clamFolder + "Clam_Closed";
    static readonly string clamOpen = clamFolder + "Clam_Open";
    static readonly string clamAttacking = clamFolder + "Clam_Attacking";

    public Clam() : base(name: "Clam", maxHP: 10, lives: 2, damage: 3, timer: 3, effect: GetClamEffect(), spriteName: clamClosed, alternateNames: "Reckless Attacker")
    { }

    public static string GetClamEffect()
    {
        return InLineIcon.DAMAGE + ": 3, Vulnerable right before attacking!";
    }

    public override void DisarmEnemy(bool DisarmFromDamage = false)
    {
        base.DisarmEnemy();
        LoadPicture(clamClosed);
    }

    public override int CurrTimer
    { get => base.CurrTimer;
        set => UpdateVulnerability(value);
    }

    private void UpdateVulnerability(int value)
    {
        base.CurrTimer = value;
        //If we have other ways of causing vulnerability, this is incorrect logic. There should be a record of vulnerable debuffs in that case
        if(CurrTimer == 1)
        {
            Vulnerable = true;
            LoadPicture(clamOpen);
        }
        else
        {
            Vulnerable = false;
        }
    }

    
    protected override void AttackUniqueEffect()
    {
        DisplayAttackSprite(clamAttacking, clamClosed);
    }

    IEnumerator DisplayAnimation()
    {
        yield return new WaitForSeconds(.2f);
    }
    
    

    
}

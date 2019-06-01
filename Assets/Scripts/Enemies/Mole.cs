using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : EnemyData
{
    private bool dodge = true;

    public Mole() : base(name: "Mole", maxHP: 4, staggers: 3, damage: 2, timer: 2, effect: InLineIcon.DAMAGE + ": 2, " + "Dodge the first attack per life.", spriteName: "Mole_Hidden", "G Ninja")
    { }

    private static readonly string MoleHidden = "Mole_Hidden";
    private static readonly string MoleShowing = "Mole";

    public override int GetModifiedDamageOnEachHit(int damage)
    {
        if(dodge)
        {
            dodge = false;
            LoadPicture(MoleShowing);
            return 0;
        } else
        {
            return damage;
        }
    }

    protected override void AttackUniqueEffect()
    {
        if (dodge)
        {
            //Need to use MonoBehavior.StartCoroutine for "Unity-Style" wait, but this class does not extend MonoBehavior
            //GameObject.Find("MonoBehaviorUtil").GetComponent<ExternalMonoBehavior>().UseStartCoroutine(PsuedoMoleAnimation());
            DisplayAttackSprite(MoleShowing, MoleHidden);
        }
    }
    /*
    private IEnumerator PsuedoMoleAnimation()
    {
        SetMolePicture(false);
        yield return new WaitForSeconds(0.3f);
        SetMolePicture(true);
        yield return null;
    }*/

    override
    protected void OnLossOfLife()
    {
        dodge = true;
        LoadPicture(MoleHidden);
        //SetMolePicture(dodge);
    }

    /*private void SetMolePicture(bool hidden)
    {
        if (hidden)
        {
            this.SpriteName = "Mole_Hidden";
        } else
        {
            SpriteName = "Mole";
        }
        LoadPicture(SpriteName);
    }*/
}

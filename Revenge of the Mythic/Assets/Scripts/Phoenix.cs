using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : PlayerController
{
    #region Serializable Private Variables
    [SerializeField]
    private GameObject Fireball, Erupt, Firewall;
    [SerializeField]
    private Transform FireballSpawn;
    [SerializeField]
    private CircleCollider2D EruptRange;
    #endregion

    protected override void Attack1() {
        //Ability 1 [Fireball] - Fireball. Does more damage if it passes through ‘Inferno Barrier’. 2 seconds cooldown.
        SetFireballSpawn();
        GameObject temp = Instantiate(Fireball, FireballSpawn.position, FireballSpawn.rotation);
    }
    protected override void Attack2() {
        //Ability 2 [Erupt] - Ranged AoE.Does damage and applies burning DoT(same as DoT mentioned above). 6 second cooldown.
        Instantiate(Erupt, Erupt.GetComponent<RangedAoE>().OptimalSpawnPoint(EruptRange), FireballSpawn.rotation);
    }
    protected override void Attack3() {
        //Ability 3 [Inferno Barrier] - Fire Wall that burns enemies on contact and protect player. Lasts for 5 seconds.
        Instantiate(Firewall, FireballSpawn.position, FireballSpawn.rotation);
    }
    private void SetFireballSpawn()
    {
        FireballSpawn.rotation = Quaternion.identity;
        switch (Direction)
        {
            case 0:
                FireballSpawn.localPosition = new Vector2(0, 0.2f);
                break;
            case 1:
                FireballSpawn.localPosition = new Vector2(0.2f, 0);
                FireballSpawn.Rotate(0, 0, 270);
                break;
            case 2:
                FireballSpawn.localPosition = new Vector2(0, -0.2f);
                FireballSpawn.Rotate(0, 0, 180);
                break;
            case 3:
                FireballSpawn.localPosition = new Vector2(-0.2f, 0);
                FireballSpawn.Rotate(0, 0, 90);
                break;
        }
    }
}

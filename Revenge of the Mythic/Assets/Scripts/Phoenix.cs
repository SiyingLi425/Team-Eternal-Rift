using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : PlayerController
{
    #region Serializable Private Variables
    [SerializeField]
    private GameObject Fireball, Erupt, Firewall;
    [SerializeField]
    private Transform FireballSpawn, FirewallSpawn;
    [SerializeField]
    private CircleCollider2D EruptRange;
    #endregion

    #region Sound Public Variables
    [Header("Audio Controller")]
    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    #endregion

    protected override void Attack1() {
        //Ability 1 [Fireball] - Fireball. Does more damage if it passes through ‘Inferno Barrier’. 2 seconds cooldown.
        SetFireballSpawn();
        Instantiate(Fireball, FireballSpawn.position, FireballSpawn.rotation);
        attackSound1.Play();
    }
    protected override void Attack2() {
        //Ability 2 [Erupt] - Ranged AoE.Does damage and applies burning DoT(same as DoT mentioned above). 6 second cooldown.
        Instantiate(Erupt, Erupt.GetComponent<RangedAoE>().OptimalSpawnPoint(EruptRange), FireballSpawn.rotation);
        attackSound2.Play();
    }
    protected override void Attack3() {
        //Ability 3 [Inferno Barrier] - Fire Wall that burns enemies on contact and protect player. Lasts for 5 seconds.
        SetFireballSpawn();
        Instantiate(Firewall, FirewallSpawn.position, FirewallSpawn.rotation);
        attackSound3.Play();
    }
    private void SetFireballSpawn()
    {
        FireballSpawn.rotation = Quaternion.identity;
        switch (Direction)
        {
            case 0:
                FireballSpawn.localPosition = new Vector2(0, 0.2f);
                FirewallSpawn.localPosition = new Vector2(0, 0.5f);
                break;
            case 1:
                FireballSpawn.localPosition = new Vector2(0.2f, 0);
                FirewallSpawn.localPosition = new Vector2(0.5f, 0);
                FireballSpawn.Rotate(0, 0, 270);
                FirewallSpawn.Rotate(0, 0, 270);
                break;
            case 2:
                FireballSpawn.localPosition = new Vector2(0, -0.2f);
                FirewallSpawn.localPosition = new Vector2(0, -0.5f);
                FireballSpawn.Rotate(0, 0, 180);
                FirewallSpawn.Rotate(0, 0, 180);
                break;
            case 3:
                FireballSpawn.localPosition = new Vector2(-0.2f, 0);
                FirewallSpawn.localPosition = new Vector2(-0.5f, 0);
                FireballSpawn.Rotate(0, 0, 90);
                FirewallSpawn.Rotate(0, 0, 90);
                break;
        }
    }
}

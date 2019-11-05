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
        Instantiate(Firewall, FirewallSpawn.position, FirewallSpawn.rotation);
        attackSound3.Play();
    }
}

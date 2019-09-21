using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerController : MonoBehaviour
{
    #region Hidden Variables
    private int instance;
    private string axisX, axisY;
    private Rigidbody2D rbody;
    private int[] abilityCooldownReset;
    private CapsuleCollider2D basicAttackRange;
    private BoxCollider2D playerCollider;
    private string[] attackAxis;
    #endregion

    //Private Variables
    private int speed = 10;
    private readonly string[] damagable;

    #region Cooldowns
    private readonly int globalCooldownReset = 25;
    private int globalCooldown = 0;
    private int[] abilityCooldown = new int[3];
    #endregion

    //Properties
    #region Protected Properties
    protected int Instance { get { return instance; } }
    protected string AxisX { get { return axisX; } }
    protected string AxisY { get { return axisY; } }
    protected Rigidbody2D rBody { get { return rbody; } }
    protected int[] AbilityCooldownReset { get { return abilityCooldownReset; } }
    protected CapsuleCollider2D BasicAttackRange { get { return basicAttackRange; } }
    protected string[] AttackAxis { get { return attackAxis; } }
    #endregion
    #region Public Properties
    public BoxCollider2D PlayerCollider { get { return playerCollider; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Set Player-Based Axises
        instance = GameObject.FindGameObjectsWithTag("Player").Length;
        axisX = "Horizontal" + instance;
        axisY = "Vertical" + instance;
        attackAxis = new string[4];
        attackAxis[0] = "P" + instance + "BasicAttack";
        for (int z = 1; z < 4; ++z) {
            attackAxis[z] = "P" + instance + "Ability" + z;
        }
        #endregion
        rbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        #region Move Player
        float horiz = Input.GetAxis(axisX);
        float vert = Input.GetAxis(axisY);
        rBody.velocity *= new Vector2(horiz, vert);
        #endregion
        #region Attack
        if (globalCooldown == 0)
        {
            for (int z = 0; z < attackAxis.Length; ++z) {
                if (Input.GetAxis(attackAxis[z]) > 0) {
                    attack(z);
                }
            }
        }
        #endregion
    }

    #region Attacks
    private void attack(int i)
    {
        globalCooldown = globalCooldownReset;
        switch (i)
        {
            case 0:
                BasicAttack();
                break;
            case 1:
                abilityCooldown[0] = abilityCooldownReset[0];
                Attack1();
                break;
            case 2:
                abilityCooldown[1] = abilityCooldownReset[1];
                Attack2();
                break;
            case 3:
                abilityCooldown[2] = abilityCooldownReset[2];
                Attack3();
                break;
        }
    }
    private void BasicAttack()
    {
        foreach (string s in damagable) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s)) {
                if (g.PrimaryCollider().enabled && basicAttackRange.IsTouching(g.PrimaryCollider())) {
                    g.Damage(2);
                }
            }
        }
    }
    protected abstract void Attack1();
    protected abstract void Attack2();
    protected abstract void Attack3();
    #endregion
}

/*
Player Controls:
WASD - Movement (done)
ZXCV - Attacks (done)
Collisions - Not to be included unless necessary
    Enemy Collisions - Each enemy should have their own collision, in case they don't outright kill the player
    Item Collisions - Each item should be responsible for the benifits it provides
*/
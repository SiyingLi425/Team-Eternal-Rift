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
    private bool dead;
    #endregion

    #region Private Variables
    [SerializeField]
    private int health;
    private int speed = 10;
    private int maximumHealth;
    [SerializeField]
    private Sprite[,] playerImages = new Sprite[4, 3];
    #endregion

    //Protected Variables
    [SerializeField]
    protected readonly string[] damagable;

    #region Cooldowns
    private readonly int globalCooldownReset = 25;
    private int globalCooldown = 0;
    private int[] abilityCooldown = new int[3];
    #endregion

    //Properties
    #region Protected Properties
    //Change to public as needed
    protected int Instance { get { return instance; } }
    protected string AxisX { get { return axisX; } }
    protected string AxisY { get { return axisY; } }
    protected Rigidbody2D rBody { get { return rbody; } }
    protected abstract int[] AbilityCooldownReset { get; }
    protected CapsuleCollider2D BasicAttackRange { get { return basicAttackRange; } }
    protected string[] AttackAxis { get { return attackAxis; } }
    #endregion
    #region Public Properties
    public BoxCollider2D PlayerCollider { get { return playerCollider; } }
    public bool Dead { get { return dead; } }
    public int MaximumHealth { get { return maximumHealth; } }
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
        #region Colliders
        playerCollider = GetComponent<BoxCollider2D>();
        basicAttackRange = GetComponent<CapsuleCollider2D>();
        #endregion
        rbody = GetComponent<Rigidbody2D>();
        maximumHealth = health;
        //Set Damagable over here... once there's a list of things that can be damaged
    }

    // Update is called once per frame
    void Update()
    {
        #region Input Actions
        if (dead == false)
        {
            #region Move Player
            float horiz = Input.GetAxis(axisX) * speed;
            float vert = Input.GetAxis(axisY) * speed;
            rbody.velocity *= new Vector2(horiz, vert);
            #endregion
            #region Attack
            if (globalCooldown == 0)
            {
                for (int z = 0; z < attackAxis.Length; ++z)
                {
                    if (Input.GetAxis(attackAxis[z]) > 0)
                    {
                        attack(z);
                    }
                }
            }
            #endregion
        }
        #endregion
        else if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject otherPlayer = players[0] = gameObject ? players[1] : players[0];
            if (otherPlayer.PrimaryCollider().IsTouching(gameObject.PrimaryCollider())){
                ++health;
                if (health >= maximumHealth)
                {
                    dead = false;
                    health = maximumHealth;
                }
            }
        }
    }

    #region Cooldown Timers
    void FixedUpdate() {
        if (globalCooldown > 0)
        {
            --globalCooldown;
        }
        for (int z=0; z<abilityCooldown.Length; ++z) {
            if (abilityCooldown[z] > 0)
            {
                --abilityCooldown[z];
            }
        }
    }
    #endregion
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
                    g.PrimaryController().Damage(2);
                }
            }
        }
    }
    protected abstract void Attack1();
    protected abstract void Attack2();
    protected abstract void Attack3();
    #endregion
    public void Damage (int d)
    {
        health -= d;
        #region Death Commands
        if (d <= 0)
        {
            dead = true;
            health = 0;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            bool allDead = false;
            if (players.Length == 2)
            {
                if (players[0].GetComponent<PlayerController>().Dead && players[1].GetComponent<PlayerController>().Dead)
                {
                    allDead = true;
                }
            }
            if (players.Length == 1 || allDead)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
            }
        }
        #endregion
    }
    public void Damage(int d, string dot)
    {
        //If any script calls this method, add DoT handling to this method
    }
    public void Heal(int percent)
    {
        health += maximumHealth * percent;
        if (health >= maximumHealth)
        {
            health = maximumHealth;
        }
    }
}

/*
Player Controls:
WASD - Movement (done)
ZXCV - Attacks (done)
Collisions - Not to be included unless necessary
    Enemy Collisions - Each enemy should have their own collision, in case they don't outright kill the player
    Item Collisions - Each item should be responsible for the benifits it provides
*/
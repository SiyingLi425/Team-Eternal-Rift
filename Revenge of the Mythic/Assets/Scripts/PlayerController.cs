using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerController : MonoBehaviour
{
    #region Hidden Variables
    private int instance;
    private string axisX, axisY;
    private Rigidbody2D rbody;
    private CapsuleCollider2D basicAttackRange;
    private BoxCollider2D playerCollider;
    private string[] attackAxis;
    private bool dead;
    private int direction = 0; //0 is north, 1 is east, 2 is south, 3 is west. (Read: NESW)
    #endregion

    #region Private Variables
    [SerializeField]
    private int health;
    private int speed = 10;
    [SerializeField]
    private int maximumHealth;
    [SerializeField]
    private Sprite[] north = new Sprite[3], east = new Sprite[3], south = new Sprite[3], west = new Sprite[3];
    private Sprite[,] playerImages = new Sprite[4, 3];
    private int animateTimer = 25, animateTimerReset = 25, animationStage = 0;
    private SpriteRenderer sr;
    #endregion

    //Protected Variables
    [SerializeField]
    protected readonly string[] damagable;

    #region Cooldowns
    private readonly int globalCooldownReset = 25;
    private int globalCooldown = 0;
    private int[] abilityCooldown = new int[3];
    [SerializeField]
    private int[] abilityCooldownReset = new int[3];
    #endregion

    //Properties
    #region Protected Properties
    //Change to public as needed
    protected int Instance { get { return instance; } }
    protected string AxisX { get { return axisX; } }
    protected string AxisY { get { return axisY; } }
    protected Rigidbody2D rBody { get { return rbody; } }
    protected CapsuleCollider2D BasicAttackRange { get { return basicAttackRange; } }
    protected string[] AttackAxis { get { return attackAxis; } }
    protected int Direction { get { return direction; } }
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
        #region Set Sprites
        for (int z=0; z<3; ++z)
        {
            playerImages[0, z] = north[z];
            playerImages[1, z] = east[z];
            playerImages[2, z] = south[z];
            playerImages[3, z] = west[z];
        }
        #endregion
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
            #region Direction Handling
            if (vert != 0 || horiz != 0)
            {
                basicAttackRange.size = vert == 0 ? new Vector2(0.25f, 0.4f) : new Vector2(0.4f, 0.25f);
                basicAttackRange.direction = vert == 0 ? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;
                if (horiz < 0 && vert == 0)
                {
                    direction = 3;
                    basicAttackRange.offset = new Vector2(-0.225f, 0);
                }
                else if (horiz > 0 && vert == 0)
                {
                    direction = 1;
                    basicAttackRange.offset = new Vector2(0.225f, 0);
                }
                else if (vert > 0)
                {
                    direction = 2;
                    basicAttackRange.offset = new Vector2(0, -0.225f);
                }
                else
                {
                    direction = 0;
                    basicAttackRange.offset = new Vector2(0, 0.225f);
                }
            }
            #endregion
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
            #region Animate Player
            --animateTimer;
            if (animateTimer <= 0)
            {
                animateTimer = animateTimerReset;
                animationStage = animationStage == 3 ? ++animationStage : 0;
                int i = animationStage == 3 ? 1 : animationStage;
                sr.sprite = playerImages[direction, i];
            }
            #endregion
        }
        #endregion
        #region Dead Actions
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
        #endregion
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
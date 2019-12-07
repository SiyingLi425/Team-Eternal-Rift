using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerController : MonoBehaviour
{
    #region Hidden Variables
    private int instance;
    private string axisX = "", axisY = "";
    private Rigidbody2D rbody;
    private CapsuleCollider2D basicAttackRange;
    private BoxCollider2D playerCollider;
    private string[] attackAxis;
    private bool dead;
    private int direction = 0; //0 is north, 1 is east, 2 is south, 3 is west. (Read: NESW)
    [SerializeField]
    private string[] damagable;
    private bool healthCode;
    #endregion

    #region Private Variables
    [SerializeField]
    private int health;
    private float speed = 3.0f;
    [SerializeField]
    private int maximumHealth;
    [SerializeField]
    private Sprite[] north = new Sprite[3], east = new Sprite[3], south = new Sprite[3], west = new Sprite[3];
    private Sprite[,] playerImages = new Sprite[4, 3];
    private int animateTimer = 15, animateTimerReset = 15, animationStage = 0;
    private SpriteRenderer sr;
    private bool animate = true;
    [SerializeField]
    private GameObject attackObject, basicAttack;
    private int basicAttackTimer = 0, basicAttackTimerReset = 25;
    private GameController gameController;
    private int level;
    private PersisableObjects persisableObjects;
    [SerializeField]
    private Sprite[] cooldownSprites = new Sprite[3];
    private string[,] instanceKeys = new string[3, 2];
    private KeyCode[,] instanceKeyCodes = new KeyCode[3, 2];
    #region Status Effects
    private int slowTimer = 0, slowTimerReset = 100;
    protected int aegisTimer = 0, aegisTimerReset = 250;
    #endregion
    #endregion

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
    protected string[] Damagable { get { return damagable; } }
    #endregion
    #region Public Properties
    public BoxCollider2D PlayerCollider { get { return playerCollider; } }
    public bool Dead { get { return dead; } }
    public int MaximumHealth { get { return maximumHealth; } }
    public int Health { get { return health; } set { health = value; } }
    public int[] AbilityCoolDown { get { return abilityCooldown; } }
    public bool HealthCode { get { return healthCode; } set { HealthCode h = gameController.gameObject.GetComponent<HealthCode>(); if (h.Health == h.HealthCodeLength) { persisableObjects.healthCode = true; healthCode = value; } } }
    public Sprite[] CooldownSprites { get { return cooldownSprites; } }

    public AudioSource BasicAttackSound;
    public AudioSource gasSound;
    public AudioSource foodSound;
    public AudioSource damageSound;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Set Player-Based Axises
        GameController g = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        g.IndexPlayer();
        instance = g.PlayerNum;
        axisX = "Horizontal" + instance;
        axisY = "Vertical" + instance;
        attackAxis = new string[4];
        attackAxis[0] = "P" + instance + "BasicAttack";
        for (int z = 1; z < 4; ++z) {
            attackAxis[z] = "P" + instance + "Ability" + z;
        }
        if (instance == 1)
        {
            instanceKeys[0, 0] = "Z";
            instanceKeys[0, 1] = "R";
            instanceKeys[1, 0] = "X";
            instanceKeys[1, 1] = "T";
            instanceKeys[2, 0] = "C";
            instanceKeys[2, 1] = "Y";
            instanceKeyCodes[0, 0] = KeyCode.Z;
            instanceKeyCodes[0, 1] = KeyCode.R;
            instanceKeyCodes[1, 0] = KeyCode.X;
            instanceKeyCodes[1, 1] = KeyCode.T;
            instanceKeyCodes[2, 0] = KeyCode.C;
            instanceKeyCodes[2, 1] = KeyCode.Y;
        }
        else
        {
            instanceKeys[0, 0] = ",";
            instanceKeys[0, 1] = "U";
            instanceKeys[1, 0] = ".";
            instanceKeys[1, 1] = "I";
            instanceKeys[2, 0] = "/";
            instanceKeys[2, 1] = "O";
            instanceKeyCodes[0, 0] = KeyCode.Comma;
            instanceKeyCodes[0, 1] = KeyCode.U;
            instanceKeyCodes[1, 0] = KeyCode.Period;
            instanceKeyCodes[1, 1] = KeyCode.I;
            instanceKeyCodes[2, 0] = KeyCode.Slash;
            instanceKeyCodes[2, 1] = KeyCode.O;
        }
        #endregion
        #region Colliders
        playerCollider = GetComponent<BoxCollider2D>();
        basicAttackRange = attackObject.GetComponent<CapsuleCollider2D>();
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
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        persisableObjects = GameObject.FindGameObjectWithTag("PersisableObject").GetComponent<PersisableObjects>();
        if (GoalController.level == 1)
        {
            health = maximumHealth;
            persisableObjects.player1hp = gameController.playerController1.MaximumHealth;
            if (persisableObjects.totalPlayers == 2)
            {
                persisableObjects.player2hp = gameController.playerController2.MaximumHealth;
            }
        }
        else
        {
            if (persisableObjects.healthCode) healthCode = true;
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        //ability1CD.text = abilityCooldown[0];
        #region Input Actions
        if (dead == false)
        {
            #region Move Player
            float s = slowTimer == 0 ? speed : speed / 2; 
            float horiz = Input.GetAxis(axisX) * s;
            float vert = Input.GetAxis(axisY) * s;
            Vector2 movement = new Vector2(horiz, vert);
            rbody.velocity = movement;
            #region Direction Handling
            if (vert != 0 || horiz != 0)
            {
                if (horiz < 0 && vert == 0)
                {
                    direction = 3;
                    
                }
                else if (horiz > 0 && vert == 0)
                {
                    direction = 1;
                }
                else if (vert > 0)
                {
                    direction = 0;
                }
                else
                {
                    direction = 2;
                }
                attackObject.transform.rotation = Quaternion.identity;
                attackObject.transform.Rotate(0, 0, -90 * direction);
            }
            #endregion
            #endregion
            #region Attack
            if (globalCooldown == 0)
            {
                for (int z = 0; z < attackAxis.Length; ++z)
                {
                    if (Input.GetAxis(attackAxis[z]) > 0 && ((z > 0 && abilityCooldown[z - 1] == 0) || z == 0))
                    {
                        if (z > 0)
                        {
                            gameController.ChangeResetKey(instance-1, z-1, Input.GetKeyDown(instanceKeyCodes[z-1, 0]) ? instanceKeys[z-1, 0] : instanceKeys[z-1, 1]);
                        }
                        attack(z);
                    }
                }
            }
            #endregion
            #region Animate Player
            try
            {
                if (axisX != "" && axisY != "" && GetComponent<Griffon>())
                {
                    animate = Input.GetAxis(AxisX) != 0 || Input.GetAxis(AxisY) != 0;
                }
            } catch (System.NullReferenceException) { }
            if (animate)
            {
                --animateTimer;
                if (animateTimer <= 0)
                {
                    animateTimer = animateTimerReset;
                    animationStage = animationStage == 3 ? 0 : ++animationStage;
                    int i = animationStage == 3 ? 1 : animationStage;
                    sr.sprite = playerImages[direction, i];
                }
            }
            #endregion
        }
        #endregion
        #region Dead Actions
        else if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject otherPlayer = players[0] = gameObject ? players[1] : players[0];
            if (otherPlayer.PrimaryCollider().IsTouching(gameObject.PrimaryCollider()))
            {
                ++health;
                if (health >= maximumHealth)
                {
                    dead = false;
                    health = maximumHealth;
                }
            }
        }
        #endregion
        if (basicAttackTimer > 0)
        {
            --basicAttackTimer;
            if (basicAttackTimer <= 0)
            {
                basicAttack.SetActive(false);
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
        if (slowTimer > 0)
        {
            --slowTimer;
        }
        if (aegisTimer > 0)
        {
            aegisTimer = 0;
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
                BasicAttackSound.Play();
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
        basicAttack.SetActive(true);
        basicAttackTimer = basicAttackTimerReset;
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
    private void RealDamage (int d)
    {
        damageSound.Play();
        health -= d;
        #region Death Commands
            if (health <= 0)
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
    private void FakeDamage(int d)
    {
        int hd = d / 2;
        hd = hd == 0 ? 1 : hd;
        health = hd >= health ? 1 : health - hd;
        /*failsafe*/if (health <= 0){health = 1;}
    }
    public void Damage(int d)
    {
        if (aegisTimer > 0)
        {
            aegisTimer = 0;
        }
        else
        {
            if (healthCode) { FakeDamage(d); }
            else { RealDamage(d); }
            persisableObjects.player1hp = gameController.playerController1.Health;
            if (persisableObjects.totalPlayers == 2)
            {
                persisableObjects.player2hp = gameController.playerController2.Health;
            }
        }
    }
    public void Damage(int d, string dot)
    {
        if (aegisTimer > 0)
        {
            aegisTimer = 0;
        }
        else
        {
            if (dot.ToLower().Contains("slow"))
            {
                gasSound.Play();
                slowTimer = slowTimerReset;
            }
            Damage(d);
        }
    }
    public void Heal(float percent)
    {
        foodSound.Play();
        health += (int)(maximumHealth * (percent/100));
        if (health >= maximumHealth)
        {
            health = maximumHealth;
        }
        persisableObjects.player1hp = gameController.playerController1.Health;
        if (persisableObjects.totalPlayers == 2)
        {
            persisableObjects.player2hp = gameController.playerController2.Health;
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
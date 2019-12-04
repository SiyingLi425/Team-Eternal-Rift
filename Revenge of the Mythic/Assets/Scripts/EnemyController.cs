using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyController : MonoBehaviour
{

    //Public Variables
    public int health = 20;
    public float walkSpeed;
    public int attackSpeed, attackDamage;
    public GameObject foodItem;


    private float speedX = 0, speedY = 0;

    ///private Animator enemyAnimator;

    //Private Variables


    protected Collider2D aggroRange, hitBox, playerCollider;
    protected GameController gameController;

    private GameObject player1;
    private GameObject player2;
    private GameObject aggroedPlayer;
    private PlayerController playerController;
    protected Vector2 target, playerPosition;
    public int attackCoolDown;
    protected Transform enemyTransform;
    private Transform playerTransform;
    
    public GameObject AggroedPlayer { get { return aggroedPlayer; } }
    protected Text healthBar;
    public AudioSource enemyHit;

    [Header("Animation Variables")]
    [SerializeField]
    private Sprite[] north = new Sprite[3], east = new Sprite[3], south = new Sprite[3], west = new Sprite[3];
    private Sprite[,] enemyImages = new Sprite[4, 3];
    private int animateTimer = 15, animateTimerReset = 15, animationStage = 0;
    private SpriteRenderer sr;
    private bool animate = true;
    [SerializeField] //Serialized if an enemy should face a certain direction
    private int direction = 2; //0 is north, 1 is east, 2 is south, 3 is west. (Read: NESW)


    public string status;

    [Header("Status Effects")]
    public float bleedTimer;
    public float bleedTime;
    public float burnTimer;
    public float burnTime;
    public float tauntTimer;
    public float tauntTime;
    public float fearTimer, fearTime;
    public float silenceTimer, silenceTime;
    public float knockbackTimer, knockbackTime;

    public Collider2D HitBox { get { return hitBox; } }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        healthBar = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        //enemyAnimator = GetComponent<Animator>();
        #region Set Sprites
        for (int z = 0; z < 3; ++z)
        {
            enemyImages[0, z] = north[z];
            enemyImages[1, z] = east[z];
            enemyImages[2, z] = south[z];
            enemyImages[3, z] = west[z];
        }
        #endregion
        sr = GetComponent<SpriteRenderer>();

        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        aggroedPlayer = GameObject.FindGameObjectWithTag("Player");

        //aggroedPlayer = GameObject.FindGameObjectWithTag("Player");
        //playerCollider = aggroedPlayer.GetComponent<BoxCollider2D>();

        if (healthBar.IsActive() == true)
        {
            healthBar.text = "HP:" + health;
        }

        #region Animation
        if (animate)
        {
            --animateTimer;
            if (animateTimer <= 0)
            {
                animateTimer = animateTimerReset;
                animationStage = animationStage == 3 ? 0 : ++animationStage;
                int i = animationStage == 3 ? 1 : animationStage;
                sr.sprite = enemyImages[direction, i];
            }
        }
        #endregion
        if (status != "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (aggroRange.IsTouching(p.GetComponent<BoxCollider2D>()))
                {
                    aggroedPlayer = p;
                    playerController = p.GetComponent<PlayerController>();
                }
            }
        }

        playerPosition = aggroedPlayer.GetComponent<Transform>().position;
        playerTransform = aggroedPlayer.GetComponent<Transform>();
        playerCollider = aggroedPlayer.GetComponent<BoxCollider2D>();


        if (health <= 0)
        {
            if(foodItem != null)
            {
                Instantiate(foodItem, enemyTransform.position, enemyTransform.rotation);
            }
            
            Destroy(gameObject);
        }

        if (playerCollider.IsTouching(hitBox) && attackCoolDown == 0)
        {
            if (silenceTimer == 0)
            {
                attack();
            }

        }

        if (playerCollider.IsTouching(aggroRange))
        {
            target = playerPosition;
            getMovementTargert();
        }
        #region StatusTimers

        if (silenceTimer > 0)
        {
            silenceTimer--;
        }
        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }

        if (bleedTimer > 0 && bleedTimer % 25 == 0)
        {
            Damage(1);
        }
        if (burnTimer > 0 && burnTimer % 25 == 0)
        {
            Damage(1);
        }

        if (bleedTimer > 0)
        {
            bleedTimer--;
        }
        if (burnTimer > 0)
        {
            burnTimer--;
        }

        if (tauntTimer > 0)
        {
            tauntTime--;
        }
        if (fearTimer > 0)
        {
            fearTimer--;
        }
        if (knockbackTimer > 0)
        {
            knockbackTimer--;
        }
        #endregion


    }
    protected virtual void getMovementTargert()
    {


        Vector2 pos = GetComponent<Transform>().position;
        float xDistance = Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(target.x));
        float yDistance = Mathf.Abs(Mathf.Abs(pos.y) - Mathf.Abs(target.y));


        if (xDistance > yDistance)
        {
            speedX = walkSpeed;
            speedY = yDistance / (xDistance / walkSpeed);
            //enemyAnimator.SetInteger("Direction", 3);

        }
        else
        {
            speedY = walkSpeed;
            speedX = xDistance / (yDistance / walkSpeed);
            //enemyAnimator.SetInteger("Direction", 2);
        }

        if (target.x < pos.x)
        {
            speedX = -speedX;
            //enemyAnimator.SetInteger("Direction", 1);
        }

        if (target.y < pos.y)
        {
            speedY = -speedY;
            //enemyAnimator.SetInteger("Direction", 0);
        }
        #region Direction Handling
        bool vert = Mathf.Abs(speedX) <= Mathf.Abs(speedY);
        if (vert)
        {
            direction = speedY > 0 ? 0 : 2;
        }
        else
        {
            direction = speedX > 0 ? 1 : 3;
        }
        #endregion
        if (fearTimer > 0 || knockbackTimer > 0)
        {
            moveEnemy(-speedX, -speedY);
        }
        else
        {
            moveEnemy(speedX, speedY);

        }
        if(fearTimer > 0)
        {
            //0 is north, 1 is east, 2 is south, 3 is west. (Read: NESW)
            if (direction == 0) direction = 2;
            else if (direction == 1) direction = 3;
            else if (direction == 2) direction = 0;
            else if (direction == 3) direction = 1;
        }
        
        animate = speedX != 0 || speedY != 0;
    }

    public abstract void moveEnemy(float speedX, float speedY);

    protected virtual void attack()
    {
        playerController.Damage(attackDamage);
        enemyHit.Play();
        attackCoolDown = attackSpeed;
    }

    public virtual void Damage(int attackDamage)
    {
        health -= attackDamage;
        healthBar.gameObject.SetActive(true);
    }

    public virtual void Damage(int attackDamage, string s)
    {

        health -= attackDamage;
        status = s; 

        if (status == "Bleed")
        {
            Debug.Log("Bleeding");
            bleedTimer = bleedTime;
        }
        if(status == "Burn")
        {
            burnTimer = burnTime;
        }
        if(status == "Fear")
        {
            fearTimer = fearTime;
        }
        if(status == "Silence")
        {
            silenceTimer = silenceTime;
        }
        if(status == "Knockback")
        {
            knockbackTimer = knockbackTime;
        }
        

        if(status == "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<CircleCollider2D>().IsTouching(hitBox))
                {
                    aggroedPlayer = p;
                }
            }
            target = aggroedPlayer.GetComponent<Transform>().position;
            tauntTimer = tauntTime;
        }
        healthBar.gameObject.SetActive(true);

    }

 


}


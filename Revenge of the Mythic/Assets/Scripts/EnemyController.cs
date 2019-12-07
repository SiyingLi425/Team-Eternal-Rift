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
    public AudioSource enemyDamage;

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

    [Header("Status Bar")]
    public Sprite burn;
    public Sprite taunt;
    public Sprite fear;
    public Sprite bleed;
    public Sprite silence;

    private int burnSlot, tauntSlot, fearSlot, bleedSlot, silenceSlot;
    private bool gotBurn, gotTaunt, gotFear, gotBleed, gotSilence;
    private GameObject status1, status2, status3;
    public List <Sprite> spriteList;

    public Collider2D HitBox { get { return hitBox; } }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        healthBar = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        status1 = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject;
        status2 = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject;
        status3 = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;
        spriteList = new List <Sprite>();
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
        if(spriteList.Count == 0)
        {
            status1.GetComponent<Image>().sprite = null;
        }
        if (spriteList.Count == 1)
        {
            status1.GetComponent<Image>().sprite = spriteList[0];
        }
        if (spriteList.Count == 2)
        {
            status2.GetComponent<Image>().sprite = spriteList[1];
        }
        if (spriteList.Count == 3)
        {
            status3.GetComponent<Image>().sprite = spriteList[2];
        }

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
        if (gotSilence)
        {
            if (silenceTimer == 0)
            {
                spriteList.Remove(silence);
                gotSilence = false;
            }
        }
        if (bleedTimer > 0)
        {
            bleedTimer--;
        }
        if (gotBleed)
        {
            if (bleedTimer == 0)
            {
                spriteList.Remove(bleed);
                gotBleed = false;
            }
        }
        if (burnTimer > 0)
        {
            burnTimer--;
        }
        if (gotBurn)
        {
            if (burnTimer == 0)
            {
                spriteList.Remove(burn);
                gotBurn = false;
            }
        }

        if (tauntTimer > 0)
        {
            tauntTime--;
        }
        if (gotTaunt)
        {
            if (tauntTimer == 0)
            {
                spriteList.Remove(taunt);
                gotTaunt = false;
            }
        }
        if (fearTimer > 0)
        {
            fearTimer--;
        }
        if (gotFear) {
            if (fearTimer == 0) {
                spriteList.Remove(fear);
                gotFear = false;
            } }
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
        enemyDamage.Play();
        health -= attackDamage;
        healthBar.gameObject.SetActive(true);
    }

    public virtual void Damage(int attackDamage, string s)
    {

        health -= attackDamage;
        status = s; 

        if (status == "Bleed")
        {
            bleedTimer = bleedTime;
            bleedSlot = spriteList.Count;
            gotBleed = true;
            spriteList.Add(bleed);
        }
        if(status == "Burn")
        {
            burnTimer = burnTime;
            burnSlot = spriteList.Count;
            gotBurn = true;
            spriteList.Add(taunt);
        }
        if(status == "Fear")
        {
            fearTimer = fearTime;
            fearSlot = spriteList.Count;
            gotFear = true;
            spriteList.Add(fear);
            
        }
        if(status == "Silence")
        {
            silenceTimer = silenceTime;
            silenceSlot = spriteList.Count;
            gotSilence = true;
            spriteList.Add(silence);
        }
        if(status == "Knockback")
        {
            knockbackTimer = knockbackTime;
        }
        

        if(status == "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(p.GetComponent<Griffon>())
                {
                    if (p.GetComponent<CircleCollider2D>().IsTouching(hitBox))
                    {
                        aggroedPlayer = p;
                        tauntSlot = spriteList.Count;
                        gotTaunt = true;
                        spriteList.Add(taunt);
                    }
                }

            }
            target = aggroedPlayer.GetComponent<Transform>().position;
            tauntTimer = tauntTime;
        }
        healthBar.gameObject.SetActive(true);

    }

 


}


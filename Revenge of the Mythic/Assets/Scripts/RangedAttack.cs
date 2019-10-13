using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    #region Private/Protected Serializable Fields
    [SerializeField]
    private float speed;
    [SerializeField]
    protected int time;
    [SerializeField]
    protected int damage;
    [SerializeField]
    private bool destroyOnHit;
    [SerializeField]
    protected string[] targets;
    [SerializeField]
    protected Collider2D Collider;
    #endregion
    #region Private & Protected Variables
    protected Rigidbody2D rBody;
    private Vector2 speedVector;
    private bool doubleDmg = false;
    #endregion
    public Collider2D ColliderGet { get { return Collider; } }
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //The following code can be used to make an object always travel in the direction it's facing (according to its z-rotation)
        /*speedVector = new Vector2(speed * Mathf.Cos(transform.rotation.z*Mathf.Deg2Rad), speed * Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad));
        Debug.Log(transform.rotation.z * Mathf.Deg2Rad);*/
        rBody.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        --time;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected void UpdateCopy()
    {
        rBody.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        --time;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        bool hit = false;
        foreach (string tar in targets)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(tar))
            {
                if (Collider.IsTouching(g.PrimaryCollider()))
                {
                    if (tar == "Wall")
                    {
                        Destroy(gameObject);
                    }
                    else if (tar == "PlayerRanged")
                    {
                        g.GetComponent<RangedAttack>().DoubleDamage();
                    }
                    else
                    {
                        g.PrimaryController().Damage(damage);
                    }
                    hit = true;
                }
            }
        }
        if (hit && destroyOnHit)
        {
            Destroy(gameObject);
        }
    
    }
    public void DoubleDamage()
    {
        if (doubleDmg == false)
        {
            damage *= 2;
            doubleDmg = true;
        }
    }
}

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
    #region Private Variables
    private Rigidbody2D rBody;
    private Vector2 speedVector;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        //Help me I'm sad :(
        switch (transform.rotation.z*180)
        {
            case 0:
                speedVector = new Vector2(0, speed);
                break;
            case 90:
                speedVector = new Vector2(speed, 0);
                break;
            case 180:
                speedVector = new Vector2(0, -speed);
                break;
            case 270:
                speedVector = new Vector2(-speed, 0);
                break;
            default:
                Debug.Log(transform.rotation.z * 180);
                    break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //The following code can be used to make an object always travel in the direction it's facing (according to its z-rotation)
        /*speedVector = new Vector2(speed * Mathf.Cos(transform.rotation.z*Mathf.Deg2Rad), speed * Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad));
        Debug.Log(transform.rotation.z * Mathf.Deg2Rad);*/
        rBody.position += speedVector;
        --time;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        bool hit = false;
        foreach (string tar in targets)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(tar))
            {
                if (Collider.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(damage);
                    hit = true;
                }
            }
        }
        if (hit && destroyOnHit)
        {
            Destroy(this);
        }
    }
}

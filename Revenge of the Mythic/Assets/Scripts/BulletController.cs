using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 speedVector;
    public int time = 500;
    public float speed = 0.02f;
    private Rigidbody2D rBody;
    private GameObject player;
    private PlayerController playerController;
    public int attackDamange = 1;
    private Collider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        bulletCollider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //The following code can be used to make an object always travel in the direction it's facing (according to its z-rotation)
        //speedVector = new Vector2(speed * Mathf.Cos(transform.rotation.z * Mathf.Deg2Rad), speed * Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad));
        //rBody.position += speedVector;
        rBody.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        --time;

        if (time <= 0)
        {
            Destroy(gameObject);
        }

        //if (player.PrimaryCollider().IsTouching(bulletCollider))
        //{
        //    playerController.Damage(attackDamange);
        //    Destroy(gameObject);
        //}
            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Damage(attackDamange);
            Destroy(this.gameObject);
        }
        else if (other.tag == ("Wall") || other.tag == ("Destroyable"))
        {

            Destroy(this.gameObject);
        }

    }
}

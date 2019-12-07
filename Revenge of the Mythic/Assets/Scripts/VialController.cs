using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialController : MonoBehaviour
{
    public int damageAmount;
    public int delayTime;
    public GameObject poison;
    private Vector2 speedVector;
    public int time = 500;
    public float speed = 0.02f;
    private Rigidbody2D rBody;
    public int attackDamange = 1;
    public AudioSource crashSound;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        rBody.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        --time;

        if (time <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Instantiate(poison, this.transform.position, this.transform.rotation);
            crashSound.Play();
            Destroy(this.gameObject);
        } else if (other.tag == ("Wall") || other.tag == ("Destroyable"))
        {
            Instantiate(poison, this.transform.position, this.transform.rotation);
            crashSound.Play();
            Destroy(this.gameObject);
        }

        
        
    }
}

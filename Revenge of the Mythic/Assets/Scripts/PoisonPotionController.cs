using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotionController : MonoBehaviour
{
    public int damageAmount;
    public int delayTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Damage(damageAmount);
        }
        Destroy(this.gameObject, 5.0f);
    }

}

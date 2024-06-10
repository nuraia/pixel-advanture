using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject EnemyDistroyVFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {   //enemy destroy
            Destroy(collision.gameObject);
           
            //show particles
            GameObject vfx = Instantiate(EnemyDistroyVFX, collision.gameObject.transform.position,
                Quaternion.identity);
            Destroy(vfx, 1f);

            // bullet destroy
            Destroy(gameObject);
        }
       

    }

    private void Start()
    {
        Destroy(gameObject, 6f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 5.5f;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        //This is for Player bullets hitting an enemy
        if (gameObject.tag.CompareTo("Bullet") == 0 && other.gameObject.tag.CompareTo("Enemy") == 0)
        {
            GameObject.Destroy(gameObject);
        }

        //This is for Enemy Bullets hitting our Player
        if (gameObject.tag.CompareTo("EnemyBullet") == 0 && other.gameObject.tag.CompareTo("Player") == 0)
        {
            other.gameObject.GetComponent<PlayerMovement>().PlayDeath();
            GameManager.Instance.playerDied();
            
            GameObject.Destroy(gameObject);
        }
        if (gameObject.tag.CompareTo("BulletRefillCase") == 0 && other.gameObject.tag.CompareTo("Player") == 0)
        {  if (other.gameObject.tag.CompareTo("Wall") == 0)
        {
            GameObject.Destroy(gameObject);
        }
            other.gameObject.GetComponent<PlayerMovement>().AddBullet(5);
            
            GameObject.Destroy(gameObject);

        }

        //
        if (other.gameObject.tag.CompareTo("Wall") == 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

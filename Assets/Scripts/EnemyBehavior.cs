using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float movementSpeed;

    public int bulletSpawnTime = 200;
    private int counter = 0;
    public GameObject bulletPrefab;

    void Start()
    {
        movementSpeed = Random.Range(-9, -20);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter >= bulletSpawnTime)
        {
            GameObject.Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            counter = 0;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.CompareTo("Bullet") == 0)
        {
            GameManager.Instance.addPoint();
            GameObject.Destroy(gameObject);
        }
        if (other.gameObject.tag.CompareTo("Wall") == 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

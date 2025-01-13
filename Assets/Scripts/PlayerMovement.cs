using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    bool isAlive = true;
    public float speed = 5.5f;
    public GameObject bulletPrefab;
    private int bulletCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBulletText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.position.x > -8)
                {
                    this.transform.position = new Vector2(transform.position.x - speed, transform.position.y);
                }

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.position.x < 8)
                {
                    this.transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                }

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootBullet();
            }
        }
    }

    void ShootBullet()
    {
        if(bulletCount > 0)
        {
            bulletCount--;
            GameObject.Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            UpdateBulletText();
        }

    }

    public void AddBullet(int Amount)
    {
        bulletCount = bulletCount + Amount;
        UpdateBulletText();
    }

    void UpdateBulletText()
    {
        GameManager.Instance.ammoText.text = "Ammo: " + bulletCount;
    }

    public void PlayDeath()
    {
        isAlive = false;
        GetComponent<AudioSource>().Play();
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(delayedDestroy());
    }

    IEnumerator delayedDestroy()
    {
        yield return new WaitForSeconds(2.2f);

        GameObject.Destroy(gameObject);
    }
}

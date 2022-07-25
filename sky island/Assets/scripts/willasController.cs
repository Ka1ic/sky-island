using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class willasController : MonoBehaviour
{
    public Camera cam;

    public float offset;

    public float delayOfPunching;

    public int speed;

    public int damage;

    public float distanceOfPunching;

    public GameObject player;

    float delay;

    bool isSpining = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);

        if (isSpining)
        {
            Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(delay <= 0)
            {
                punch();
            }
        }

        delay -= Time.deltaTime;
    }

    public void punch()
    {
        if(isSpining)
        {
            StartCoroutine("animOfPunching");

            delay = delayOfPunching;
        }
    }

    public IEnumerator animOfPunching()
    {
        player.GetComponent<playerControls>().move = false;

        isSpining = false;

        Vector3 pos = transform.position - player.transform.position;

        while(Vector2.Distance(pos, transform.position - player.transform.position) < distanceOfPunching)
        {
            transform.Translate(Vector2.up *  speed * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }
        while(Vector2.Distance(pos, transform.position - player.transform.position) > 0.1f)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }

        transform.position = pos + player.transform.position;

        isSpining = true;

        player.GetComponent<playerControls>().move = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Debug.Log(collision.isTrigger);

            if(collision.isTrigger == false)
            {
                collision.GetComponent<enemyControls>().takeDamage(damage);
            }
        }
    }
}

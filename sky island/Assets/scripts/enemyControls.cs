using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class enemyControls : MonoBehaviour
{
    public float delayOfShoting;

    public GameObject bullet;

    public GameObject tileMap;

    public float speed;

    public GameObject target;

    public GameObject shotPoint;

    public float offset;

    public Vector3Int findedBlock = new Vector3Int(0, 0, 0);

    public bool isBlockFind = false;

    //public int side; //0 = left top; 1 = right top; 2 = left bottom; 3 = right bottom

    private int numLifes = 100;

    private Tilemap mapbc;

    private Tilemap tm;

    private bool enter = false;

    private bool des = true;

    private Vector3Int targetTM;

    private Vector3 targetOfShot;

    private float delay;

    void Start()
    {
        tileMap = GameObject.Find("island");

        tm = tileMap.GetComponent<Tilemap>();

        mapbc = GameObject.Find("bc").GetComponent<Tilemap>();

        target = GameObject.Find("center");
    }

    // Update is called once per frame
    void Update()
    {
        targetTM = findedBlock;

        targetOfShot = new Vector3(tm.CellToWorld(targetTM).x, tm.CellToWorld(targetTM).y + 0.25f, tm.CellToWorld(targetTM).z);

        //if (side == 0)
        //{
        //    targetOfShot = new Vector3(targetOfShot.x - 0.25f, targetOfShot.y + 0.125f, targetOfShot.z);
        //}
        //else if (side == 1)
        //{
        //    targetOfShot = new Vector3(targetOfShot.x + 0.25f, targetOfShot.y + 0.125f, targetOfShot.z);
        //}
        //else if (side == 2)
        //{
        //    targetOfShot = new Vector3(targetOfShot.x - 0.25f, targetOfShot.y - 0.125f, targetOfShot.z);
        //}
        //else if (side == 3)
        //{
        //    targetOfShot = new Vector3(targetOfShot.x + 0.25f, targetOfShot.y - 0.125f, targetOfShot.z);
        //}

        //Debug.Log("x = " + targetOfShot.x);

        //Debug.Log("y = " + targetOfShot.y);

        Vector3 difference = targetOfShot - shotPoint.transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (!enter)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {

            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            if(des)
            {
                attack();
                des = false;
            }
        }

        if (isBlockFind)
        {
            if (delay <= 0)
            {
                shot();
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("wall"))
        {
            enter = true;
        }
    }

    public void shot()
    {
        shotPoint = GameObject.Find("shot point");

        Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);

        delay = delayOfShoting;
    }

    public void attack()
    {
        tileMap.GetComponent<tileSystem>().choseTheBlock();
    }

    public void takeDamage(int damage)
    {
        numLifes -= damage;

        if(numLifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

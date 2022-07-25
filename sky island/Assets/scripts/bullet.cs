using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bullet : MonoBehaviour
{
    public GameObject damagePointMiddle;

    public GameObject damagePointLeft;

    public GameObject damagePointRight;

    public float speed;

    public float lifetime;

    public float distance;

    public int damage;

    public LayerMask whatIsSolid;

    public GameObject effect;

    private Tilemap island;

   //private Vector3Int targetTM;

    //private Vector3 target;

    void Start()
    {
        island = GameObject.Find("island").GetComponent<Tilemap>();

        //targetTM = island.GetComponent<tileSystem>().findedBlock;

        //target = island.CellToWorld(targetTM);
    }

    // Update is called once per frame
    void Update()
    {
        //island = GameObject.Find("island").GetComponent<Tilemap>();

        //targetTM = island.GetComponent<tileSystem>().findedBlock;

        //target = island.CellToWorld(targetTM);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("block"))
            {
                //Debug.Log(island.WorldToCell(damagePointMiddle.transform.position) + " " + island.WorldToCell(damagePointLeft.transform.position) + " " + island.WorldToCell(damagePointRight.transform.position));

                hitInfo.collider.GetComponent<tileSystem>().takeDamage(damage, island.WorldToCell(damagePointMiddle.transform.position), island.WorldToCell(damagePointLeft.transform.position), island.WorldToCell(damagePointRight.transform.position));
            }

            Instantiate(effect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        if(lifetime <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}

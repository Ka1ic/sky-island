using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class builder : MonoBehaviour
{
    public Camera cam;

    public Tilemap island;

    public Tilemap bc;

    public Tile redTile, greenTile;

    public Tile emptyBlock;

    public GameObject buttonBuild;

    public GameObject gameManager;

    Tile tileChosen;

    TileBase previousBlock;

    Vector3Int posBefore;

    Vector3 posMouseBefore;

    public void Start()
    {
        previousBlock = island.GetTile(island.WorldToCell(posBefore));

        buttonBuild.GetComponent<Button>().enabled = false;

        Color color = buttonBuild.GetComponent<Image>().color;

        color.a = 0.5f;

        buttonBuild.GetComponent<Image>().color = color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                island.SetTile(posBefore, previousBlock);

                posMouseBefore = cam.ScreenToWorldPoint(Input.mousePosition);

                posBefore = island.WorldToCell(posMouseBefore);

                previousBlock = island.GetTile(island.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)));

                if (bc.GetTile(island.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition))) == emptyBlock)
                {
                    buttonBuild.GetComponent<Button>().enabled = true;

                    Color color = buttonBuild.GetComponent<Image>().color;

                    color.a = 1;

                    buttonBuild.GetComponent<Image>().color = color;

                    island.SetTile(island.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), greenTile);
                }
                else
                {
                    buttonBuild.GetComponent<Button>().enabled = false;

                    Color color = buttonBuild.GetComponent<Image>().color;

                    color.a = 0.5f;

                    buttonBuild.GetComponent<Image>().color = color;

                    island.SetTile(island.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), redTile);
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (posMouseBefore != cam.ScreenToWorldPoint(Input.mousePosition))
                {
                    if (cam.transform.position.x < 10 && posMouseBefore.x - cam.ScreenToWorldPoint(Input.mousePosition).x > 0)
                    {
                        transform.position = new Vector3(transform.position.x + (posMouseBefore.x - cam.ScreenToWorldPoint(Input.mousePosition).x), transform.position.y, transform.position.z);
                    }

                    if (cam.transform.position.x > -10 && posMouseBefore.x - cam.ScreenToWorldPoint(Input.mousePosition).x < 0)
                    {
                        transform.position = new Vector3(transform.position.x + (posMouseBefore.x - cam.ScreenToWorldPoint(Input.mousePosition).x), transform.position.y, transform.position.z);
                    }

                    if (cam.transform.position.y < 5 && posMouseBefore.y - cam.ScreenToWorldPoint(Input.mousePosition).y > 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + (posMouseBefore.y - cam.ScreenToWorldPoint(Input.mousePosition).y), transform.position.z);
                    }

                    if (cam.transform.position.y > -5 && posMouseBefore.y - cam.ScreenToWorldPoint(Input.mousePosition).y < 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + (posMouseBefore.y - cam.ScreenToWorldPoint(Input.mousePosition).y), transform.position.z);
                    }
                }

                posMouseBefore = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    public void chosenStructure(Tile chosen)
    {
        tileChosen = chosen;
    }

    public void build()
    {
        island.SetTile(posBefore, tileChosen);

        island.GetComponent<tileSystem>().addBlock(posBefore, 100);

        if (island.GetTile(new Vector3Int(posBefore.x + 1, posBefore.y, posBefore.z)) == null && bc.GetTile(new Vector3Int(posBefore.x + 1, posBefore.y, posBefore.z)) == null)
        {
            bc.SetTile(new Vector3Int(posBefore.x + 1, posBefore.y, posBefore.z), emptyBlock);
        }

        if (island.GetTile(new Vector3Int(posBefore.x - 1, posBefore.y, posBefore.z)) == null && bc.GetTile(new Vector3Int(posBefore.x - 1, posBefore.y, posBefore.z)) == null)
        {
            bc.SetTile(new Vector3Int(posBefore.x - 1, posBefore.y, posBefore.z), emptyBlock);
        }

        if (island.GetTile(new Vector3Int(posBefore.x, posBefore.y + 1, posBefore.z)) == null && bc.GetTile(new Vector3Int(posBefore.x, posBefore.y + 1, posBefore.z)) == null)
        {
            bc.SetTile(new Vector3Int(posBefore.x, posBefore.y + 1, posBefore.z), emptyBlock);
        }

        if (island.GetTile(new Vector3Int(posBefore.x, posBefore.y - 1, posBefore.z)) == null && bc.GetTile(new Vector3Int(posBefore.x, posBefore.y - 1, posBefore.z)) == null)
        {
            bc.SetTile(new Vector3Int(posBefore.x, posBefore.y - 1, posBefore.z), emptyBlock);
        }

        bc.SetTile(posBefore, null);

        gameManager.GetComponent<manager>().buildModeOff();
    }

    public void over()
    {
        posBefore = new Vector3Int(0, 0, 0);

        previousBlock = island.GetTile(island.WorldToCell(posBefore));

        buttonBuild.GetComponent<Button>().enabled = false;

        Color color = buttonBuild.GetComponent<Image>().color;

        color.a = 0.5f;

        buttonBuild.GetComponent<Image>().color = color;
    }
}

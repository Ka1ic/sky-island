using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class tileSystem : MonoBehaviour
{
    public GameObject spawnPoint;

    public GameObject enemyPref;

    public GameObject player;

    public TileBase TileToSet;

    public Tile[] tiles;

    public int numLifes;

    public GameObject blockAnimDeath;

    public bool CreateTheEnemy;

    public int size;

    public Vector3Int findedBlock = new Vector3Int(0, 0, 0);

    public bool isBlockFind = false;

    public block[] blocks = new block[2500];

    public int counter = 0;

    //private int side = 2; //0 = left top; 1 = right top; 2 = left bottom; 3 = right bottom

    private GameObject enemy;

    private Tilemap map;

    private Tilemap mapbc;

    int x = 0;

    int y = 0;

    Vector3Int PosEnemyInTM;

    public class block
    {
        public int posx;

        public int posy;

        public bool isWhole;

        public int life;

        public Vector3Int pos;

        public void info()
        {
            Debug.Log("pos = " + pos + "\nis whole = " + isWhole + "\n life = " + life);
        }
    }

    void Start()
    {
        map = GetComponent<Tilemap>();

        mapbc = GameObject.Find("bc").GetComponent<Tilemap>();

        //mainCamera = Camera.main;

        for(int i = -size / 2; i < size / 2 + size % 2; i++)
        {

            for (int j = -size / 2; j < size / 2 + size % 2; j++)
            {
                if(i == -size / 2)
                {
                    mapbc.SetTile(new Vector3Int(i - 1, j, 0), TileToSet);
                }

                if (j == -size / 2)
                {
                    mapbc.SetTile(new Vector3Int(i, j - 1, 0), TileToSet);
                }

                if (i == size / 2 + size % 2 - 1)
                {
                    mapbc.SetTile(new Vector3Int(i + 1, j, 0), TileToSet);
                }

                if (j == size / 2 + size % 2 - 1)
                {
                    mapbc.SetTile(new Vector3Int(i, j + 1, 0), TileToSet);
                }

                if (i == 0 && j == 0)
                {
                    map.SetTile(new Vector3Int(i, j, 0), tiles[1]);
                }
                else
                {
                    map.SetTile(new Vector3Int(i, j, 0), tiles[0]);
                }

                blocks[counter] = new block();

                blocks[counter].posx = i;

                blocks[counter].posy = j;

                blocks[counter].isWhole = true;

                blocks[counter].life = numLifes;

                blocks[counter].pos = new Vector3Int(i, j, 0);

                counter++;
            }
        }

        if(CreateTheEnemy)
        {
            Instantiate(enemyPref, spawnPoint.transform.position, Quaternion.identity);

            enemy = GameObject.Find("enemy(Clone)");

            //enemy.GetComponent<enemyControls>().side = side;
        }
    }



    void Update()
    {

    }

    public void choseTheBlock()
    {

        x = 0;

        y = 0;

        PosEnemyInTM = map.WorldToCell(enemy.transform.position);

        int sub = 1000;

        for (int i = 0; i < counter; i++)
        {

            if (Math.Abs(PosEnemyInTM.x - blocks[i].posx) + Math.Abs(PosEnemyInTM.y - blocks[i].posy) < Math.Abs(sub) && blocks[i].isWhole)
            {
                x = blocks[i].posx;

                y = blocks[i].posy;

                sub = Math.Abs(PosEnemyInTM.x - blocks[i].posx) + Math.Abs(PosEnemyInTM.y - blocks[i].posy);
            }
        }

        findedBlock = new Vector3Int(x, y, 0);

        enemy.GetComponent<enemyControls>().findedBlock = findedBlock;

        isBlockFind = true;

        enemy.GetComponent<enemyControls>().isBlockFind = isBlockFind;
    }

    public void destroyBlock(Vector3Int positionOfBlock)
    {
        //mapbc.SetTile(new Vector3Int(positionOfBlock.x + 1, positionOfBlock.y, positionOfBlock.z), null);

        mapbc.SetTile(new Vector3Int(positionOfBlock.x - 1, positionOfBlock.y, positionOfBlock.z), null);

        mapbc.SetTile(new Vector3Int(positionOfBlock.x, positionOfBlock.y - 1, positionOfBlock.z), null);

        mapbc.SetTile(new Vector3Int(positionOfBlock.x + 1, positionOfBlock.y, positionOfBlock.z), null);

        mapbc.SetTile(new Vector3Int(positionOfBlock.x, positionOfBlock.y + 1, positionOfBlock.z), null);

        for (int i = 0; i < 8; i++)
        {
            int xd = 0;

            int yd = 0;

            if (i == 0)
            {
                 xd = 0;

                 yd = 1;
            }
            else if(i == 1)
            {
                 xd = 1;

                 yd = 0;
            }
            else if (i == 2)
            {
                 xd = -1;

                 yd = 0;
            }
            else if (i == 3)
            {
                 xd = 0;

                 yd = -1;
            }
            else if(i == 4)
            {
                xd = 1;

                yd = 1;
            }
            else if (i == 5)
            {
                xd = -1;

                yd = -1;
            }
            else if (i == 6)
            {
                xd = -1;

                yd = 1;
            }
            else if (i == 7)
            {
                xd = 1;

                yd = -1;
            }

            if (map.GetTile(new Vector3Int(positionOfBlock.x + xd, positionOfBlock.y + yd, positionOfBlock.z)) != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    int xd1 = 0;

                    int yd1 = 0;

                    if (j == 0)
                    {
                        xd1 = 0;

                        yd1 = 1;
                    }
                    else if (j == 1)
                    {
                        xd1 = 1;

                        yd1 = 0;
                    }
                    else if (j == 2)
                    {
                        xd1 = -1;

                        yd1 = 0;
                    }
                    else if (j == 3)
                    {
                        xd1 = 0;

                        yd1 = -1;
                    }

                    if (map.GetTile(new Vector3Int(positionOfBlock.x + xd + xd1, positionOfBlock.y + yd + yd1, positionOfBlock.z)) == null)
                    {
                        mapbc.SetTile(new Vector3Int(positionOfBlock.x + xd + xd1, positionOfBlock.y + yd + yd1, positionOfBlock.z), TileToSet);
                    }
                    else
                    {
                        mapbc.SetTile(new Vector3Int(positionOfBlock.x + xd + xd1, positionOfBlock.y + yd + yd1, positionOfBlock.z), null);
                    }
                }
            }
            else if(i < 4)
            {
                for (int j = 0; j < 4; j++)
                {
                    int xd1 = 0;

                    int yd1 = 0;

                    if (j == 0)
                    {
                        xd1 = 0;

                        yd1 = 1;
                    }
                    else if (j == 1)
                    {
                        xd1 = 1;

                        yd1 = 0;
                    }
                    else if (j == 2)
                    {
                        xd1 = -1;

                        yd1 = 0;
                    }
                    else if (j == 3)
                    {
                        xd1 = 0;

                        yd1 = -1;
                    }

                    if (map.GetTile(new Vector3Int(positionOfBlock.x + xd + xd1, positionOfBlock.y + yd + yd1, positionOfBlock.z)) != null && new Vector3Int(positionOfBlock.x + xd + xd1, positionOfBlock.y + yd + yd1, positionOfBlock.z) != positionOfBlock)
                    {
                        mapbc.SetTile(new Vector3Int(positionOfBlock.x + xd, positionOfBlock.y + yd, positionOfBlock.z), TileToSet);

                        break;
                    }
                }
            }
        }

        map.SetTile(positionOfBlock, null);

        Instantiate(blockAnimDeath, new Vector3(map.CellToWorld(positionOfBlock).x, map.CellToWorld(positionOfBlock).y, map.CellToWorld(positionOfBlock).z), Quaternion.identity);

        mapbc.SetTile(positionOfBlock, TileToSet);
    }



    public void addBlock(Vector3Int pos, int numLifes)
    {
        bool find = false;

        for(int i = 0; i < counter; i++)
        {
            if(pos == blocks[i].pos)
            {
                blocks[i].isWhole = true;

                blocks[i].life = numLifes;

                find = true;

                break;
            }
        }

        if(!find)
        {
            blocks[counter] = new tileSystem.block();

            blocks[counter].pos = pos;

            blocks[counter].posx = pos.x;

            blocks[counter].posy = pos.y;

            blocks[counter].isWhole = true;

            blocks[counter].life = numLifes;

            counter++;
        }
    }



    public void takeDamage(int damage, Vector3Int posHitMiddle, Vector3Int posHitLeft, Vector3Int posHitRight)
    {
        bool find = false;
        for (int i = 0; i < counter; i++)
        {
            if (blocks[i].pos == posHitMiddle)
            {

                if (blocks[i].isWhole == false)
                {
                    break;
                }

                blocks[i].life -= damage;

                if (blocks[i].life <= 0)
                {
                    blocks[i].isWhole = false;

                    isBlockFind = false;

                    destroyBlock(blocks[i].pos);

                    enemy.GetComponent<enemyControls>().attack();
                }

                find = true;

                break;
            }
        }
        if(!find)
        {
            for (int i = 0; i < counter; i++)
            {
                if (blocks[i].pos == posHitLeft)
                {

                    if (blocks[i].isWhole == false)
                    {
                        break;
                    }

                    blocks[i].life -= damage;

                    if (blocks[i].life <= 0)
                    {
                        blocks[i].isWhole = false;

                        isBlockFind = false;

                        destroyBlock(blocks[i].pos);

                        enemy.GetComponent<enemyControls>().attack();
                    }

                    find = true;

                    break;
                }
            }
        }
        if (!find)
        {
            for (int i = 0; i < counter; i++)
            {
                if (blocks[i].pos == posHitRight)
                {

                    if (blocks[i].isWhole == false)
                    {
                        break;
                    }

                    blocks[i].life -= damage;

                    if (blocks[i].life <= 0)
                    {
                        blocks[i].isWhole = false;

                        isBlockFind = false;

                        destroyBlock(blocks[i].pos);

                        enemy.GetComponent<enemyControls>().attack();
                    }

                    find = true;

                    break;
                }
            }
        }
    }
}

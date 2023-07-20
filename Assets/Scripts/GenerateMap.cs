using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Notifications.Android;
using UnityEditor;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    //These are the 3 blocks are used as a platform
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    public GameObject wall;

    //Game object of the character
    public GameObject character;

    //Variables needed for the script
    private float cameraHeight;
    private float cameraWidth;

    //Latest position
    private Vector2 edge;

    //Constant for tile Size
    void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        edge = new Vector2(Convert.ToInt32(character.transform.position.x+cameraWidth),0);
        
    }

 
    void Update()
    {
        if (EdgeDetector())
        {
            generateTile();
        }
    }

    //Fonction telling me if player is reaching the edge
    bool EdgeDetector()
    {
        if (Convert.ToInt32(character.transform.position.x) == edge.x)
        {
            edge = new Vector2(Convert.ToInt32(character.transform.position.x + cameraWidth),0);
            return true;
        }
        return false;
    }

    void generateTile()
    {
        //Random value for block choice
        System.Random rand = new System.Random();
        int val1          = rand.Next(1, 4);

        //Random vector for tile size
        System.Random rand2 = new System.Random();
        int valW = rand2.Next(10, 16);
        Vector2 tileSize = new Vector2(valW,1);
        GameObject newTile;

        //Random Y value for height Tile
        System.Random rand3 = new System.Random();
        int valY = rand3.Next(6, 12);

        switch (val1)
        {
            case 1:
                newTile = Instantiate(block1, new Vector2(character.transform.position.x + cameraWidth, valY), Quaternion.identity);
                break;

            case 2:
                newTile = Instantiate(block2, new Vector2(character.transform.position.x + cameraWidth, valY), Quaternion.identity);
                break;

            case 3:
                newTile = Instantiate(block3, new Vector2(character.transform.position.x + cameraWidth, valY), Quaternion.identity);
                break;

            default:
                newTile = null;
                break;

        }

        //Getting the components involved
        SpriteRenderer sp = newTile.GetComponent<SpriteRenderer>();
        BoxCollider2D  bc = newTile.GetComponent<BoxCollider2D>();

        sp.drawMode = SpriteDrawMode.Tiled;
        sp.size = tileSize;
        bc.size = tileSize; 
    }

}

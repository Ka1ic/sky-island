using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject player;

    public GameObject cameraObj;

    public GameObject builder;

    public float speedOfBuilder;

    public GameObject menuOfBlocks;

    bool wakeUp = false;

    public void menuOn(GameObject menu)
    {
        player.GetComponent<playerControls>().move = false;

        menu.SetActive(true);
    }

    public void menuOff(GameObject menu)
    {
        player.GetComponent<playerControls>().move = true;

        menu.SetActive(false);
    }

    public void buildModeOn()
    {
        builder.SetActive(true);

        builder.transform.position = player.transform.position;

        cameraObj.transform.SetParent(builder.transform, true);
    }

    public void buildModeOff()
    {
        builder.GetComponent<builder>().enabled = false;

        builder.transform.position = player.transform.position;

        cameraObj.GetComponent<Camera>().orthographicSize = 1;

        cameraObj.transform.SetParent(player.transform, true);

        builder.SetActive((false));

        builder.GetComponent<builder>().enabled = true;

        menuOn(menuOfBlocks);
    }
}

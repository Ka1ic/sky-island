using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public GameObject menuChoseBlock;

    public GameObject gameManager;
    public void OnMouseDown()
    {
        gameManager.GetComponent<manager>().menuOn(menuChoseBlock);

        this.gameObject.SetActive(false);
    }
}

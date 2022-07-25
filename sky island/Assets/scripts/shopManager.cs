using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    public GameObject button;

    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);

        Color color = sr.color;

        color.a = 0;

        sr.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("Invisible");

            StartCoroutine("Visible");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("Visible");

            StartCoroutine("Invisible");
        }
    }

    IEnumerator Visible()
    {
        button.gameObject.SetActive(true);

        for (float i = sr.color.a; i <= 1.05f; i += 0.05f)
        {
            Color color = sr.color;

            color.a = i;

            sr.color = color;

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Invisible()
    {
        for (float i = sr.color.a; i >= 0; i -= 0.05f)
        {
            Color color = sr.color;

            color.a = i;

            sr.color = color;

            yield return new WaitForSeconds(0.01f);
        }

        button.gameObject.SetActive(false);
    }
}

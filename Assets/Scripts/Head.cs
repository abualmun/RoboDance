using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            GameManager.gameManager.EndGame();
        }
        if (other.CompareTag("ExtraLife"))
        {
            Destroy(other.gameObject);
            GameManager.gameManager.GetExtraLife();
        }
        if (other.CompareTag("SlowTime"))
        {
            Destroy(other.gameObject);
            GameManager.gameManager.UseSlowTime();

        }
    }
}

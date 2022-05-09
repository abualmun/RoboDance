using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * (0.035f + 0.005f * GameManager.gameManager.level));
        if (transform.position.z <= -5)
        {
            Destroy(this.gameObject);
        }
    }
}

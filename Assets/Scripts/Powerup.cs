using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] float startSpeed = 0.035f;
    [SerializeField] float accelerationPerLevel = 0.005f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.slowMotionBool)
        {
            transform.Translate(Vector3.back * startSpeed / 2);
        }
        else
        {
            transform.Translate(Vector3.back * (startSpeed + accelerationPerLevel * GameManager.gameManager.level));
        }
        if (transform.position.z <= -5)
        {
            Destroy(this.gameObject);
        }
    }
}

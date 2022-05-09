using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjects : MonoBehaviour
{
    [SerializeField] float playerZPosition = -1.16f;
    [SerializeField] float startSpeed = 0.035f;
    [SerializeField] float accelerationPerLevel = 0.005f;
    bool scoreWasAdded;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * (startSpeed + accelerationPerLevel * GameManager.gameManager.level));
        if (transform.position.z <= -5)
        {
            Destroy(this.gameObject);
            GameManager.gameManager.walls.RemoveAt(0);
        }
        if (transform.position.z <= playerZPosition && !scoreWasAdded)
        {
            GameManager.gameManager.AddWallScore();
            scoreWasAdded = true;
        }
    }
}

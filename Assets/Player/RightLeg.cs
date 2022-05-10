using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightLeg : MonoBehaviour
{
    // parameters /////////////////////////////////////
    float zOffset;
    [SerializeField] Vector2 clamp;

    // private vars /////////////////////////////////////


    // status bools //////////////////////////////////
    public bool isHeld;


    // Components /////////////////////////////////////
    [SerializeField] Transform joint;


    // Debug //////////////////////////////////////////
    [SerializeField] Vector2 debug;
    [SerializeField] float error;

    Quaternion defaultAngle;

    void Start()
    {
        zOffset = GameManager.gameManager.zOffset;
        defaultAngle = joint.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            RotateSelectedJoint();
            GameManager.gameManager.returnTimer = 3;
        }
        else
        {
            if (GameManager.gameManager.returnTimer < 0)
            {
                joint.rotation = Quaternion.RotateTowards(joint.rotation, defaultAngle, 3);
            }
        }

    }

    private void OnMouseDown()
    {
        isHeld = true;
        GetComponent<TrailRenderer>().enabled = true;
    }
    private void OnMouseUp()
    {
        isHeld = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    void RotateSelectedJoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (Input.touchCount > 0)
        {
            mousePosition = Input.touches[0].position;
        }
        mousePosition.z = zOffset;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Mathf.Atan2(mousePosition.y - joint.position.y, mousePosition.x - joint.position.x) * Mathf.Rad2Deg;

        joint.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(angle + error, clamp.x, clamp.y));
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
            GameManager.gameManager.hasSlowTime = true;
            GameManager.gameManager.slowTimeButton.SetActive(true);
        }
        if (other.CompareTag("Orb"))
        {
            Destroy(other.gameObject);
            GameManager.gameManager.score += 200;
            GameManager.gameManager.scoreMultiplier += 10;
        }
    }
}

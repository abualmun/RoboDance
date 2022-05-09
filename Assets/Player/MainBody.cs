using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // parameters /////////////////////////////////////
    [SerializeField] float zOffset;
    [SerializeField] Vector2 xBorders;

    // private vars /////////////////////////////////////
    float yDiffrence;
    float yDefaultPosition;

    // status bools //////////////////////////////////
    [SerializeField] bool isHeld;


    // Components /////////////////////////////////////
    [SerializeField] Transform joint;
    [SerializeField] Transform lLeg;
    [SerializeField] Transform rLeg;

    // powerups/////////////////////////


    // Debug //////////////////////////////////////////
    [SerializeField] Vector2 debug;
    [SerializeField] float error;

    void Start()
    {
        yDiffrence = joint.position.y - lLeg.position.y + 0.126f;
        yDefaultPosition = joint.position.y - yDiffrence;
    }

    // Update is called once per frame
    void Update()
    {
        debug = new Vector2(lLeg.rotation.eulerAngles.z - 270, 90 - rLeg.rotation.eulerAngles.z);
        float leftAngleFix = lLeg.rotation.eulerAngles.z;
        if (lLeg.rotation.eulerAngles.z == 0)
        {
            leftAngleFix = 360;
        }
        joint.position = new Vector3(joint.position.x, Mathf.Sin(Mathf.Deg2Rad * (Mathf.Max(leftAngleFix - 270, 90 - rLeg.rotation.eulerAngles.z))) * yDiffrence + yDefaultPosition, joint.position.z);

        if (isHeld)
        {
            MovePlayer();
        }
        else
        {

        }

    }

    private void OnMouseDown()
    {
        isHeld = true;
    }
    private void OnMouseUp()
    {
        isHeld = false;
    }

    void MovePlayer()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (Input.touchCount > 0)
        {
            mousePosition = Input.touches[0].position;
        }
        mousePosition.z = zOffset;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        joint.position = new Vector3(Mathf.Clamp(mousePosition.x, xBorders.x, xBorders.y), joint.position.y, joint.position.z);
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
    }
}


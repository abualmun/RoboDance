using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // parameters /////////////////////////////////////
    float zOffset;
    [SerializeField] Vector2 xBorders;

    // private vars /////////////////////////////////////
    float yDiffrence;
    float yDefaultPosition;

    // status bools //////////////////////////////////
    public bool isHeld;


    // Components /////////////////////////////////////
    [SerializeField] Transform joint;
    [SerializeField] Transform lLeg;
    [SerializeField] Transform rLeg;

    // powerups/////////////////////////


    // Debug //////////////////////////////////////////
    [SerializeField] Vector2 debug;
    [SerializeField] float error;
    [SerializeField] float jumpHight;
    [SerializeField] float jumpInAirTime;
    Vector3 defaultPosition;


    void Start()
    {
        zOffset = GameManager.gameManager.zOffset;
        yDiffrence = joint.position.y - lLeg.position.y + 0.126f;
        yDefaultPosition = joint.position.y - yDiffrence;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            MovePlayer();
            GameManager.gameManager.returnTimer = 3;
        }
        else
        {

        }

        if (GameManager.gameManager.isJumping)
        {
            return;
        }
        debug = new Vector2(lLeg.rotation.eulerAngles.z - 270, 90 - rLeg.rotation.eulerAngles.z);
        float leftAngleFix = lLeg.rotation.eulerAngles.z;
        if (lLeg.rotation.eulerAngles.z == 0)
        {
            leftAngleFix = 360;
        }
        joint.position = new Vector3(joint.position.x, Mathf.Sin(Mathf.Deg2Rad * (Mathf.Max(leftAngleFix - 270, 90 - rLeg.rotation.eulerAngles.z))) * yDiffrence + yDefaultPosition, joint.position.z);


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
        if (other.CompareTag("Orb"))
        {
            Destroy(other.gameObject);
            GameManager.gameManager.score += 200;
            GameManager.gameManager.scoreMultiplier += 10;
        }
    }

    public void StartJump()
    {
        StartCoroutine(Jump());
    }
    IEnumerator Jump()
    {
        GameManager.gameManager.isJumping = true;
        defaultPosition = joint.position;
        while (Vector3.Distance(joint.position, defaultPosition + Vector3.up * jumpHight) > 0.01f)
        {
            joint.position = Vector3.MoveTowards(joint.position, defaultPosition + Vector3.up * jumpHight, .02f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(jumpInAirTime);
        while (Vector3.Distance(joint.position, defaultPosition) > 0.01f)
        {
            defaultPosition = new Vector3(joint.position.x, defaultPosition.y, defaultPosition.z);
            joint.position = Vector3.MoveTowards(joint.position, defaultPosition, 0.02f);
            yield return new WaitForEndOfFrame();
        }
        defaultPosition = joint.position;
        GameManager.gameManager.isJumping = false;
    }
    public void StopJump()
    {
        StopAllCoroutines();
        StartCoroutine(UnJump());

    }
    IEnumerator UnJump()
    {

        while (Vector3.Distance(joint.position, defaultPosition) > 0.01f)
        {
            defaultPosition = new Vector3(joint.position.x, defaultPosition.y, defaultPosition.z);
            joint.position = Vector3.MoveTowards(joint.position, defaultPosition, 0.02f);
            yield return new WaitForEndOfFrame();
        }
        GameManager.gameManager.isJumping = false;
    }
}


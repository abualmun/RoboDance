using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joint : MonoBehaviour
{
    // parameters /////////////////////////////////////
    [SerializeField] float zOffset;
    [SerializeField] Vector2 clamp;

    // private vars /////////////////////////////////////


    // status bools //////////////////////////////////
    [SerializeField] bool isHeld;


    // Components /////////////////////////////////////
    [SerializeField] Transform joint;

    // powerups/////////////////////////


    // Debug //////////////////////////////////////////
    [SerializeField] Vector2 debug;
    [SerializeField] float error;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            RotateSelectedJoint();
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

    void RotateSelectedJoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zOffset;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Mathf.Atan2(mousePosition.y - joint.position.y, mousePosition.x - joint.position.x) * Mathf.Rad2Deg;

        joint.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(angle + error, clamp.x, clamp.y));
    }
}

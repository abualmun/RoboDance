using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSwipeDetector : MonoBehaviour
{
    public bool isHeld;
    [SerializeField] LeftArm la;
    [SerializeField] RightArm ra;
    [SerializeField] LeftLeg ll;
    [SerializeField] RightLeg rl;
    [SerializeField] MainBody mb;
    // Update is called once per frame
    void Update()
    {
        if (la.isHeld || ra.isHeld || ll.isHeld || rl.isHeld || mb.isHeld) return;
        Swipe();
    }


    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("Jump");

            }
            // //swipe down
            // if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            // {
            //     Debug.Log("down swipe");
            // }
            // //swipe left
            // if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            // {
            //     Debug.Log("left swipe");
            // }
            // //swipe right
            // if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            // {
            //     Debug.Log("right swipe");
            // }
        }
    }

}

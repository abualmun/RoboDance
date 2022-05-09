using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
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


    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
        void DetectSwipe()
        {
            if (isHeld) return;

            if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
            {
                Debug.Log("Vertical Swipe Detected!");
                if (fingerDownPos.y - fingerUpPos.y > 0)
                {
                    OnSwipeUp();
                }
                else if (fingerDownPos.y - fingerUpPos.y < 0)
                {
                    OnSwipeDown();
                }
                fingerUpPos = fingerDownPos;

            }
            else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
            {
                Debug.Log("Horizontal Swipe Detected!");
                if (fingerDownPos.x - fingerUpPos.x > 0)
                {
                    OnSwipeRight();
                }
                else if (fingerDownPos.x - fingerUpPos.x < 0)
                {
                    OnSwipeLeft();
                }
                fingerUpPos = fingerDownPos;

            }
            else
            {
                Debug.Log("No Swipe Detected!");
            }
        }

        float VerticalMoveValue()
        {
            return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
        }

        float HorizontalMoveValue()
        {
            return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
        }

        void OnSwipeUp()
        {
            Debug.Log("Jump");
        }

        void OnSwipeDown()
        {
            //Do something when swiped down
        }

        void OnSwipeLeft()
        {
            //Do something when swiped left
        }

        void OnSwipeRight()
        {
            //Do something when swiped right
        }
    }



}
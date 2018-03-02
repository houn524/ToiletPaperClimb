using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public float swipePower = 0.05f;

    private ToiletPaper toiletPaper;

    private Vector2 prevMousePosition;
    private Vector2 currMousePosition;

    private string debug;

	// Use this for initialization
	void Start () {
        toiletPaper = GameObject.Find("ToiletPapers").GetComponent<ToiletPaper>() as ToiletPaper;
	}

    public void OnMouseDown()
    {
        currMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        prevMousePosition = currMousePosition;

    }

    // Update is called once per frame
    void Update () {
        if (StageManager.isGameOver == true)
            return;
#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            prevMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            currMousePosition = Input.mousePosition;
            Vector2 mouseDeltaPosition = currMousePosition - prevMousePosition;
            if (mouseDeltaPosition.y < 0)
            {
                //toiletPaper.speed += Mathf.Abs(mouseDeltaPosition.y) * swipePower;
                toiletPaper.PullPapers(mouseDeltaPosition.y);
            }
        }

        //if(Input.GetMouseButtonUp(0))
        //{
        //    currMousePosition = Input.mousePosition;
        //    Vector2 mouseDeltaPosition = currMousePosition - prevMousePosition;
        //    if (mouseDeltaPosition.y < 0)
        //    {
        //        toiletPaper.speed += Mathf.Abs(mouseDeltaPosition.y) * swipePower;
        //        toiletPaper.PullPapers();
        //    }

        //}
#else
        if (Input.touchCount > 0)
        {
            //Vector2 tapPoint1;
            //tapPoint1 = Input.GetTouch(0).position;
            //tapPoint1.y = Screen.height - tapPoint1.y;
            //if (leftRect.Contains(tapPoint1))
            //{
            //    player.MoveLeft();
            //    debug = "left" + leftRect.ToString();
            //}

            //if (rightRect.Contains(tapPoint1))
            //{
            //    player.MoveRight();
            //    debug = "right";
            //}

            //if (bottomRect.Contains(tapPoint1))
            //{
            //    debug = "bottom 0" + bottomRect.ToString();
            Vector2 touchDeltaPosition = new Vector2(0, 0);
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (touchDeltaPosition.y < 0)
            {
                toiletPaper.PullPapers(touchDeltaPosition.y);
                //toiletPaper.speed += Mathf.Abs(touchDeltaPosition.y) * swipePower;
            }
                
            //}
        }
        //else if (Input.touchCount > 1 && Input.touchCount <= 2)
        //{
        //    Vector2 tapPoint1;
        //    tapPoint1 = Input.GetTouch(0).position;
        //    tapPoint1.y = Screen.height - tapPoint1.y;
        //    Vector2 tapPoint2;
        //    tapPoint2 = Input.GetTouch(1).position;
        //    tapPoint2.y = Screen.height - tapPoint2.y;

        //    if (leftRect.Contains(tapPoint1) || leftRect.Contains(tapPoint2))
        //    {
        //        player.MoveLeft();
        //        debug = "left" + leftRect.ToString();
        //    }

        //    if (rightRect.Contains(tapPoint1) || rightRect.Contains(tapPoint2))
        //    {
        //        player.MoveRight();
        //        debug = "right";
        //    }

        //    if (bottomRect.Contains(tapPoint1))
        //    {
        //        debug = "bottom 0" + bottomRect.ToString();
        //        Vector2 touchDeltaPosition = new Vector2(0, 0);
        //        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //            touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        //        if (touchDeltaPosition.y < 0)
        //            toiletPaper.speed += Mathf.Abs(touchDeltaPosition.y) * swipePower;
        //    }

        //    if (bottomRect.Contains(tapPoint2))
        //    {
        //        debug = "bottom 1" + bottomRect.ToString();
        //        Vector2 touchDeltaPosition = new Vector2(0, 0);
        //        if (Input.GetTouch(1).phase == TouchPhase.Moved)
        //            touchDeltaPosition = Input.GetTouch(1).deltaPosition;

        //        if (touchDeltaPosition.y < 0)
        //            toiletPaper.speed += Mathf.Abs(touchDeltaPosition.y) * swipePower;
        //    }
        //}
#endif
    }
}

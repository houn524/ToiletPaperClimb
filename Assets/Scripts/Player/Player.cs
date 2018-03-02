using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float xAxisSpeed = 0.1f;
    [HideInInspector]
    public bool isDead = false;

    private Animator animator;
    private ToiletPaper toiletPaper;

    private bool leftButtonDown = false;
    private bool rightButtonDown = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        toiletPaper = GameObject.Find("ToiletPapers").GetComponent<ToiletPaper>() as ToiletPaper;
	}
	
	// Update is called once per frame
	void Update () {
        //if (toiletPaper.speed > 0f)
        //{
        //    animator.SetBool("Climbing", true);
        //    animator.speed = toiletPaper.speed * 0.5f;
        //}

        //if (leftButtonDown && StageManager.isGameOver == false)
        //    MoveLeft();

        //if (rightButtonDown && StageManager.isGameOver == false)
        //    MoveRight();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
            Die();
    }

    private void Die()
    {
        isDead = true;
    }

    public void MoveLeft()
    {
        if(transform.position.x > -1.35f)
            transform.Translate(-xAxisSpeed, 0, 0, Space.World);
    }

    public void MoveRight()
    {
        if(transform.position.x < 0.75)
            transform.Translate(xAxisSpeed, 0, 0, Space.World);
    }

    public void LeftButtonDown()
    {
        leftButtonDown = true;
    }

    public void LeftButtonUp()
    {
        leftButtonDown = false;
    }

    public void RightButtonDown()
    {
        rightButtonDown = true;
    }

    public void RightButtonUp()
    {
        rightButtonDown = false;
    }
}

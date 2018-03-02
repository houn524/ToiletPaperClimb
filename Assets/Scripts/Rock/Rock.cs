using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    private ToiletPaper toiletPaper;

    private float defaultXPos;
    private float i = 0;

	// Use this for initialization
	void Start () {
        toiletPaper = GameObject.Find("ToiletPapers").GetComponent<ToiletPaper>() as ToiletPaper;
        defaultXPos = transform.position.x;
        transform.localRotation = Quaternion.Euler(90f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (StageManager.isGameOver == true)
            return;

        MoveControl();
        DestroyOutOfScreen();
    }

    private void MoveControl()
    {
        if (transform.position.y <= 2.75f && transform.position.y >= 2.0f)
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3((transform.position.y - 2f) / 0.0085f, 0);
            transform.eulerAngles = rotation.eulerAngles;

            transform.position = new Vector3(defaultXPos - (((0.75f - (transform.position.y - 2f)) * 100f) * 0.0044f), transform.position.y);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0);
            transform.position = new Vector3(defaultXPos - 0.3f, transform.position.y);
            transform.Rotate(transform.forward, i % 360);
            i++;
        }

        //transform.Translate(new Vector3(0, -toiletPaper.speed * Time.deltaTime * 0.585f), Space.World);
    }

    private void DestroyOutOfScreen()
    {
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);//월드 좌표를 스크린 좌표로 변형한다.
        if (view.y < -50)
        {
            Destroy(gameObject);    //스크린 좌표가 -50 이하일시 삭제  
        }
    }
}

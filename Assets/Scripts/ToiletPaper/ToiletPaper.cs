using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToiletPaper : MonoBehaviour {

    public GameObject prefabPaper;
    public GameObject prefabPickedPapers;

    public float leftLength;
    private float leftPage;

    private float velocity = 0f;
    private bool underInertia = false;
    private bool underThrow = false;
    private bool pickPaper = false;
    private float time = 0f;
    private float inertiaTime = 1f;

    private Animator animator;
    private GameObject pickedPaper;
    private GameObject sidePaper;
    private GameObject toiletPaper;
    private GameObject pickedPapers;
    private PaperReferee paperReferee;

    private Vector2 prevMousePosition;
    private Vector2 curMousePosition;
    private Vector2 delPointPosition;

    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
    {
        pickedPaper = null;
#if UNITY_EDITOR
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
#else
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
#endif
        if (hit.collider != null)
        { //히트되었다면 여기서 실행
            Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 
            pickedPaper = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
        }
    }

#if UNITY_EDITOR
    private void ManageMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            underInertia = false;
            velocity = 0.0f;
            curMousePosition = Input.mousePosition;
            CastRay();
        }
        else if(Input.GetMouseButton(0))
        {
            prevMousePosition = curMousePosition;
            curMousePosition = Input.mousePosition;
            delPointPosition = curMousePosition - prevMousePosition;
            if (delPointPosition.y < 0)
            {
                velocity = delPointPosition.y;
                PullPapers(velocity);
            }
            if(pickedPaper != null && delPointPosition.x < -10.0f)
            {
                PickPaper(ThrowDir.Left);
                underThrow = true;
            }
            else if (pickedPaper != null && delPointPosition.x > 10.0f)
            {
                PickPaper(ThrowDir.Right);
                underThrow = true;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            underInertia = true;
            underThrow = false;
        }
    }
#else
    private void ManageTouchInput()
    {
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                underInertia = false;
                CastRay();
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                delPointPosition = Input.GetTouch(0).deltaPosition;
                if(delPointPosition.y < 0)
                {
                    velocity = delPointPosition.y;
                    PullPapers(velocity);
                }

                if (pickedPaper != null && delPointPosition.x < -25.0f)
                {
                    PickPaper(ThrowDir.Left);
                    underThrow = true;
                }
                else if (pickedPaper != null && delPointPosition.x > 25.0f)
                {
                    PickPaper(ThrowDir.Right);
                    underThrow = true;
                }
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                underInertia = true;
            }
        }
    }
#endif

    public void MoveControl()
    {
        if(underInertia == true && time <= inertiaTime)
        {
            PullPapers(velocity);
            velocity = Mathf.Lerp(velocity, 0, time);
            time += Time.smoothDeltaTime;
        }
        else
        {
            underInertia = false;
            time = 0f;
        }
    }

    private void Start()
    {
        paperReferee = gameObject.GetComponent<PaperReferee>() as PaperReferee;
        Debug.Log(paperReferee);
    }

    private void Update()
    {
#if UNITY_EDITOR
        ManageMouseInput();
#else
        ManageTouchInput();
#endif
        MoveControl();
        FillPapers();

    }

    private void FillPapers()
    {
        List<Transform> papers = transform.Cast<Transform>().ToList();
        if(papers.Count == 0)
        {
            GameObject paper = Instantiate(prefabPaper, new Vector3(0.0f, 5.54f, 0.0f), Quaternion.identity);
            paper.transform.parent = gameObject.transform;
            paper.transform.localPosition = new Vector3(0.0f, 5.54f, 0.0f);
        }
        else if(papers[papers.Count - 1].localPosition.y < 6.0f)
        {
            GameObject paper = Instantiate(prefabPaper, papers[papers.Count - 1].localPosition + new Vector3(0f, 1.92f, 0f), Quaternion.identity);
            paper.transform.parent = gameObject.transform;
            paper.transform.localPosition = papers[papers.Count - 1].position + new Vector3(0, 1.92f, 0);
        }
    }

    public void PullPapers(float deltaY)
    {
        deltaY = Mathf.Max(deltaY, -100.0f);
        List<Transform> papers = transform.Cast<Transform>().ToList();
        foreach (Transform paper in papers)
        {
            paper.Translate(0, deltaY * Time.deltaTime * 0.3f, 0, Space.Self);
        }

        //Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(papers[papers.Count - 1].position);
        //if (papers.Count < 6 && (targetScreenPos.x < Screen.width || targetScreenPos.x > 0 || 
        //    targetScreenPos.y < Screen.height || targetScreenPos.y > 0))
        //{
        //    GameObject paper = Instantiate(prefabPaper, papers[papers.Count - 1].localPosition + new Vector3(0f, 1.92f, 0f), Quaternion.identity);
        //    paper.transform.parent = gameObject.transform;
        //    paper.transform.localPosition = papers[papers.Count - 1].position + new Vector3(0, 1.92f, 0);
        //}
    }

    private void PickPaper(ThrowDir dir)
    {
        int pickedPaperCount = 0;
        pickedPapers = Instantiate(prefabPickedPapers, new Vector2(0, 0), Quaternion.identity);
        pickedPapers.GetComponent<PickedPapers>().Direction = dir;
        List<Transform> papers = transform.Cast<Transform>().ToList();

        int i = 0;

        for (i = 0; i < papers.Count; i++)
        {
            if (pickedPaper.GetInstanceID() == papers[i].gameObject.GetInstanceID())
                break;

            if (i >= papers.Count)
                return;
        }

        for(; i >= 0; i--)
        {
            papers[i].parent = pickedPapers.transform;
            papers[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            pickedPaperCount++;
        }
        pickedPaper = null;

        paperReferee.CheckWhereToGo(dir, pickedPaperCount);
    }
}

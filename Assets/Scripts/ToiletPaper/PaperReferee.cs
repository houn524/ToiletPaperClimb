using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperReferee : MonoBehaviour
{
    public GameObject prefabRequiredPaper;

    private int score = 0;

    private GameObject requiredPaperLeft;
    private GameObject requiredPaperRight;

    // Use this for initialization
    void Start()
    {
        requiredPaperLeft = GameObject.Find("RequiredPaper").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRequiredPaper();
    }

    private void SpawnRequiredPaper()
    {
        RequiredPaper csRequiredPaper;
        if(requiredPaperLeft == null)
        {
            requiredPaperLeft = Instantiate(prefabRequiredPaper, new Vector2(-1.55f, 0), Quaternion.identity);
            csRequiredPaper = requiredPaperLeft.GetComponent<RequiredPaper>() as RequiredPaper;
            csRequiredPaper.Dir = ThrowDir.Left;
            csRequiredPaper.LeftPapers = 5;
        }
    }

    public void CheckWhereToGo(ThrowDir dir, int throwedPaperCount)
    {
        int result;
        switch(dir)
        {
            case ThrowDir.Left:
                result = requiredPaperLeft.GetComponent<RequiredPaper>().CheckThrowedPapers(throwedPaperCount);
                break;
            default:
                result = 0;
                break;
        }

        if(result > 0)
        {
            StageManager.score += result;
        }
    }
}

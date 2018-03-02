using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredPaper : MonoBehaviour {

    private int leftPapers = 5;
    public int LeftPapers {
        get {
            return leftPapers;
        }
        set {
            leftPapers = value;
        }
    }

    private ThrowDir dir = ThrowDir.Left;
    public ThrowDir Dir {
        get {
            return dir;
        }
        set {
            dir = value;
        }
    }

    private GameObject paper;
    private TextMesh txtNumber;

	// Use this for initialization
	void Start () {
        paper = transform.Find("Paper").gameObject;
        txtNumber = paper.transform.Find("txt_Number").gameObject.GetComponent<TextMesh>();
        txtNumber.text = leftPapers.ToString();
	}
	
    public int CheckThrowedPapers(int throwedPaperCount)
    {
        leftPapers -= throwedPaperCount;
        txtNumber.text = leftPapers.ToString();
        if (leftPapers > 0)
            return throwedPaperCount;
        else if (leftPapers == 0)
        {
            Destroy(gameObject);
            return throwedPaperCount;
        }
        else
        {
            Destroy(gameObject);
            return leftPapers;
        }  
    }
}

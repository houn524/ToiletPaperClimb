using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowDir { Left = -1, Right = 1 };

public class PickedPapers : MonoBehaviour {

    private ThrowDir direction;
    public ThrowDir Direction {
        get {
            return direction;
        }
        set {
            direction = value;
        }
    }

    private float velocity = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveControl();
        if (transform.childCount <= 0)
            Destroy(gameObject);
	}

    private void MoveControl()
    {
        transform.Translate((float)direction * Time.deltaTime * 15.0f, 0.0f, 0.0f, Space.World);
    }
}

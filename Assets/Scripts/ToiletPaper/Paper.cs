using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (targetScreenPos.x > Screen.width + 400 || targetScreenPos.x < -400)
        {
            Destroy(gameObject);
        }
    }
}

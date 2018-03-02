using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    static public int score;
    static public bool isGameOver = false;

    private float fps;

    public Text txtScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start () {
        score = 0;
        isGameOver = false;
        txtScore.text = "Score : " + score;
        //player = GameObject.Find("Player").GetComponent<Player>() as Player;
	}

    private void Update()
    {
        txtScore.text = "Score : " + score;
        //if (player.isDead == true)
        //    GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        ClearStage();
    }

    public void ClearStage()
    {
        StartCoroutine("DelayClearStage");
    }

    IEnumerator DelayClearStage()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("GameScene");
    }

    private void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUI.color = Color.green;
        GUI.Label(new Rect(0, 0, 100, 20), fps + "fps");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool levelFailed = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            levelFailed = true;
            print("Fail");
            SceneManager.LoadScene(1);
        }
    }

    public bool GetFailed()
    {
        return levelFailed;
    }

    public void SetWin()
    {
        SceneManager.LoadScene(2);
    }
}

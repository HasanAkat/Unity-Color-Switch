using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float jump = 10f;
    public Rigidbody2D rb;
    public string currentColor;
    public int nextSceneLoad;
    private void Start()
    {
        ChangeColor();
    }
    void Update()
    {
        if( Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) )
        {
            rb.velocity = Vector2.up * jump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Changer")
        {
            ChangeColor();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                Debug.Log("You Completed ALL Levels");
                SceneManager.LoadScene(nextSceneLoad);

            }
            else
            {
                //Move to next level
                SceneManager.LoadScene(nextSceneLoad);

                //Setting Int for Index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }

        else if (collision.gameObject.tag != gameObject.tag)
        {
            RestartGame();
        }
    }

    void ChangeColor()
    {
        Color randomColor = RandomizeColor();
        Renderer playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = randomColor;
        SetPlayerTag(randomColor);
    }

    Color RandomizeColor()
    {
        Color[] possibleColors = { Color.cyan, Color.yellow, new Color(1f, 0f, 0.5019608f, 1f), new Color(0.5490196f, 0.07450981f, 0.9843137f, 1f) }; // cyan, yellow, pink, purple
        return possibleColors[Random.Range(0, possibleColors.Length)];
    }

    void SetPlayerTag(Color playerColor)
    {
        if (playerColor.Equals(Color.cyan))
        {
            gameObject.tag = "Cyan";
        }
        else if (playerColor.Equals(Color.yellow))
        {
            gameObject.tag = "Yellow";
        }
        else if (playerColor.Equals(new Color(1f, 0f, 0.5019608f, 1f))) // Pink
        {
            gameObject.tag = "Pink";
        }
        else if (playerColor.Equals(new Color(0.5490196f, 0.07450981f, 0.9843137f, 1f))) // Purple
        {
            gameObject.tag = "Purple";
        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;

    public static GameManager instance;

    private void Awake()
    {
        instance= this;
    }

    private void Start() 
    { 
        winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ShowWin(bool hasPlayer1Won)
    {
       
        if (hasPlayer1Won){
            winText.text = "Player 1 Wins";
        }
        else
        {
            winText.text = "Player 2 Wins";

        }

        winText.gameObject.SetActive(true);
    }
}

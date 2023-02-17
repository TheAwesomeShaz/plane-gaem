using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Netcode;
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
            RequestReloadServerRPC();
        }
    }

    [ServerRpc]
    private void RequestReloadServerRPC()
    {
        ReloadClientRPC();
    }
    [ClientRpc]
    private void ReloadClientRPC()
    {
       ReloadScene();
    }


    [ServerRpc]
    private void RequestShowWinServerRPC(bool hasPlayer1Won)
    {
        ShowWinClientRPC(hasPlayer1Won);
    }
    [ClientRpc]
    private void ShowWinClientRPC(bool  hasPlayer1Won)
    {
        ShowWin(hasPlayer1Won);
    }

    private static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }


    public void ShowWinAccrossNetwork(bool hasPlayer1Won)
    {
       
        RequestShowWinServerRPC(hasPlayer1Won);
    }
    public void ShowWin(bool hasPlayer1Won)
    {

        if (hasPlayer1Won)
        {
            winText.text = "Player 1 Wins";
        }
        else
        {
            winText.text = "Player 2 Wins";

        }

        winText.gameObject.SetActive(true);
    }
}

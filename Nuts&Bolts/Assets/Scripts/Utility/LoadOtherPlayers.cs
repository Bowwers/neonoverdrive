using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadOtherPlayers : MonoBehaviour
{
    #region Fields
    public List<Transform> SpawnLocations = new List<Transform>();
    public Object PlayerPrefab;
    public Object GUIPrefab;
    #endregion

    // Use this for initialization
    void Start ()
    {
        StateCarry MultiplayerNum = FindObjectOfType<StateCarry>();
        for (int i = 0; i < MultiplayerNum.ConnectedPlayers; i++)
        {
            // Create a player prefab
            GameObject NewPlayer = Instantiate(PlayerPrefab, SpawnLocations[i].position, SpawnLocations[i].rotation) as GameObject;

            // Preps the camera
            Camera NewCamera = NewPlayer.GetComponentInChildren<Camera>();
            if (i == 0)
            {
                NewCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            }
            else if (i == 1)
            {
                NewCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            }
            else if (i == 2)
            {
                NewCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
            }
            else if (i == 3)
            { 
                NewCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            }

            GameObject newGUI = Instantiate(GUIPrefab) as GameObject;
            newGUI.GetComponent<Canvas>().worldCamera = NewCamera;
            newGUI.GetComponent<Canvas>().planeDistance = 0.5f;
            newGUI.name = "PlayerGUI" + (i + 1).ToString();

            // Assign the right identity to it
            NewPlayer.GetComponent<PlayerChar>().SetPlayerIdentity(i + 1);
        }
    }
}

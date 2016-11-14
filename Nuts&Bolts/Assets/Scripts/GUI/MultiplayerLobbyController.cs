using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiplayerLobbyController : MonoBehaviour
{
    #region Fields
    public GameObject InformationHolder;
    public List<UnityEngine.UI.Text> PlayerConnectionPrompts;
    public float DelayMaxTime = 3f;

    List<bool> PlayerNConnected;
    int ConnectedPlayers = 0;

    float TimeLeftOnDelay = 3f;
    bool TimerActive = false;
    #endregion
	
    // Setups the connection text/prompt
    void Start ()
    {
        PlayerNConnected = new List<bool>();
        foreach (UnityEngine.UI.Text text in PlayerConnectionPrompts)
        {
            PlayerNConnected.Add(false);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < PlayerConnectionPrompts.Count; i++)
        {
            string PlayerIndex = "Player " + (i + 1) + " Start";

            if (Input.GetButton(PlayerIndex) &&
                !PlayerNConnected[i])
            {
                PlayerNConnected[i] = true;
                PlayerConnectionPrompts[i].text = "Player " + (i + 1) + " is connected";
                ConnectedPlayers++;
            }
        }

        if (ConnectedPlayers > 2 &&
            Input.GetButtonDown("Player 1 Start") &&
            TimerActive != true)
        {
            TimerActive = true;
            TimeLeftOnDelay = DelayMaxTime;
        }

        // Timer goes down, if it's active
        if (TimerActive)
        {
            TimeLeftOnDelay -= Time.deltaTime;
            if (TimeLeftOnDelay < 0)
            {
                StateCarry Carrier = FindObjectOfType<StateCarry>();
                Carrier.ConnectedPlayers = ConnectedPlayers;
                DontDestroyOnLoad(Carrier);

                // Load game or whatever
                SceneManager.LoadScene("LoadingScreen");
            }
        }

        // Stops the timer if the button is released
        if (Input.GetButtonUp("Player 1 Start"))
        {
            TimerActive = false;
        }
	}
}

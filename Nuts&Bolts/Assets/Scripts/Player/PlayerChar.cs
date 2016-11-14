using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerChar : MonoBehaviour
{
    #region Variables
    // Current equipped weapon
    public int PlayerId;
    public float MaxHealth;

    private float Health;
    private float Scraps;
    private Text HealthText; // Used to display the player's health
    private Text ScrapText; // Used to display the player's scrap
    #endregion

    // Resets the GUI when it needs to be updated
    private void ResetGUI ()
    {
        HealthText.text = Health.ToString();
        ScrapText.text = Scraps.ToString();
    }

    // Resets the player on death
    private void Reset ()
    {
        // Instantiate explosion
        // Create scrap

        GameObject StateManager = GameObject.Find("StateManager");
        Debug.Log(PlayerId.ToString());
        transform.position = StateManager.GetComponent<LoadOtherPlayers>().SpawnLocations[PlayerId].transform.position;
        transform.eulerAngles = new Vector3(0, 0, 0);

        ControlActiveWeapons WeaponsController = gameObject.GetComponentInChildren<ControlActiveWeapons>();
        WeaponsController.ResetGuns();

        Health = MaxHealth;
        Scraps = 0;
    }

    // Sets the player Id (just a number to identify them) used to listen to input
    public void SetPlayerIdentity (int Id)
    {
        PlayerId = Id;

        Health = MaxHealth;
        Scraps = 0;

        GameObject UIElements = GameObject.Find("PlayerGUI" + PlayerId);
        HealthText = UIElements.GetComponentsInChildren<Text>().First(x => x.gameObject.name == "HealthText");
        ScrapText = UIElements.GetComponentsInChildren<Text>().First(x => x.gameObject.name == "ScrapText");

        ControlActiveWeapons WeaponsController = gameObject.GetComponentInChildren<ControlActiveWeapons>();
        WeaponsController.SetPlayerId (Id);

        PlayerController PlayerController = gameObject.GetComponentInChildren<PlayerController>();
        PlayerController.SetPlayerId (Id);

        ResetGUI();
    }

    // Takes damage
    public void Damage (GunHit gunHit)
    {
        Health -= gunHit.DamageDealt;
        if (Health < 0)
        {
            Reset();
        }

        ResetGUI();
    }

    // Reacts to picking up a collectible
    public void GetPickUp (PickUpData pickUpData)
    {
        // Process data
        if (pickUpData.Type == PickUpScript.PickUpType.Scrap)
        {
            Scraps += pickUpData.Value;
        }
        else if (pickUpData.Type == PickUpScript.PickUpType.Health)
        {
            Health += pickUpData.Value;
        }

        ResetGUI();
    }
}

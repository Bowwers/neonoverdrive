using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ControlActiveWeapons : MonoBehaviour
{
    #region Variables
    public string InputNameNextWeapon;
    public string InputNamePreviousWeapon;

    public Text WeaponName;
    public Text AmmoText;
    public char AmmoSeparatorCharacter;
    public int PlayerId;

    private int CurrentWeaponIndex = 0;
    private BaseWeapon[] AvailableWeapons;
    #endregion

    // Use this for initialization
    void Start ()
    {
        AvailableWeapons = gameObject.GetComponentsInChildren<BaseWeapon>();
        AvailableWeapons[CurrentWeaponIndex].SetActive(true);

        int playerId = gameObject.GetComponentInParent<PlayerChar>().PlayerId;
        WeaponName = GameObject.Find("PlayerGUI" + playerId).GetComponentsInChildren<Text>().First(x => x.name == "CurrentWeaponText");
        WeaponName.text = AvailableWeapons[CurrentWeaponIndex].WeaponName;

        AmmoText = GameObject.Find("PlayerGUI" + playerId).GetComponentsInChildren<Text>().First(x => x.name == "AmmoText");
        UpdateAmmo();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButtonDown (InputNameNextWeapon))
        {
            AvailableWeapons[CurrentWeaponIndex].SetActive(false);

            if (CurrentWeaponIndex < AvailableWeapons.Length - 1)
            {
                CurrentWeaponIndex++;
            }
            else
            {
                CurrentWeaponIndex = 0;
            }

            AvailableWeapons[CurrentWeaponIndex].SetActive(true);
            WeaponName.text = AvailableWeapons[CurrentWeaponIndex].WeaponName;
        }

        if (Input.GetButtonDown (InputNamePreviousWeapon))
        {
            AvailableWeapons[CurrentWeaponIndex].SetActive(false);

            if (CurrentWeaponIndex < AvailableWeapons.Length - 1)
            {
                CurrentWeaponIndex++;
            }
            else
            {
                CurrentWeaponIndex = 0;
            }

            AvailableWeapons[CurrentWeaponIndex].SetActive(true);
            WeaponName.text = AvailableWeapons[CurrentWeaponIndex].WeaponName;
        }

        UpdateAmmo();
    }

    // Sets the player Id so it only listens to the correct joystick
    public void SetPlayerId (int Id)
    {
        PlayerId = Id;
        InputNameNextWeapon = InputNameNextWeapon + PlayerId.ToString();
        InputNamePreviousWeapon = InputNamePreviousWeapon + PlayerId.ToString();
    }

    // Updates the GUI
    public void UpdateAmmo ()
    {
        AmmoText.text = AvailableWeapons[CurrentWeaponIndex].CurrentAmmoCount.ToString() +
                        AmmoSeparatorCharacter +
                        AvailableWeapons[CurrentWeaponIndex].CurrentAmmoMax.ToString();
    }

    // Resets the guns upon death
    public void ResetGuns ()
    {
        for(int i = 0; i < AvailableWeapons.Length; i++)
        {
            AvailableWeapons[i].Reset();
        }
    }
}

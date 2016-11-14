using UnityEngine;

public class GunHit
{
    public float DamageDealt;
    public RaycastHit Hit;
}

// Used to make all standard weapons have a similar interface
[RequireComponent(typeof(AudioSource))]
public abstract class BaseWeapon : MonoBehaviour
{
    #region Variables
    public string WeaponName;
    public int AmmoMax;
    public int AmmoPerShot;
    public int StartingAmmo;
    public string ButtonToFire;
    public string ButtonToReload;
    public float DelayOnFire = 0.1f;
    public float DelayOnReload = 1.2f;
    public int PlayerId;
    public int CurrentAmmoMax;
    public int CurrentAmmoCount;

    public AudioClip ShootSound;

    protected bool ReadyToFire = true;
    protected bool Active = true;
    protected AudioSource Source;
    protected ControlActiveWeapons WeaponController;
    protected Camera PlayerCamera;
    #endregion

    public abstract void Shoot();
    
    public void SetActive (bool Value)
    {
        Active = Value;
    }

    public void Reset()
    {
        ReadyToFire = true;
        CurrentAmmoCount = StartingAmmo;
        CurrentAmmoMax = AmmoMax;
        WeaponController.UpdateAmmo();
    }

    // Used to add a delay between shots
    protected void SetReadyToFire ()
    {
        ReadyToFire = true;
    }

    // Is called to reload the weapon
    protected void Reload()
    {
        if (CurrentAmmoMax != 0 &&
            CurrentAmmoCount != StartingAmmo)
        {
            CurrentAmmoCount += StartingAmmo - CurrentAmmoCount;
            CurrentAmmoMax -= StartingAmmo - CurrentAmmoCount;
        }
    }

    // Updates the ammo so that it is reduced appropriately
    protected void UpdateAmmo ()
    {
        CurrentAmmoCount -= AmmoPerShot;
    }
}

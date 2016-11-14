using UnityEngine;

// Basic weapon
public class Pistol : BaseWeapon
{
    #region Variables
    public LayerMask Mask = -1;

    public float SpreadAngle;
    public int Damage;

    private PlayerController PlayerScript;
    #endregion

    // Use this for initialization
    void Start ()
    {
        // Get PlayerController
        WeaponController = gameObject.GetComponentInParent<ControlActiveWeapons>();
        PlayerId = WeaponController.PlayerId;
        ButtonToFire = ButtonToFire + PlayerId.ToString();
        ButtonToReload = ButtonToReload + PlayerId.ToString();

        PlayerCamera = gameObject.transform.parent.transform.parent.GetComponentInChildren<Camera>();

        Source = gameObject.GetComponent<AudioSource>();
        Source.clip = ShootSound;
        CurrentAmmoCount = StartingAmmo;
        CurrentAmmoMax = AmmoMax;
        WeaponController.UpdateAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            float Axis = Input.GetAxisRaw(ButtonToFire);
            if (Axis < -0.1 &&
                ReadyToFire &&
                CurrentAmmoCount > 0)
            {
                Shoot();
                UpdateAmmo();
                WeaponController.UpdateAmmo();
            }

            if (Input.GetButtonDown(ButtonToReload))
            {
                Reload();
            }
        }
    }

    // The method responsible for actually doing the hit test using the appropriate method
    public sealed override void Shoot()
    {
        RaycastHit hit;

        Vector3 fireDirection = PlayerCamera.transform.forward;
        Quaternion fireQuaternion = Quaternion.LookRotation(fireDirection);
        Quaternion randomRotation = Random.rotation;

        fireQuaternion = Quaternion.RotateTowards(fireQuaternion, randomRotation, Random.Range(0.0f, SpreadAngle));

        Source.Play();
        ReadyToFire = false;
        Invoke("SetReadyToFire", DelayOnFire);

        if (Physics.Raycast(PlayerCamera.transform.position, fireQuaternion * Vector3.forward, out hit, Mathf.Infinity, Mask))
        {
            GunHit gunHit = new GunHit();
            gunHit.DamageDealt = Damage;
            gunHit.Hit = hit;
            hit.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);
        }
    }
}

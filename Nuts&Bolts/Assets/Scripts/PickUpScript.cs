using UnityEngine;
using System.Collections;

public class PickUpData
{
    public PickUpScript.PickUpType Type;
    public int Value;
}

public class PickUpScript : MonoBehaviour
{
    public enum PickUpType { Health, Scrap,
        FireAmmo, IceAmmo, ShotgunAmmo, SMGAmmo };
    public enum AxisToRotate { X, Y, Z };

    #region Fields
    public PickUpType TypeOfPickup;
    public int HealthAmount;
    public int ScrapAmount;

    public float RotationAngleSpeed;
    public AxisToRotate RotationAxis;
    public float RespawnTimer;

    private Vector3 originalRotation;
    #endregion

    // USed for initialization
    void Start()
    {
        originalRotation = transform.eulerAngles;
    }

    // Called when colliding with something
    void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.collider.gameObject.tag == "Player" &&
            this.isActiveAndEnabled)
        {
            // Get the component
            PlayerChar playerScript = CollisionInfo.collider.gameObject.GetComponent<PlayerChar>();
            PickUpData data = new PickUpData();
            data.Type = TypeOfPickup;
            if (TypeOfPickup == PickUpType.Health)
            {
                data.Value = HealthAmount;
            }
            else if (TypeOfPickup == PickUpType.Scrap)
            {
                data.Value = ScrapAmount;
            }

            playerScript.SendMessage("GetPickUp", data);
            gameObject.SetActive(false);
            Invoke("Activate", RespawnTimer);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 Axis = new Vector3();
        if (RotationAxis == AxisToRotate.X)
        {
            Axis = new Vector3(1, 0, 0);
        }
        else if(RotationAxis == AxisToRotate.Y)
        {
            Axis = new Vector3(0, 1, 0);
        }
        else
        {
            Axis = new Vector3(0, 0, 1);
        }

        float rotation = RotationAngleSpeed * Time.deltaTime;
        transform.Rotate(Axis, rotation);
	}

    // Makes this item active again
    void Activate ()
    {
        gameObject.transform.eulerAngles = originalRotation;
        gameObject.SetActive(true);
    }
}

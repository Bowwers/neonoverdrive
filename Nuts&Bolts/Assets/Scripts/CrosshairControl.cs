using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    #region Variables
    public Texture2D CrosshairImage;
    public float CentreOfScreenX;
    public float CentreOfScreenY;

    Camera PlayerCamera;
    Rect CrosshairLocation;
    #endregion

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        PlayerCamera = gameObject.GetComponentInChildren<Camera>();

        // Gotta convert from normalized to screen coordinates
        Vector2 bottomLeftCorner = PlayerCamera.ViewportToScreenPoint(new Vector3(0, 0, PlayerCamera.nearClipPlane));
        Vector2 topRightCorner = PlayerCamera.ViewportToScreenPoint(new Vector3(1, 1, PlayerCamera.nearClipPlane));

        CentreOfScreenX = ((topRightCorner.x - bottomLeftCorner.x) / 2) - (CrosshairImage.width / 2);
        CentreOfScreenY = ((topRightCorner.y - bottomLeftCorner.y) / 2) - (CrosshairImage.height / 2);
        CrosshairLocation = new Rect(CentreOfScreenX, CentreOfScreenY, CrosshairImage.width, CrosshairImage.height);
    }

    /// <summary>
    /// Used to show the crosshair
    /// </summary>
    void OnGUI()
    {
        GUI.DrawTexture(CrosshairLocation, CrosshairImage);
    }

    // Update is called once per frame
    void Update()
    {
        // If we need any animation stuff...
    }

    void OnDestroy ()
    {
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }
}

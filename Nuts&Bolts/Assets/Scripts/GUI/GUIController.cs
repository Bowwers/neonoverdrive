using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
    #region GUI_Change_Notification
    public delegate void GUIFieldChangedEventHandler(object Sender,
                                                     GUIElementChangedEventArgs E);
    public event GUIFieldChangedEventHandler OnGUIFieldChange;
    #endregion



    // Use this for initialization
    void Start ()
    {
	
	}

    // Use to retrieve 
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

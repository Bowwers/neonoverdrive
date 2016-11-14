using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SwitchMenus : MonoBehaviour
{
    #region Fields
    public string RootMenu;

    List<Canvas> MenuCanvas = new List<Canvas>();
    GameObject currentActiveMenu;
    #endregion

    // Used to get all the menu canvas in the scene
    void Awake ()
    {
        Object[] canvases = Object.FindObjectsOfType<Canvas>();
        foreach (Object obj in canvases)
        {
            MenuCanvas.Add(obj as Canvas);
        }

        // Hides all menus, except for the top one
        foreach (Canvas canvas in MenuCanvas)
        {
            if (canvas.gameObject.name != RootMenu)
            {
                canvas.gameObject.SetActive(false);
            }
            else
            {
                currentActiveMenu = canvas.gameObject;
            }
        }
    }

    // Switches the menus that are shown ATM
    public void HideMenu (string MenuNameActivated)
    {
        currentActiveMenu.SetActive(false);
        currentActiveMenu = MenuCanvas.Where(x => x.name == MenuNameActivated).First().gameObject;
        currentActiveMenu.SetActive(true);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoadTimer : MonoBehaviour {
    public string levelToLoad;
    public float timer = 5f;
    public Image loadingBar;
    public float increaseAmount;
    

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        loadingBar.fillAmount += increaseAmount * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <=0)
        {
           
            Application.LoadLevel(levelToLoad);
        }
	}
}

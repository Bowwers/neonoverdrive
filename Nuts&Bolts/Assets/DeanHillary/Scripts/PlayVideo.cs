using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayVideo : MonoBehaviour {

    public MovieTexture movie;

    RawImage rawImageComp;
 

    void Start ()
    {
        rawImageComp = GetComponent<RawImage>();
       

        PlayClip();
    }

    void PlayClip()
    {
        rawImageComp.texture = movie;
        movie.Play();

    }

}

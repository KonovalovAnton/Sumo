using UnityEngine;
using System.Collections;

public class BGMStart : MonoBehaviour {

    public Console cons;
    //public AudioClip aud;
    public AudioSource source;
    private bool playing;

	// Use this for initialization
	void Start () {
 //       source.Play();
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (cons.ready)
        {
            if (!playing) {
                source.Play();
                playing = true;
            }
        }
        else
        {
            source.Stop();
            playing = false;
        }
	}
}

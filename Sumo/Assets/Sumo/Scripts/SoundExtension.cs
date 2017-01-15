using UnityEngine;
using System.Collections;

public class SoundExtension : MonoBehaviour
{

	SoundManager manager;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("_SoundManager").GetComponent<SoundManager> ();
	}

	void PlayKick ()
	{
		manager.PlayKick ();
	}

	void PlayJump ()
	{
		manager.PlayJump ();
	}

	void PlayLanding ()
	{
		manager.PlayLanding ();
	}
}

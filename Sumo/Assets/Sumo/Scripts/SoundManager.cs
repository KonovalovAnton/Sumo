using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

	public AudioSource audioSource;
	public AudioClip[] sounds;

	public void Start ()
	{
	}

	public void PlayGong ()
	{
		AudioSource.PlayClipAtPoint (sounds [0], this.gameObject.transform.position);
	}

	public void PlayKick ()
	{
		AudioSource.PlayClipAtPoint (sounds [1], this.gameObject.transform.position);
	}

	public void PlayJump ()
	{
		AudioSource.PlayClipAtPoint (sounds [2], this.gameObject.transform.position);
	}

	public void PlayLanding ()
	{
		AudioSource.PlayClipAtPoint (sounds [3], this.gameObject.transform.position);
	}

	public void PlayAttacked ()
	{
		AudioSource.PlayClipAtPoint (sounds [4], this.gameObject.transform.position, 0.3f);
	}

	public void PlayEat ()
	{
		AudioSource.PlayClipAtPoint (sounds [5], this.gameObject.transform.position);
	}

	public void PlayStunned ()
	{
		AudioSource.PlayClipAtPoint (sounds [6], this.gameObject.transform.position, 0.1f);
	}

    public void PlayBGM()
    {
        AudioSource.PlayClipAtPoint(sounds[7], this.gameObject.transform.position, 0.1f);
    }
	
}

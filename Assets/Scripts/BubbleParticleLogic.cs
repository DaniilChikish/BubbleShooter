using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BubbleParticleLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (gameObject.GetComponent<ParticleSystem>().emission.enabled)
	    {
	        gameObject.GetComponent<AudioSource>().Play();
	    }
    }
}

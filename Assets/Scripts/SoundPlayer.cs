using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer sm;
    public AudioSource audioUse;

    public AudioClip placeCard;
    // Start is called before the first frame update
    void Start()
    {
        sm = this;
        audioUse = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceCardSound()
    {
        audioUse.PlayOneShot(placeCard, 1f);
    }
}

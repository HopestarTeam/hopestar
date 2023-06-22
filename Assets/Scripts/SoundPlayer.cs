using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer sm;
    public AudioSource audioUse;

    public AudioClip placeCard;

    public AudioClip scrollDeck;

    public AudioClip endTurn;
    public AudioClip gameOver;
    public AudioClip victory;


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

    public void ScrollDeckSound()
    {
        audioUse.PlayOneShot(scrollDeck, 1f);
    }

    public void EndTurnSound()
    {
        audioUse.PlayOneShot(endTurn, 1f);
    }

    public void GameOverSound()
    {
        audioUse.PlayOneShot(gameOver, 1f);
    }

    public void VictorySound()
    {
        audioUse.PlayOneShot(victory, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScroll : MonoBehaviour
{
    [SerializeField] AnimationCurve upCurve, forwardCurve;
    DeckBehaviour deck;
    float scrollValue = 0f, maxZ = 5f, minZ = -5f, maxY = 2f, minY = 0f, maxX = -1f, minX = 0f, posy, posz, posx;
    public GameObject CurrentTop;


    private void Start() {
        deck = GetComponent<DeckBehaviour>();
    }

    public void ArrangeCards()
    {
        float placer = 1f/deck.cardObjects.Count/2f;
        float checker = 1f;
        foreach(GameObject cardGO in deck.cardObjects)
        {
            posy = Mathf.Lerp(minY,maxY,upCurve.Evaluate(placer));
            posz = Mathf.Lerp(minZ,maxZ,forwardCurve.Evaluate(placer));
            posx = Mathf.Lerp(minX,maxX,forwardCurve.Evaluate(placer));
            cardGO.transform.position = new Vector3(transform.position.x + posx,
                                        transform.position.y + posy,
                                        transform.position.z + posz);
            placer += (1f/deck.cardObjects.Count);
            if(checker > Mathf.Abs(placer - 0.5f))
            {
                checker = Mathf.Abs(placer - 0.5f);
                CurrentTop = cardGO;
            }
        }
    }

    float oldValue = -1;
    void Update()
    {
        scrollValue = scrollValue + -Input.mouseScrollDelta.y / (deck.cardObjects.Count);
        if(scrollValue != oldValue)
        {
            oldValue = scrollValue;
            float cardSpace = 1f/deck.cardObjects.Count;
            scrollValue = Mathf.Clamp(scrollValue, -0.5f + cardSpace/2, 0.5f - cardSpace/2);
            float placer = Mathf.Clamp(cardSpace/2 + scrollValue, -1f + cardSpace, 1f - cardSpace);
            float checker = 1f;
            foreach(GameObject cardGO in deck.cardObjects)
            {
                cardGO.GetComponent<BoxCollider>().enabled = false;
                if(checker >= Mathf.Abs(placer - 0.5f))
                {
                    checker = Mathf.Abs(placer - 0.5f);
                    CurrentTop = cardGO;
                    //Debug.Log(cardGO.name + checker);
                }
                posy = Mathf.Lerp(minY,maxY,upCurve.Evaluate(placer));
                posz = Mathf.Lerp(minZ,maxZ,forwardCurve.Evaluate(placer));
                posx = Mathf.Lerp(minX,maxX,forwardCurve.Evaluate(placer));
                cardGO.transform.position = new Vector3(transform.position.x + posx,
                                            transform.position.y + posy,
                                            transform.position.z + posz);
                placer += (1f/deck.cardObjects.Count);
                placer = Mathf.Clamp(placer, -1f + cardSpace, 1f - cardSpace);
            }
            CurrentTop.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

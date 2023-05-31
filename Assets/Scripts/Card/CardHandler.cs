using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public CardSO properties;
    MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        switch (properties.cardType)
        {
            case CardTypeDefinition.CardType.PRODUCTION:
                mesh.material.color = Color.yellow;
                break;
            default:
                mesh.material.color = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHandler : MonoBehaviour
{
    public CardSO properties;

    [SerializeField] TextMeshProUGUI cardName;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        cardName.text = properties.cardName;
        switch (properties.cardType)
        {
            case CardType.FOOD:
                mesh.material.color = Color.green;
                break;
            case CardType.ENERGY:
                mesh.material.color = Color.blue;
                break;
            case CardType.RAW:
                mesh.material.color = Color.red;
                break;
            case CardType.INDUSTRY:
                mesh.material.color = Color.gray;
                break;
            case CardType.URBAN:
                mesh.material.color = Color.yellow;
                break;
            default:
                mesh.material.color = Color.magenta;
                break;
        }
        ResolveCard();
    }

    public void ResolveCard()
    {
        if(properties.hasFunction)
        {
            switch (properties.functionType)
            {
                case CardSO.FunctionType.IF:
                    IfTileCardSO iCard = (IfTileCardSO)properties;
                    if(iCard.CheckCardIf())
                    {

                    }
                    break;
                case CardSO.FunctionType.TIMER:
                    if(properties.cardTimer == 0)
                    {
                        TimerCardSO tCard = (TimerCardSO)properties;
                        tCard.RunFunction();
                    }
                    break;
                default:

                    break;
            }
        }
        properties.RunCard();
        
    }
}

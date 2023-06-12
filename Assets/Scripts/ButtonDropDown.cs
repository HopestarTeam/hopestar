using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonDropDown : MonoBehaviour, IPointerDownHandler
{
    bool isExtended = false;
    [SerializeField] TextMeshProUGUI buttonText;
    


    public void WasClicked(){
        isExtended = !isExtended;
        if (!isExtended) {buttonText.text = "Extend";}
        if (isExtended) {buttonText.text = "Retract";}
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        
    }

    private void Awake() {
        buttonText.text = "Extend";

    }

    private void Update() {
        
    }



}

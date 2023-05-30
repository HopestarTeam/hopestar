using System;
using UnityEngine.UIElements;
using UnityEngine;

[Serializable]
public class MenuManager
{
    public UIDocument Hud;
    public bool OnElement;

    public void Initialize()
    {
        Hud.rootVisualElement.Query(className: "GUIBackground").ForEach( (element) => 
        {
            element.RegisterCallback<MouseEnterEvent>(e => OnElement = true);
            element.RegisterCallback<MouseLeaveEvent>(e => OnElement = false);  
        });
    }
    
}

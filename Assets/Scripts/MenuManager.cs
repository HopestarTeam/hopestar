using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

[Serializable]
public class MenuManager
{
    public UIDocument Hud;
    public ToolTip toolTip;
    public bool OnElement;

    public void Initialize()
    {
        Hud.rootVisualElement.Query(className: "GUIBackground").ForEach( (element) => 
        {
            element.RegisterCallback<MouseEnterEvent>(e => OnElement = true);
            element.RegisterCallback<MouseLeaveEvent>(e => OnElement = false);  
        });
        toolTip = new ToolTip("Test",Input.mousePosition);
        Hud.rootVisualElement.Add(toolTip);
    }
    
    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mousePositionCorrected = new Vector2(Screen.width - mousePosition.x, Screen.height - mousePosition.y);
        mousePositionCorrected = RuntimePanelUtils.ScreenToPanel(Hud.rootVisualElement.panel,mousePositionCorrected);
        toolTip.screenPosition = mousePositionCorrected;
    }
}

public class ToolTip : Label
{
    Vector2 m_ScreenPosition;

    public Vector2 screenPosition
    {
        get
        {
            return m_ScreenPosition;
        }
        set
        {
            m_ScreenPosition = value;
            Debug.Log(screenPosition);
            this.style.top = m_ScreenPosition.y;
            this.style.right = m_ScreenPosition.x;
        }
    }

    public ToolTip(string text, Vector2 screenPosition) : base(text)
    {
        this.visible = false;
        style.position = new StyleEnum<Position>(Position.Absolute);
        m_ScreenPosition = screenPosition;
        m_ScreenPosition.y = Screen.height - screenPosition.y;
        style.top = m_ScreenPosition.y;
        style.right = m_ScreenPosition.x;
        usageHints = UsageHints.DynamicTransform;
        AddToClassList("tooltip");
    }
}

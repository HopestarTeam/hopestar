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

    //TODO: Someone please fix this code.  It has way too many lines
    public void ShowInfoScreen(GlobalVariables variables)
    {
        VisualElement InfoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        InfoScreen.SetEnabled(true);
        InfoScreen.visible = true;
        VisualElement StatsContainer = InfoScreen.Query(name:"Stats");

        TextElement co2 = StatsContainer.Q<TextElement>(name:"CO2");
        co2.text = $"CO<sup>2</sup>: {variables.CO2}";
        TextElement happiness = StatsContainer.Q<TextElement>(name:"Happiness");
        happiness.text = $"Happiness: {variables.CitizenUnrest}";
        TextElement resources = StatsContainer.Q<TextElement>(name:"Resources");
        resources.text = $"Resources: {variables.RawResources}";
        TextElement food = StatsContainer.Q<TextElement>(name:"Food");
        food.text = $"Food: {variables.Food}";
        TextElement energy = StatsContainer.Q<TextElement>(name:"Energy");
        energy.text = $"Energy: {variables.Energy}";
        TextElement consumerGoods = StatsContainer.Q<TextElement>(name:"ConsumerGoods");
        consumerGoods.text = $"Consumer Goods {variables.ConsumerGoods}";
        TextElement industryGoods = StatsContainer.Q<TextElement>(name:"IndustryGoods");
        industryGoods.text = $"Industry Goods: {variables.IndustryGoods}";
        //Yet another thing that I could've used loops and arrays for if my foresight were as good as my hindsight

        Button closeButton = InfoScreen.Q<Button>();
        closeButton.clicked -= HideInfoScreen;
        closeButton.clicked += HideInfoScreen;
    }

    public void HideInfoScreen()
    {
        VisualElement InfoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        InfoScreen.SetEnabled(false);
        InfoScreen.visible = false;
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
           // Debug.Log(screenPosition);
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

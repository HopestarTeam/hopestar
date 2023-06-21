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

    public Dictionary<GlobalVariableEnum, string> GlobalVariableNames = new Dictionary<GlobalVariableEnum, string>()
    {
        {GlobalVariableEnum.Material, "Materials"},
        {GlobalVariableEnum.Consumer, "Consumer Goods"},
        {GlobalVariableEnum.Energy, "Energy"},
        {GlobalVariableEnum.Industry, "Industry Goods"},
        {GlobalVariableEnum.Science, "Science"}
    };


    //TODO: Someone please fix this code.  It has way too many lines
    public void ShowInfoScreen(GlobalVariables variables)
    {
        VisualElement infoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        infoScreen.SetEnabled(true);
        infoScreen.visible = true;
        VisualElement statsContainer = infoScreen.Query(name:"Stats");

        statsContainer.Clear();

        foreach(KeyValuePair<GlobalVariableEnum,GlobalVariables.ResourceVariable> current in variables.variables)
        {
            Label currentLabel = new Label()
            {
                text = $"{GlobalVariableNames.GetValueOrDefault(current.Key,current.Key.ToString())}: {current.Value.production}"
            };
            statsContainer.Add(currentLabel);
        }
        //Yet another thing that I could've used loops and arrays for if my foresight were as good as my hindsight

        Button closeButton = infoScreen.Q<Button>();
        closeButton.clicked -= HideInfoScreen;
        closeButton.clicked += HideInfoScreen;
    }

    public void HideInfoScreen()
    {
        VisualElement InfoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        InfoScreen.SetEnabled(false);
        InfoScreen.visible = false;
    }

    public void UpdateHud()
    {
        GlobalVariables variables = GameManager.gm.variables;
        /*
        VisualElement CurrentContainer = Hud.rootVisualElement.Query(name:"CurrentResources");
        VisualElement UpkeepContainer = Hud.rootVisualElement.Query(name:"UpkeepResources");
        TextElement emission = CurrentContainer.Q<TextElement>(name:"EmissionText");
        TextElement energy = CurrentContainer.Q<TextElement>(name:"EnergyText");
        TextElement raw = CurrentContainer.Q<TextElement>(name:"RawText");
        TextElement food = CurrentContainer.Q<TextElement>(name:"FoodText");
        TextElement consumer = CurrentContainer.Q<TextElement>(name:"ConsumerText");
        TextElement industry = CurrentContainer.Q<TextElement>(name:"industryText");
        TextElement happiness = UpkeepContainer.Q<TextElement>(name:"HappinessText");
        TextElement energyUpkeep = UpkeepContainer.Q<TextElement>(name:"EnergyUpkeepText");
        TextElement rawUpkeep = UpkeepContainer.Q<TextElement>(name:"RawUpkeepText");
        TextElement foodUpkeep = UpkeepContainer.Q<TextElement>(name:"FoodUpkeepText");
        TextElement consumerUpkeep = UpkeepContainer.Q<TextElement>(name:"ConsumerUpkeepText");
        TextElement industryUpkeep = UpkeepContainer.Q<TextElement>(name:"industryUpkeepText");

        emission.text = $"Emission: {variables.variables[GlobalVariableEnum.CO2].production}";
        energy.text = $"Energy: {variables.variables[GlobalVariableEnum.Energy].production}";
        raw.text = $"Raw Resources: {variables.variables[GlobalVariableEnum.Material].production}";
        food.text = $"Food: {variables.variables[GlobalVariableEnum.Food].production}";
        consumer.text = $"Consumer Goods: {variables.variables[GlobalVariableEnum.Consumer].production}";
        industry.text = $"industry Goods: {variables.variables[GlobalVariableEnum.Industry].upkeep}";

        energyUpkeep.text = $"Energy Upkeep: {variables.variables[GlobalVariableEnum.Energy].upkeep}";
        rawUpkeep.text = $"Raw Resources Upkeep: {variables.variables[GlobalVariableEnum.Material].upkeep}";
        foodUpkeep.text = $"Food Upkeep: {variables.variables[GlobalVariableEnum.Food].upkeep}";
        consumerUpkeep.text = $"Consumer Goods Upkeep: {variables.variables[GlobalVariableEnum.Consumer].upkeep}";
        industryUpkeep.text = $"Industry Goods Upkeep: {variables.variables[GlobalVariableEnum.Industry].upkeep}";
    */
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

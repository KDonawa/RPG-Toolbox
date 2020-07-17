using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TabGroup : MonoBehaviour
{
    //public List<TabButton> tabButtons;
    //TabButton[] tabButtons;

    [Header("UI/Prefabs")]
    [SerializeField] TabButton tabButtonPrefab;

    [Header("Default State")]
    [SerializeField] Color tabDefaultColor = new Color();
    [SerializeField] float tabDefaultSize = 24f;

    [Header("Hover State")]
    [SerializeField] Color tabHoverColor = new Color();
    [SerializeField] float tabHoverSize = 24f;

    [Header("Selected State")]  
    [SerializeField] Color tabSelectedColor = new Color();
    [SerializeField] float tabSelectedSize = 28f;

    List<TabButton> _tabButtons;
    TabButton _selectedTab;

    public delegate void TabButtonSelectedDelegate(string filter);
    TabButtonSelectedDelegate tabButtonSelected;

    private void Awake()
    {
        _tabButtons = new List<TabButton>();
    }    
    void AddTabButton(string name)
    {
        TabButton newTab = Instantiate(tabButtonPrefab, transform);
        newTab.InitializeTab(name, this);
        _tabButtons.Add(newTab);        
    }
    public void InitilalizeTabGroup(string[] tabNames, TabButtonSelectedDelegate tabButtonSelectedDelegate)
    {
        foreach (string name in tabNames) AddTabButton(name);

        tabButtonSelected = tabButtonSelectedDelegate;

        if (_tabButtons.Count > 0) OnTabSelected(_tabButtons[0]);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(_selectedTab == null || _selectedTab != button)
        {
            button.Text.color = tabHoverColor;
            button.Text.fontSize = tabHoverSize;
        }        
    }
    public void OnTabExit()
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        if (_selectedTab == button) return;

        _selectedTab = button;
        ResetTabs();
        button.Text.color = tabSelectedColor;
        button.Text.fontSize = tabSelectedSize;
        tabButtonSelected(button.Text.text);
    }
    void ResetTabs()
    {
        foreach (var button in _tabButtons)
        {
            if (_selectedTab != null && _selectedTab == button) continue;

            button.Text.color = tabDefaultColor;
            button.Text.fontSize = tabDefaultSize;            
        }
    }
}

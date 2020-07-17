using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    TabGroup _tabGroup;
    TextMeshProUGUI _tabText;

    public TextMeshProUGUI Text => _tabText;

    public void OnPointerClick(PointerEventData eventData) => _tabGroup.OnTabSelected(this);

    public void OnPointerEnter(PointerEventData eventData) => _tabGroup.OnTabEnter(this);

    public void OnPointerExit(PointerEventData eventData) => _tabGroup.OnTabExit();

    public void InitializeTab(string name, TabGroup tabGroup)
    {        
        _tabText = GetComponent<TextMeshProUGUI>();
        if (_tabText == null) return;

        _tabText.text = name;
        _tabGroup = tabGroup;

        //Debug.Log("init tab");
    }

}

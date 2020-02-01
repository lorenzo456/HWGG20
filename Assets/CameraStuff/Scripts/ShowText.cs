using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public Text text;
    public bool DisplayUpgradeText = true;
    public bool DisplayPickupText;
    public bool DisplayFullInvText;
    public bool DisplayRepairCarEnterCarText;
    private Color invis;
    private Color show;

    private void Start()
    {
        invis.a = 0f;
        show = new Color(0.7f, 0.3f, 0f, 255f);
    }

    void Update()
    {
        if (DisplayUpgradeText)
        {
            text.color = show;
            text.text = "Press Q to enter car";               
        }

        else if (DisplayPickupText)
        {
            text.color = show;
            text.text = "Press E to pickup scrap";
        }

        else if (DisplayFullInvText)
        {
            text.color = show;
            text.text = "Inventory full, can't pick up scrap";
        }

        else if (DisplayRepairCarEnterCarText)
        {
            text.color = show;
            text.text = "Press E to repair car or Q to enter car";
        }

        else
        {
            text.color = invis;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlighter : MonoBehaviour {

    public Color highlightColor;

    private MenuButton activeButton;

    public void SetButtonActive(MenuButton button)
    {
        // reset previous button color
        if(activeButton != null)
        {
            activeButton.buttonBackground.color = activeButton.startButtonColor;
        }

        // set new button color
        button.buttonBackground.color = highlightColor;
        activeButton = button;
    }

}

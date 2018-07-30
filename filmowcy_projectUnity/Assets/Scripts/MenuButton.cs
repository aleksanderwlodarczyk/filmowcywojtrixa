using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    private Transform leftPanelTransform;
    private Button playButton;
    private Text leftLevelTitle;
    private Text leftLevelScore;
    private Image leftLevelScreen;

    private Text buttonLevelTitle;
    private Text buttonLevelScore;

    private SceneManaging scenes;

    private ButtonHighlighter highlighter;
    
    public Image buttonBackground;
    public Color startButtonColor;

    public string levelTitle;
    public int levelScore;
    public int levelID;
    
    [SerializeField]
    private Sprite levelScreen;

    private void Start()
    {
        // find highlighter
        highlighter = GameObject.FindObjectOfType<ButtonHighlighter>().GetComponent<ButtonHighlighter>();

        // remember what color of level button is at start
        buttonBackground = gameObject.transform.Find("frame").gameObject.GetComponent<Image>();
        startButtonColor = buttonBackground.color;

        scenes = GameObject.Find("scenes").GetComponent<SceneManaging>();

        // find button textes
        buttonLevelScore = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
        buttonLevelTitle = gameObject.transform.Find("level_title").gameObject.GetComponent<Text>();

        // set button textes
        buttonLevelTitle.text = levelTitle;

        // find left frame objects
        leftPanelTransform = GameObject.Find("left_frame").transform;
        leftLevelTitle = leftPanelTransform.Find("title").gameObject.GetComponent<Text>();
        leftLevelScore = leftPanelTransform.Find("score").gameObject.GetComponent<Text>();
        leftLevelScreen = leftPanelTransform.Find("level_screen").gameObject.GetComponent<Image>();
        playButton = leftPanelTransform.Find("playButton").gameObject.GetComponent<Button>();

    }

    public void ButtonClicked()
    {
        // highlight the button
        highlighter.SetButtonActive(this);

        // remove listeners from previous buttons
        playButton.onClick.RemoveAllListeners();

        // set left frame objects
        leftLevelScore.text = levelScore.ToString();
        leftLevelTitle.text = levelTitle;
        leftLevelScreen.overrideSprite = levelScreen;

        // set play button
        playButton.interactable = true;
        playButton.onClick.AddListener(LoadMyLevel);
    }

    public void LoadMyLevel()
    {
        scenes.LoadLevel(levelID);
    }
}

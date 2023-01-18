using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] 
    private Button pauseButton;
    [SerializeField] 
    private GameObject pausePopup;
    [SerializeField] 
    private Text aboutControl;

    [Header("Controls buttons")] 
    [SerializeField]
    private Button swipeButton;
    [SerializeField]
    private Button dragButton;
    [SerializeField]
    private Button keyboardButton;

    private bool pause;

    public static Action<ControlType> onChangeControlType;
    private Action updateTextAboutControl;

    private void Start()
    {
        pauseButton.onClick.AddListener(OpenOrClosePopup);
        
        swipeButton.onClick.AddListener(() => SetControlType(ControlType.Swipe));
        dragButton.onClick.AddListener(() => SetControlType(ControlType.Drag));
        keyboardButton.onClick.AddListener(() => SetControlType(ControlType.Keyboard));

        UpdateTextAboutControl();
        updateTextAboutControl += UpdateTextAboutControl;
    }

    private void OpenOrClosePopup()
    {
        pausePopup.SetActive(!pause);
        Time.timeScale = pause ? 1f : 0f;
        pause = !pause;
    }

    private void SetControlType(ControlType type)
    {
        PlayerPrefs.SetInt(Constants.InputTypeIndex, Constants.controlsDictionary.First(x => x.Value == type).Key);
        onChangeControlType?.Invoke(type);
        updateTextAboutControl?.Invoke();
    }

    private void UpdateTextAboutControl()
    {
        aboutControl.text = PlayerPrefs.HasKey(Constants.InputTypeIndex) ? 
            Constants.controlsDictionary.First(x => x.Key == PlayerPrefs.GetInt(Constants.InputTypeIndex)).Value.ToString(): 
            Constants.ControlsType.Keyboard;
    }
    
    private void OnDestroy()
    {
        pauseButton.onClick.RemoveAllListeners();
        
        swipeButton.onClick.RemoveAllListeners();
        dragButton.onClick.RemoveAllListeners();
        keyboardButton.onClick.RemoveAllListeners();
        
        updateTextAboutControl -= UpdateTextAboutControl;
    }
}

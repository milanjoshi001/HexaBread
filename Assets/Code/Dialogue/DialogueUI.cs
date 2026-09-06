using System.Collections.Generic;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Singleton<DialogueUI>
{
    [SerializeField] private DialogueData _testDialogueData;
    [SerializeField] private GameObject _dialoguePanelRoot;
    [Header("Left")]
    [SerializeField] private Image _leftCharacterImage;
    [SerializeField] private TextMeshProUGUI _leftCharacterName;
    [SerializeField] private TextMeshProUGUI _leftDialogue;
    [SerializeField] private Transform _leftContainer;
    
    [Header("Right")]
    [SerializeField] private Image _rightCharacterImage;
    [SerializeField] private TextMeshProUGUI _rightCharacterName;
    [SerializeField] private TextMeshProUGUI _rightDialogue;
    [SerializeField] private Transform _rightContainer;
    
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _skipButton;

    private DialogueContentData _currentDialogueContent;
    private List<DialogueContentData> _dialogueContentDatas = new ();
    
    private void Start()
    {
        _continueButton.onClick.AddListener(ContinueButton);
        _skipButton.onClick.AddListener(SkipButton);
        
        //Only for testing purpose
        OpenDialogue(_testDialogueData);
    }

    public void OpenDialogue(DialogueData dialogueData)
    {
        _dialoguePanelRoot.SetActive(true);
        _dialogueContentDatas.AddRange(dialogueData.DialogueContentDatas);
        _currentDialogueContent = _dialogueContentDatas[0];
        LoadDialogueData(_currentDialogueContent);
    }

    public void CloseDialogue()
    {
        _dialogueContentDatas.Clear();
        _currentDialogueContent = null;
        _dialoguePanelRoot.SetActive(false);
    }

    private void SkipButton()
    {
        CloseDialogue();
    }
    
    private void ContinueButton()
    {
        for (int i = 0; i < _dialogueContentDatas.Count; i++)
        {
            if (_dialogueContentDatas[i] == _currentDialogueContent)
            {
                if (i == _dialogueContentDatas.Count - 1)
                {
                    CloseDialogue();
                    return;
                }

                _currentDialogueContent = _dialogueContentDatas[i + 1];
                LoadDialogueData(_currentDialogueContent);
                return;
            }
        }
    }

    private void LoadDialogueData(DialogueContentData dialogueContentData)
    {
        switch (dialogueContentData.DialoguePosition)
        {
            case DialoguePosition.Left:
                _leftCharacterImage.sprite = dialogueContentData.CharacterSprite;
                _leftCharacterName.SetText(dialogueContentData.CharacterName);
                _leftDialogue.SetText(dialogueContentData.DialogueLine);
                _leftContainer.gameObject.SetActive(true);
                _rightContainer.gameObject.SetActive(false);
                break;
            case DialoguePosition.Right:
                _rightCharacterImage.sprite = dialogueContentData.CharacterSprite;
                _rightCharacterName.SetText(dialogueContentData.CharacterName);
                _rightDialogue.SetText(dialogueContentData.DialogueLine);
                _rightContainer.gameObject.SetActive(true);
                _leftContainer.gameObject.SetActive(false);
                break;
        }
    }
}

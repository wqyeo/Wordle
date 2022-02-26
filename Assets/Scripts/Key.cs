using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class Key : MonoBehaviour {

    public delegate char OnKeyPress();

    public OnKeyPress onKeyPressCallback;

    private Button button;

    public char Character {
        get; private set;
    }

    private void Start() {
        button = GetComponent<Button>();

        try {
            AssignCharacterByText();
            AddButtonCallback();

        } catch (Exception e) {
            Debug.LogError($"Key.cs :: {e.Message}");
        }

        #region Local_Function
        void AddButtonCallback() {
            button.onClick.AddListener(delegate {
                onKeyPressCallback.Invoke();
            });
        }

        void AssignCharacterByText() {
            var text = GetComponentInChildren<TextMeshProUGUI>();
            Character = text.text[0];
        }
        #endregion
    }
}
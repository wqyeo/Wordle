using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : Singleton<InputManager> {

    public delegate void OnCharacterPressed(char character);
    public delegate void OnEnterPressed();
    public delegate void OnBackspacePressed();

    public OnCharacterPressed onCharacterPressedCallback;
    public OnEnterPressed onEnterPressedCallback;
    public OnBackspacePressed onBackspacePressedCallback;

    [SerializeField]
    private Button enterButton;

    [SerializeField]
    private Button backspaceButton;

    private bool onDesktop;

    private KeyCode[] keyboardKeyCodes;

    private void Start() {
        Key[] keys = FindObjectsOfType<Key>();
        foreach (var key in keys) {
            key.onKeyPressCallback = CallbackCharacterPressed;
        }

        enterButton.onClick.AddListener(CallbackEnterPressed);
        backspaceButton.onClick.AddListener(CallbackBackspacePressed);

        onDesktop = DeviceType.Desktop == SystemInfo.deviceType;
        if (onDesktop) {
            GenerateKeyCodes();
        }
    }


    private void Update() {
        if (!onDesktop) {
            return;
        }

        CheckCharacterKeyPressesDesktop();

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            CallbackBackspacePressed();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            CallbackEnterPressed();
        }
    }

    private void CallbackCharacterPressed(char character) {
        onCharacterPressedCallback?.Invoke(character);
    }

    private void CallbackBackspacePressed(){
        onBackspacePressedCallback?.Invoke();
    }

    private void CallbackEnterPressed() {
        onEnterPressedCallback?.Invoke();
    }

    private void CheckCharacterKeyPressesDesktop() {
        foreach (var keycode in keyboardKeyCodes) {
            if (Input.GetKeyDown(keycode)) {
                CallbackCharacterPressed(keycode.ToString()[0]);
            }
        }
    }

    private void GenerateKeyCodes() {
        keyboardKeyCodes = new KeyCode[] {
            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z,
        };
    }
}

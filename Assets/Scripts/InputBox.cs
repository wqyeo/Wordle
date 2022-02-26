using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

[RequireComponent(typeof(Image))]
public class InputBox : MonoBehaviour {
    private Image imageRenderer;

    private TextMeshProUGUI text;

    public string CurrentText {
        get => text.text;
    }

    private void Start() {
        imageRenderer = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        text.text = string.Empty;

#if UNITY_EDITOR
        if (text == null) {
            Debug.LogError($"InputBox.cs ({gameObject.name}) :: InputBox have no TextMeshPro in children.");
        }
#endif
    }

    public void Type(string character) {
        text.text = character;
    }

    public void Empty() {
        text.text = string.Empty;
    }

    public void ChangeSprite(Sprite sprite) {
        imageRenderer.sprite = sprite;
    }
}

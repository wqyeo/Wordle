using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class InputBox : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    private TextMeshProUGUI text;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshProUGUI>();

#if UNITY_EDITOR
        if (text == null) {
            Debug.LogError($"InputBox.cs ({gameObject.name}) :: InputBox have no TextMeshPro in children.");
        }
#endif
    }
}

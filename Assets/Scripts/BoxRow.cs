using UnityEngine;

[System.Serializable]
public class BoxRow : MonoBehaviour {

    private InputBox[] boxRow;

    public InputBox[] Row {
        get => boxRow;
    }

    private void Start() {
        boxRow = GetComponentsInChildren<InputBox>();

#if UNITY_EDITOR
        if (boxRow.Length == 0) {
            Debug.LogWarning($"BoxRow.cs ({gameObject.name}) :: No InputBox in childrens.");
        }
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBoxManager : MonoBehaviour {

    [System.Serializable]
    public struct BoxSprites {
        [SerializeField, Tooltip("Totally correct")]
        private Sprite correctSprite;

        [SerializeField, Tooltip("Correct but wrong position")]
        private Sprite positionSprite;

        [SerializeField, Tooltip("Totally wrong")]
        private Sprite wrongSprite;

        public Sprite CorrectSprite {
            get => correctSprite;
        }
        public Sprite PositionSprite {
            get => positionSprite;
        }
        public Sprite WrongSprite {
            get => wrongSprite;
        }
    }

    [SerializeField]
    private BoxSprites boxSprites;

    public BoxSprites BoxSprite {
        get => boxSprites;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

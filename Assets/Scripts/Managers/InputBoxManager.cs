using UnityEngine;

public class InputBoxManager : MonoBehaviour {

    [System.Serializable]
    private struct BoxSprites {
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

    [SerializeField]
    private BoxRow[] boxRows;

    public Sprite GetSpriteByCharState(CharState charState) {
        switch (charState) {
            case CharState.CORRECT:
                return boxSprites.CorrectSprite;
            case CharState.POSITION:
                return boxSprites.PositionSprite;
            case CharState.WRONG:
                return boxSprites.WrongSprite;
        }

        Debug.LogWarning("InputBoxManager.cs :: Switch Case fallthrough.");
        return boxSprites.WrongSprite;
    }
}

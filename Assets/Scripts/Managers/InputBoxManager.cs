using UnityEngine;

public class InputBoxManager : Singleton<InputBoxManager> {

    private const int LETTER_COUNT = 5;

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

    private string currentTyping;

    private int currentRow;

    private int currentBoxIndex;

    private void Start() {
        currentTyping = string.Empty;
        currentRow = 0;
        currentBoxIndex = 0;

        InputManager.Instance.onCharacterPressedCallback = TypeOnBox;
        InputManager.Instance.onBackspacePressedCallback = RemoveLast;
        InputManager.Instance.onEnterPressedCallback = Submit;
    }

    private void Submit() {
        if (currentTyping.Length != LETTER_COUNT) {
            GameManager.Instance.FlashNotFullyFilled();
            return;
        }

        if (!GameManager.Instance.IsValidWord(currentTyping)) {
            GameManager.Instance.FlashInvalidWordWarn();
            return;
        }

        string correctWord = GameManager.Instance.CurrentWord;
        var currRow = boxRows[currentRow];

        for (int i = 0; i < LETTER_COUNT; ++i) {
            var currBox = currRow.Row[i];

            if (correctWord[i] == currentTyping[i]) {
                // Correct position and char.
                currBox.ChangeSprite(boxSprites.CorrectSprite);
            } else if (correctWord.Contains(currentTyping[i].ToString())) {
                // Contains, but wrong position
                currBox.ChangeSprite(boxSprites.PositionSprite);
            } else {
                currBox.ChangeSprite(boxSprites.WrongSprite);
            }
        }

        if (correctWord == currentTyping) {
            GameManager.Instance.ShowGameOver($"{correctWord} was the correct word!");
            return;
        }

        ++currentRow;
        if (currentRow >= boxRows.Length) {
            GameManager.Instance.ShowGameOver($"The correct word was {correctWord}.");
            return;
        }

        currentBoxIndex = 0;
        currentTyping = string.Empty;
    }

    private void RemoveLast() {
        var currRow = boxRows[currentRow];

        var currBox = currRow.Row[currentBoxIndex];

        if (currBox.CurrentText == string.Empty) {
            --currentBoxIndex;
            if (currentBoxIndex < 0) {
                // No more place to backspace on.
                currentBoxIndex = 0;
                return;
            }

            currBox = currRow.Row[currentBoxIndex];
        }

        currBox.Empty();
        currentTyping = currentTyping.Remove(currentTyping.Length - 1);

        Debug.Log(currentTyping);
    }

    private void TypeOnBox(char character) {
        var currRow = boxRows[currentRow];

        var currBox = currRow.Row[currentBoxIndex];

        if (currBox.CurrentText != string.Empty) {
            ++currentBoxIndex;
            if (currentBoxIndex >= currRow.Row.Length) {
                // End of input, can only backspace/submit.
                --currentBoxIndex;
                return;
            }

            currBox = currRow.Row[currentBoxIndex];
        }

        currBox.Type(character.ToString());
        currentTyping += character;

        Debug.Log($"({character}) {currentTyping}");
    }

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

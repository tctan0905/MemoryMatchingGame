using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using System;

public enum GameState
{
    PREVIEW = 0,
    PLAYING = 1,
    GAME_OVER = 2
}

public class GameController : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private GameObject cardPrefab; // Reference to the player prefab
    [SerializeField] private CardScriptable cardScriptable; // Reference to the scriptable object containing card data
    [SerializeField] private RectTransform cardGrid; // Reference to the parent transform for the cards
    [SerializeField] private RectTransform canvasUI; // Reference to the parent transform for the cards
    [SerializeField] private List<Card> listCard; // Reference to the parent transform for the cards
    [SerializeField] private GameState gameState; // Current state of the game
    [SerializeField] private Text txt_level;
    private Card cardFist;
    private Card cardSecond;
    int totalPairs; // Total number of pairs in the game
    int matchedPairs = 0; // Counter for matched pairs
    [SerializeField] private LevelData levelData;
    private List<CardData> cards = new List<CardData>();

    [Header("Timer Settings")]
    [SerializeField] Image timerImage; // Reference to the UI Image component for the timer
    [SerializeField] Text txtTime, txtTimePreview; // Reference to the UI Text component for displaying time
    private float remainingTime;
    private float remainingTimePreview = 3f; // Duration of the preview phase in seconds
    bool isGameStarted = false; // Flag to indicate if the game has started
    bool isGameOver = false; // Flag to indicate if the game is over

    int nextLevel = 0;
    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
        GameEvents.OnNextLevel -= NextLevel;
        GameEvents.OnNextLevel += NextLevel;

        // GameEvents.OnRestart -= Rese;
        // GameEvents.OnNextLevel += NextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.PREVIEW)
        {
            remainingTimePreview -= Time.deltaTime;
            txtTime.gameObject.SetActive(false);
            txtTimePreview.gameObject.SetActive(true);
            txtTimePreview.text = Mathf.CeilToInt(remainingTimePreview).ToString();
            if (remainingTimePreview <= 0f)
            {
                remainingTimePreview = 0f;
                txtTimePreview.gameObject.SetActive(false);
                StartGame();
            }
            timerImage.fillAmount = 1f;
        }
        else if (gameState == GameState.PLAYING && isGameStarted && !isGameOver)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            // Handle game over logic here
            remainingTime = 0;
            if (!isGameOver)
            {
                Gameover();
            }
        }
    }

    void LoadData()
    {
        Time.timeScale = 1f;
        LoadLevel();
        remainingTime = levelData.TimeLimit;
        gameState = GameState.PREVIEW;
        remainingTimePreview = 3f;
        var gridCard = cardGrid.GetComponent<GridLayoutGroup>();
        if (gridCard != null)
        {
            gridCard.constraintCount = levelData.Column;
        }
        else
        {
            Debug.LogError("GridLayoutGroup component not found on cardGrid.");
        }
        totalPairs = (levelData.Column * levelData.Row) / 2;
        CreatePairs();
        Shuffle(cards);
        CreateCards();
        gameState = GameState.PREVIEW;
        
    }

    private void CreatePairs()
    {
        var indexData = 0;
        for (int i = 0; i < totalPairs; i++)
        {
            if (indexData > cardScriptable.cardData.Count -1)
            {
                indexData = 0;
            }
            var card = cardScriptable.cardData[indexData]; 
            CardData cardData = new CardData
            {
                cardId = card.cardId,    
                cardSprite = card.cardSprite
            };
            cards.Add(cardData);
            cards.Add(cardData);
            indexData ++;
        }
    }

    void Shuffle(List<CardData> cards)
    {
        var listTemp = new List<CardData>();
        // Shuffle the value cards in the game
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, cards.Count - 1);

            CardData temp = cards[randomIndex];

            listTemp.Add(temp);
            this.cards.RemoveAt(randomIndex);
        }
        this.cards = listTemp;
    }

    void StartGame()
    {
        isGameStarted = true;
        foreach (var item in listCard)
        {
            item.HideCard();
        }
        gameState = GameState.PLAYING;
        remainingTime = levelData.TimeLimit;
        UpdateTimer();
    }
    
    private void LoadLevel()
    {
        if(LevelManager.instance == null)
            return; 
        var levelManager = LevelManager.instance;
        levelData = levelManager.GetCurrentLevel();
        txt_level.text = "Level\n" + (levelData.Level);
    }

    void CreateCards()
    {
        // Load game data from a file or PlayerPrefs
        totalPairs = (levelData.Column * levelData.Row) / 2;
        for (int i = 0; i < levelData.Row; i++)
        {
            for (int j = 0; j < levelData.Column; j++)
            {
                int index = (i * levelData.Column) + j;
                GameObject cardObject = PoolingCard();
                cardObject.name = "Card_" + (index);
                Card card = cardObject.GetComponent<Card>();
                if (card != null)
                {
                    var cardData = cards[index];
                    card.SetCard(cardData.cardId, cardData.cardSprite);
                    if (gameState == GameState.PREVIEW)
                    {
                        card.FlipCard(); // Flip the card to show its image during the preview phase
                    }
                    else
                    {
                        card.HideCard(); // Hide the card's image initially
                    }
                    card.gameObject.GetComponent<Button>()?.onClick.AddListener(() => OnCardClicked(card));
                    // listCard.Add(card);
                }
            }
        }
    }

    void UpdateTimer()
    {
        if (gameState != GameState.PLAYING)
            return;
        
        float fillAmount = remainingTime / levelData.TimeLimit;
        timerImage.fillAmount = fillAmount;
        timerImage.gameObject.SetActive(remainingTime > 0); // Hide the timer when time is up
        txtTime.gameObject.SetActive(remainingTime > 0);
        txtTime.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    void Gameover()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
        gameState = GameState.GAME_OVER;
        Lose();
    }

    void RestetData()
    {
        // Reset game data to initial state
        isGameStarted = false;
        isGameOver = false;
        cardFist = cardSecond = null;
        matchedPairs = totalPairs = 0;
        foreach (var item in listCard)
            item.Reset();

        cards.Clear();
        // UpdateTimer();
    }

    public void OnCardClicked(Card clickedCard)
    {
        if (isGameOver || clickedCard.isFlipped || clickedCard.isMatched || gameState != GameState.PLAYING)
        {
            return;
        }

        if (cardFist == null)
        {
            cardFist = clickedCard;
            cardFist.FlipCard();
        }
        else if (cardSecond == null && clickedCard != cardFist)
        {
            cardSecond = clickedCard;
            cardSecond.FlipCard();
            StartCoroutine(IECompare());
        }
    }
    
    private IEnumerator IECompare()
    {
        Debug.Log("Comparing cards: " + cardFist.cardId + " and " + cardSecond.cardId + " - Match: " + (cardFist.cardId == cardSecond.cardId));

        if (cardFist.cardId == cardSecond.cardId)
        {
            // Cards match, keep them flipped
            cardFist.isMatched = true;
            cardSecond.isMatched = true;
            cardFist = null;
            cardSecond = null;
            matchedPairs++;
            if (matchedPairs == totalPairs)
            {
                Win();
            }
        }
        else
        {
            // Cards do not match, flip them back after a short delay
            yield return new WaitForSeconds(1f); // Wait for 1 second before flipping back
            cardFist.HideCard();
            cardSecond.HideCard();
            cardFist = null;
            cardSecond = null;
        }
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 second before comparing the cards
    }

    public void Win()
    {
        Debug.Log("Win Game at Level: " + levelData.Level);
        
        PauseGame();

        var dialog = LoadDialog(Constant.DIALOG_WIN);
        if(dialog == null) return;

        GameObject dialog_win = Instantiate(dialog, canvasUI);
        var dialogScript = dialog_win.GetComponent<WinDialog>();
        if (dialogScript != null)
        {
            dialogScript.Init(nextLevel);
            nextLevel++;
        }
        if (DataManager.instance == null)
            return;
        
        var dataManger = DataManager.instance;
        dataManger.CurrentLevel = nextLevel;
    }   

    public void Lose()
    {
        var dialog = LoadDialog(Constant.DIALOG_GAME_OVER);
        if(dialog == null) return;

        var dialogScript = dialog.GetComponent<GameOverDialog>();
        if (dialogScript != null)
        {
            dialogScript.Init();
        }
        Debug.Log("Lose Game at Level: " + levelData.Level);
    } 

    public void NextLevel()
    {
        if(DataManager.instance == null && LevelManager.instance == null)
            return;
        // var dataManager = DataManager.instance;
        // dataManager.CurrentLevel++;
        // dataManager.SaveData();

        // var levelManager = LevelManager.instance;
        // LevelData levelData = levelManager.GetCurrentLevel();
        // if(levelData == null) return;
        
        // levelData.Row = (byte)levelData.Row;
        // levelData.Column = (byte)levelData.Column;
        // remainingTime = levelData.TimeLimit;
        RestetData();
        LoadData();
    }

    public void PauseGame()
    {
        var dialog = LoadDialog(Constant.DIALOG_PAUSE_GAME);
        if(dialog == null) return;

        GameObject dialog_pause_game = Instantiate(dialog, canvasUI);
        var dialogScript = dialog_pause_game.GetComponent<PauseGameDialog>();
        if (dialogScript != null)
        {
            dialogScript.Init();
        }
    }

   
    GameObject LoadDialog(string dialogName)
    {
        var dialog = Resources.Load<GameObject>("Prefabs/" + dialogName);
        if (dialog == null)
        {
            Debug.Log("Reload fail dialog: " + dialogName);
            return null;
        }
        Debug.Log("Load success: " + dialogName);
        return dialog;
    }

    void OnDestroy()
    {
        GameEvents.OnNextLevel -= NextLevel;
    }

    GameObject PoolingCard()
    {
        if (listCard.Count > 0)
        {
            foreach (var item in listCard)
            {
                if (!item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(true);
                    item.isMatched = item.isFlipped = false;
                    item.gameObject.GetComponent<Button>()?.onClick.RemoveListener(() => OnCardClicked(item));
                    return item.gameObject;
                }
            }
        }
        
        GameObject cardObject = Instantiate(cardPrefab, cardGrid);
        var cardScript = cardObject.GetComponent<Card>();
        if (cardScript != null)
        {
            listCard.Add(cardScript);
        }
        return cardObject;
    }
}

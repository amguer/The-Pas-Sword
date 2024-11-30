using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<int> OnUpdateRound;
    public static Action<int> OnUpdatePlayerCP;
    public static Action<string> OnUpdatePlayerDisplayName;
    public static Action<int> OnUpdateEnemyCP;
    public static Action<string> OnUpdateEnemyDisplayName;
    public static Action OnBeginGame;
    public static Action OnEndTurn;
    public static Action OnLose;
    public static Action OnWin;

    public int roundNum { get; private set; }
    public int playerCP { get; private set; }
    public int enemyCP { get; private set; }
    public string fullPlayerName { get; private set; }
    public string playerDisplayName { get; private set; }
    public string fullEnemyName { get; private set; }
    public string enemyDisplayName { get; private set; }
    public Instigator currentInstigator { get; private set; }

    [SerializeField] EnemyAIManager enemyAI;
    [SerializeField] int defaultPlayerCP = 100;
    [SerializeField] string defaultPlayerName = string.Empty;
    [SerializeField] int defaultEnemyCP = 100;
    [SerializeField] string defaultEnemyName = string.Empty;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        InitializeGameStats();
        OnBeginGame?.Invoke();
    }

    public void EndTurn()
    {
        roundNum++;
        if(currentInstigator == Instigator.Player)
        {
            currentInstigator = Instigator.Enemy;
            UpdateEnemyCP(100);
            enemyAI.Play();
        }
        else
        {
            currentInstigator = Instigator.Player;
            UpdatePlayerCP(100);
        }
        OnUpdateRound?.Invoke(roundNum);
        OnEndTurn?.Invoke();
    }

    public void InitializeGameStats()
    {
        roundNum = 1;
        playerCP = defaultPlayerCP;
        enemyCP = defaultEnemyCP;
        currentInstigator = Instigator.Player;
        if(defaultPlayerName != string.Empty)
        {
            SetPlayerName(defaultPlayerName);
        }
        if(defaultEnemyName != string.Empty)
        {
            SetEnemyName(defaultEnemyName);
        }
        OnUpdateRound?.Invoke(roundNum);
        OnUpdatePlayerCP?.Invoke(playerCP);
        OnUpdateEnemyCP?.Invoke(enemyCP);
    }

    public void SetPlayerName(string name)
    {
        fullPlayerName = name;
        string displayName = string.Empty;
        for(int i = 0; i < fullPlayerName.Length; i++)
        {
            displayName += "_";
        }
        UpdatePlayerDisplayName(displayName);
    }

    public void SetEnemyName(string name)
    {
        fullEnemyName = name;
        string displayName = string.Empty;
        for (int i = 0; i < fullEnemyName.Length; i++)
        {
            displayName += "_";
        }
        UpdateEnemyDisplayName(displayName);
        
    }

    public void UpdatePlayerDisplayName(string displayName)
    {
        playerDisplayName = displayName;
        OnUpdatePlayerDisplayName?.Invoke(displayName);
        if (playerDisplayName == fullPlayerName)
        {
            OnLose?.Invoke();
        }
    }

    public void UpdateEnemyDisplayName(string displayName)
    {
        enemyDisplayName = displayName;
        OnUpdateEnemyDisplayName?.Invoke(displayName);
        if(enemyDisplayName == fullEnemyName)
        {
            OnWin?.Invoke();
        }
    }

    public void UpdatePlayerCP(int amount)
    {
        playerCP += amount;
        if (playerCP < 0)
            playerCP = 0;
        OnUpdatePlayerCP?.Invoke(playerCP);
    }

    public void UpdateEnemyCP(int amount)
    {
        enemyCP += amount;
        if (enemyCP < 0)
            enemyCP = 0;
        OnUpdateEnemyCP?.Invoke(enemyCP);
    }

    public void UpdateRoundNumber(int number)
    {
        roundNum = number;
        OnUpdateRound?.Invoke(roundNum);
    }


}

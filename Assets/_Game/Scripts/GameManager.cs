using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player;

    [Header("targetCircle")]
    [SerializeField] private TargetCircle targetCirCle;

    [Header("List")]
    public List<Character> characterList = new List<Character>();

    [Header("Alive")]
    public int initialAlive;
    public int currentAlive;

    [Header("Bool Variables")]
    public bool isGaming;
    public bool isWin;
    public bool isPause;

    [Header("Maps")]
    [SerializeField] private Transform mapParent;
    public GameObject[] mapPrefabs;
    public GameObject currentMap;

    [Header("NavMeshData")]
    public NavMeshData[] navMeshData;
    public NavMeshData currentNavMeshData;
    public int currentLevelIndex = 0;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        characterList.Add(player);
    }

    private void Start()
    {
        currentAlive = initialAlive;
        isGaming = false;
        isWin = false;
        BotManager.instance.DisableAllBots();
    }

    private void Update()
    {
        if (currentAlive == 1 && player.isDead == false && isGaming ==true )
        {
            player.Dance();
            isGaming = false;
            isWin = true;
        }
    }

    public void DeleteCharacters()
    {
        if (!player.isDead)
        {
            characterList.Remove(player);
        }
        while(characterList.Count >0)
        {
            //tat het cac weapon dang bay
            for (int j = 0; j < characterList[0].pooledWeaponList.Count; j++)

            {
                if (characterList[0].pooledWeaponList[j].gameObject.activeSelf)
                {
                    characterList[0].pooledWeaponList[j].gameObject.SetActive(false);
                }
            }

            // despawn bot
            BotManager.instance.DesSpawn(characterList[0] as Bot);
        }

        characterList.Clear();
    }

    public void RespawnCharascter()
    {
        characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.SpawnBot();
        }
    }

    public void DeleteCharacterInEnemyList(Character character)
    {
        for (int i =0; i < characterList.Count; i++)
        {
            if (characterList[i].enemyList.Contains(character))
            {
                characterList[i].enemyList.Remove(character);
            }
        }
    }

    public void PlayGame()
    {
        BotManager.instance.EnableAllBots();
        UIManager.instance.HideMainMenu();
        UIManager.instance.ShowIndicators();
        UIManager.instance.ShowJoystick();
        UIManager.instance.ShowAliveText();
        UIManager.instance.HideCoin();
        currentAlive = initialAlive;
        isGaming = true;
        //Player.instance.isDead = false;
        //Player.instance.Idle();
    }
    public void ResetTargetCircle()
    {
        targetCirCle.Deactive();
    }
    
}

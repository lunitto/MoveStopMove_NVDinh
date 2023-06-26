using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player:")]
    [SerializeField] private Player player;

    [Header("targetCircle")]
    [SerializeField] private TargetCircle targetCircle;

    [Header("Lists:")]
    public List<Character> characterList = new List<Character>();

    [Header("Alive:")]
    public int initialAlive;
    public int currentAlive;

    [Header("Bool Variables:")]
    public bool isGaming;
    public bool isWin;
    public bool isPause;

    [Header("Maps:")]
    [SerializeField] private Transform mapParent;
    public GameObject[] mapPrefabs;
    private GameObject currentMap;
    [Header("NavMeshDatas:")]
    public NavMeshData[] navMeshDatas;
    private NavMeshData currentNavMeshData;
    public int currentLevelIndex = 0;

    //singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        characterList.Add(player);
    }

    void Start()
    {
        currentAlive = initialAlive;
        isGaming = false;
        isWin = false;
        BotManager.instance.DisableAllBots();
        SpawnMap(currentLevelIndex);
        SpawnNav(currentLevelIndex);

    }

    private void Update()
    {
        if (currentAlive == 1 && player.isDead == false && isGaming == true)
        {
            UIManager.instance.ShowWinPanel();
            player.Dance();
            isGaming = false;
            isWin = true;
            currentLevelIndex++;
            if (currentLevelIndex >= navMeshDatas.Length)
            {
                currentLevelIndex = 0;
            }
            SoundManager.instance.Play(SoundType.Win);
        }
        
    }

    

    public void DeleteCharacters()
    {
        if (!player.isDead)
        {
            characterList.Remove(player);
        }
        while (characterList.Count > 0)
        {
            // tat het cac weapon dang bay
            for (int j = 0; j < characterList[0].pooledWeaponList.Count; j++)
            {
                if (characterList[0].pooledWeaponList[j].gameObject.activeSelf)
                {
                    characterList[0].pooledWeaponList[j].gameObject.SetActive(false);
                }
            }
            // despawn bot
            BotManager.instance.DeSpawn(characterList[0] as Bot);
        }
        characterList.Clear();
    }

    public void RespawnCharacters()
    {
        characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            //characterList.Add(BotManager.instance.botList[i]);
            BotManager.instance.SpawnBot();
        }
    }

    public void DeleteThisElementInEnemyLists(Character character)
    {
        for (int i = 0; i < characterList.Count; i++)
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
        UIManager.instance.ShowJoystick();
        UIManager.instance.ShowIndicators();
        UIManager.instance.ShowCanvasName();
        UIManager.instance.ShowAliveText();
        UIManager.instance.ShowCoin();
        currentAlive = characterList.Count;
        isGaming = true;
    }

    public void ResetTargetCircle()
    {
        targetCircle.Deactive();
    }

    public void SpawnMap(int index)
    {
        if (currentMap == mapPrefabs[index])
        {
            return;
        }
        Destroy(currentMap);
        currentMap = Instantiate(mapPrefabs[index]);
        currentMap.transform.SetParent(mapParent.transform);
        currentMap.transform.localPosition = Vector3.zero;
        currentMap.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void SpawnNav(int index)
    {
        if (currentNavMeshData == navMeshDatas[index])
        {
            return;
        }
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshDatas[index]);
        currentNavMeshData = navMeshDatas[index];
    }
    public void EnableALlCharacters()
    {
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.botList[i].ChangeState(new PatrolState());
        }
        isGaming = true;
    }

    public void DisnableALlCharacters()
    {
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.botList[i].ChangeState(new IdleState());
        }
        isGaming = false;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

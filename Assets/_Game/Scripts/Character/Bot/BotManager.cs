using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [SerializeField] private float spawnDisance;
    [SerializeField] private Transform poolBot;

    public Transform topLeft;
    public Transform bottomRight;
    public float initialY;
    public int size;
    public BotPool botPool;
    public List<Bot> botList = new List<Bot>();

    public static BotManager instance;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            SpawnBot();
        }
    }

    public void SpawnBot()
    {
        Vector3 spawnRotate = new Vector3(0, Random.Range(0, 360), 0);
        Vector3 spawnPosition;
        do
        {
            int randomX = (int)Random.Range(topLeft.position.x, bottomRight.position.x);
            int randomZ = (int)Random.Range(bottomRight.position.z, topLeft.position.z);
            spawnPosition = new Vector3(randomX, initialY, randomZ);
        } while (CheckPosition(spawnPosition));



        Bot BotClone = botPool.GetObject().GetComponent<Bot>();
        BotClone.transform.position = spawnPosition;
        BotClone.transform.rotation = Quaternion.Euler(spawnRotate);
        BotClone.OnInit();
        BotClone.transform.SetParent(poolBot);
        SpawnBotName(BotClone);
        SpawnBotIndicator(BotClone);

        if (botList.Count < size)
        {
            botList.Add(BotClone);
        }
    }

    public void DesSpawn (Bot bot)
    {
        bot.DeActiveNavmeshAgent();
        botPool.ReturnToPool(bot.gameObject);
    }

    public void SpawnBotName(Bot bot)
    {
        //Debug.Log("spawn bot name");
        GameObject botName = BotNamePool.instance.GetObject();
        botName.GetComponent<BotName>().SetTargetTransform(bot.transform);
        botName.GetComponent<BotName>().SetColor(bot);
        bot.botName = botName;
        botName.SetActive(true);
    }
    public void SpawnBotIndicator(Bot bot)
    {
        bot.indicator.SetColor(bot);
    }
    public bool CheckPosition(Vector3 pos)
    {
        if (botList.Count < 2)
        {
            return false;
        }
        for (int i = 0; i < botList.Count; i++)
        {
            if (Vector3.Distance(botList[i].transform.position, pos) < spawnDisance)
            {
                return true;
            }
        }
        return false;
    }

    public void EnableAllBots()
    {
        for ( int i=0; i < botList.Count; i++)
        {
            if (botList[i].isDead == false)
            {
                botList[i].ChangeState(new PatrolState());
            }
        }
        GameManager.instance.isGaming = true;
    }

    public void DisableAllBots()
    {
        for (int i = 0; i < botList.Count; i++)
        {
            if (botList[i].isDead == false)
            {
                botList[i].ChangeState(new IdleState());
            }
        }
        GameManager.instance.isGaming = false;
    }
}

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
        Bot BotClone = botPool.GetObject().GetComponent<Bot>();
        SetPosAndRotBot(BotClone);
        BotClone.transform.SetParent(poolBot);
        
        BotClone.OnInit();
        SpawnBotName(BotClone);
        SpawnBotIndicator(BotClone);
        SpawnWeaponBot(BotClone);
        if (botList.Count < size)
        {
            botList.Add(BotClone);
        }
        if (!GameManager.instance.characterList.Contains(BotClone))
        {
            GameManager.instance.characterList.Add(BotClone);
        }
        
    }

    public void DeSpawn (Bot bot)
    {       
        bot.DeActiveNavmeshAgent();      
        BotNamePool.instance.ReturnToPool(bot.botName);
        GameManager.instance.characterList.Remove(bot);
        botPool.ReturnToPool(bot.gameObject);
    }

    public void SetPosAndRotBot(Bot bot)
    {
        
        Vector3 spawnRotate = new Vector3(0, Random.Range(0, 360), 0);
        Vector3 spawnPosition;
        do
        {
            int randomX = (int)Random.Range(topLeft.position.x, bottomRight.position.x);
            int randomZ = (int)Random.Range(bottomRight.position.z, topLeft.position.z);
            spawnPosition = new Vector3(randomX, initialY, randomZ);
        } while (CheckPosition(spawnPosition));
        bot.transform.position = spawnPosition;
        bot.transform.rotation = Quaternion.Euler(spawnRotate);
    }

    public bool CheckPosition(Vector3 pos)
    {
        for (int i = 0; i < GameManager.instance.characterList.Count; i++)
        {
            if (Vector3.Distance(GameManager.instance.characterList[i].transform.position, pos) < spawnDisance)
            {
                return true;
            }
        }
        return false;
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

    public void SpawnWeaponBot(Bot bot)
    {
        if (bot.isHaveWeapon == false)
        {
            GameObject newWeaponObj = Instantiate(ShopWeapManager.instance.weapons[(int)Random.Range(0, ShopWeapManager.instance.weapons.Length)].gameObject);
            Weapon newWeapon = newWeaponObj.GetComponent<Weapon>();
            newWeapon.gameObject.SetActive(true);
            newWeapon.transform.SetParent(bot.rightHand.transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            newWeapon.SetCharacterAndWeaponPool(bot, bot.weaponPool);
            bot.onHandWeapon = newWeapon.gameObject;
            bot.weaponPool.prefabWeapon = newWeapon.gameObject;
            bot.onHandWeapon.GetComponent<BoxCollider>().enabled = false;
            bot.isHaveWeapon = true;
        }
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
                //Debug.Log("aaaaaaaa");
                botList[i].ChangeState(new IdleState());
            }
        }
        GameManager.instance.isGaming = false;
    }
}

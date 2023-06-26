using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameStateObjects;
    [Header("GameMenu:")]
    [SerializeField] private GameObject gameMenuMain;
    //[SerializeField] private GameObject weaponShop;
    //[SerializeField] private GameObject skinShop;

    [Header("Joystick")]
    [SerializeField] private GameObject joystick;

    [Header("Alive")]
    [SerializeField] private Text aliveText;
    [SerializeField] private GameObject aliveTextObj;

    [Header("WinLose")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    public Text loseText;
    public Text rankText;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject canvasName;

    [Header("Coin")]
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject coin;

    [Header("Shop")]
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject hatArea;
    [SerializeField] private GameObject pantArea;
    [SerializeField] private GameObject shieldArea;
    [SerializeField] private GameObject fullsetArea;

    //singleton
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        HideJoystick();
        HideAliveText();
        HideIndicators();
        UpdateUICoin();
    }

    private void Update()
    {
        aliveText.text = GameManager.instance.currentAlive.ToString();
    }

    public void CloseAll()
    {
        HideJoystick();
        HideLosePanel();
        HideWinPanel();
        HideIndicators();
        HideWeaponShop();
        HideCoin();
        HideAliveText();
        HideMainMenu();
    }

    //main menu
    public void HideMainMenu()
    {
        gameMenuMain.SetActive(false);
    }
    public void ShowMainMenu()
    {
        gameMenuMain.SetActive(true);
    }

    //joystick
    public void ShowJoystick()
    {
        joystick.gameObject.SetActive(true);
        
    }

    public void HideJoystick()
    {
        
        joystick.gameObject.SetActive(false);
    }

    // win lose
    public void ShowWinPanel()
    {
        CloseAll();
        winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        winPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        CloseAll();
        losePanel.SetActive(true);
    }

    public void HideLosePanel()
    {
        losePanel.SetActive(false);
    }


    //indicator
    public void ShowIndicators()
    {
        indicator.SetActive(true);
    }

    public void HideIndicators()
    {
        indicator.SetActive(false);
    }

    public void ShowCanvasName()
    {
        canvasName.SetActive(true);
    }

    public void HideBotName()
    {
        canvasName.SetActive(false);
    }

    //weap shop
    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
    }

    public void HideWeaponShop()
    {
        weaponShop.SetActive(false);
    }

    //coin
    public void ShowCoin()
    {
        coin.SetActive(true);
    }
    
    public void HideCoin()
    {
        coin.SetActive(false);
    }

    public void UpdateUICoin()
    {
        coinText.text = DataManager.ins.playerData.coin.ToString();
    }

    //alive text
    public void ShowAliveText()
    {
        aliveTextObj.SetActive(true);
    }

    public void HideAliveText()
    {
        aliveTextObj.SetActive(false);
    }

    public void SetRankText(int rank)
    {
        rankText.text = "Rank: " + rank.ToString();
    }

    //choose areas
    public void HideAllItemChooseAreas()
    {
        hatArea.SetActive(false);
        pantArea.SetActive(false);
        shieldArea.SetActive(false);
        fullsetArea.SetActive(false);
    }
}

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
        HideWeaponShop();
        HideCoin();
        HideAliveText();
        HideMainMenu();
    }

    public void HideMainMenu()
    {
        gameMenuMain.SetActive(false);
    }
    public void ShowJoystick()
    {
        joystick.gameObject.SetActive(true);
        
    }

    public void HideJoystick()
    {
        
        joystick.gameObject.SetActive(false);
    }

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

    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
    }

    public void HideWeaponShop()
    {
        weaponShop.SetActive(false);
    }

    public void ShowCoin()
    {
        coin.SetActive(true);
    }

    public void HideCoin()
    {
        coin.SetActive(false);
    }

    public void ShowAliveText()
    {
        aliveTextObj.SetActive(true);
    }

    public void HideAliveText()
    {
        aliveTextObj.SetActive(false);
    }
}

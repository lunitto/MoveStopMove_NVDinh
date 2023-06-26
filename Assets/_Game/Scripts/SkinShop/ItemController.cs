﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemController : MonoBehaviour
{
    [Header("Player:")]
    public PlayerWearSkinItems player;
    [Header("Button Array:")]
    public Text buyButtonText;
    public Button[] buttons;
    [Header("Items Array:")]
    public Item[] items;
    [Header("Indexs:")]
    public int currentIndex = 0;
    public int usingIndex = -1;
    [Header("Item Type:")]
    public ItemType itemType;


    protected virtual void Start()
    {
        AddEventToAllItems();
        items[0].DisplayOutline();
    }

    public void AddEventToAllItems()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int localIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(localIndex));
        }
    }

    protected virtual void UnDisplayAllOutlines()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            item.UnDisplayOutline();
        }
    }

    public virtual void OnButtonClick(int index)
    {
        Item item = items[index];
        // nếu item đang được chọn chưa mua thì button sẽ hiển thị giá tiền
        if (item.isPurchased == false)
        {
            UpdateBuyButton(item.cost.ToString());
        }
        else
        {
            // nếu item đang được chọn mua rồi và item đang được chọn chính là item đang sử dụng thì button sẽ hiển thị USING
            if (usingIndex == index)
            {
                UpdateBuyButton("using");
            }
            // nếu item đang được chọn mua rồi và item đang được chọn không phải item đang sử dụng thì button sẽ hiển thị USE
            else
            {
                UpdateBuyButton("use");
            }
        }
        currentIndex = index;
        SkinShopManager.instance.currentItem = item;

        UnDisplayAllOutlines();
        item.DisplayOutline();
    }

    public void UpdateBuyButton(string str)
    {
        buyButtonText.text = str;
    }

    public void LoadIsPurchasedData()
    {
        //duyet dict de tim tim ra itemcontroller, sau do set tat ca isPurchased = data.isPurchased
        for (int i = 0; i < DataManager.ins.playerData.dict.Length; i++)
        {
            if (DataManager.ins.playerData.dict[i].itemType == this.itemType)
            {
                for (int j = 0; j < DataManager.ins.playerData.dict[i].isPurchaseds.Length; j++)
                {
                    items[j].isPurchased = DataManager.ins.playerData.dict[i].isPurchaseds[j];
                    if (items[j].isPurchased == true)
                    {
                        OnButtonClick(j);
                        SkinShopManager.instance.UnlockSkin(items[j]);
                    }
                }
            }
        }
        //mua usingitem
        for (int i = 0; i < DataManager.ins.playerData.usingItemIndexs.Length; i++)
        {
            if (DataManager.ins.playerData.usingItemIndexs[i] >= 0 && i == (int)this.itemType)
            {
                OnButtonClick(DataManager.ins.playerData.usingItemIndexs[i]);
                SkinShopManager.instance.BuyItem(DataManager.ins.playerData.dict[i].itemType);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotName : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Text text;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    public string nameString;

    private void Start()
    {
        // neu la bot thi moi random name
        Bot bot = targetTransform.GetComponent<Bot>();
        if (bot != null)
        {
            nameString = BotNamePool.instance.nameList[(int)Random.Range(0, BotNamePool.instance.nameList.Count)];
            text.text = nameString;
        }
    }

    void LateUpdate()
    {
        targetPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, speed * Time.deltaTime);
    }

    public void SetTargetTransform(Transform targetTF)
    {
        targetTransform = targetTF;
    }

    public void SetColor(Bot bot)
    {
        text.color = bot.skinnedMeshRenderer.material.color;
    }
}

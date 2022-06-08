using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShopC : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _canvas;
    public void OnPointerDown(PointerEventData eventData)
    {
        print(eventData);
        OpenTab(_canvas);
    }

    private void OpenTab(GameObject canvas)
    {
        canvas.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
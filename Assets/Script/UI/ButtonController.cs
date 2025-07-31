using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour
{
    private Action _clickAction;

    public void Init(Action clickAction)
    {
        _clickAction = clickAction;
        GetComponent<Button>().onClick.AddListener(() => _clickAction?.Invoke());
    }
}
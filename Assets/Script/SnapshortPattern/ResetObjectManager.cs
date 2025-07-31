using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetObjectManager : MonoBehaviour
{
    public static ResetObjectManager Instance;

    [SerializeField] private Button _resetBtn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _resetBtn.onClick.AddListener(ResetAllObjectState);
    }

    private List<IResetableObject> _listResetableObjects = new List<IResetableObject>();

    public void RegisterResetObject(IResetableObject resetableObj)
    {
        if (!_listResetableObjects.Contains(resetableObj))
        {
            _listResetableObjects.Add(resetableObj);
            resetableObj.SaveInitState();
        }
    }

    public void ResetAllObjectState()
    {
        foreach (var resettableObj in _listResetableObjects)
        {
            resettableObj.ResetToInitState();
        }
    }

    
}

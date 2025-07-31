using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetableObject
{
    public void SaveInitState();
    public void ResetToInitState();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CGameManager : CScriptableSingletone<CGameManager>
{
    public override void BeforeLoadInstance()
    {
        throw new System.NotImplementedException();
    }


    //TODO : ���� ����� ���� ���⼭ ����
    public void Save() 
    {
        Debug.Log("QUIT");
    }

}

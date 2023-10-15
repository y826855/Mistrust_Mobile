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


    //TODO : 게임 종료시 저장 여기서 하자
    public void Save() 
    {
        Debug.Log("QUIT");
    }

}

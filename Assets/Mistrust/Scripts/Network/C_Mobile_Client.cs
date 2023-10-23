using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System;
using TMPro;

public class C_Mobile_Client : MonoBehaviour
{
    bool socketReady = false;
    TcpClient socket = null;
    NetworkStream stream = null;
    StreamWriter writer = null;
    StreamReader reader = null;

#if UNITY_EDITOR
	public TextMeshProUGUI m_TMP_CurrIP = null;
	public static string Client_IP
	{
		get
		{
			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
			string ClientIP = string.Empty;
			for (int i = 0; i < host.AddressList.Length; i++)
			{
				if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
				{
					ClientIP = host.AddressList[i].ToString();
				}
			}
			return ClientIP;
		}
	}
#endif

	public void ConnectToServer(System.Action<bool> _callBack = null)
	{
		// 이미 연결되었다면 함수 무시
		if (socketReady) return;

		// 기본 호스트/ 포트번호
#if UNITY_EDITOR
		string ip = Client_IP;
#endif
		//string ip = "NONE";
		int port = 7777;

		// 소켓 생성
		try
		{
			socket = new TcpClient(ip, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;

			if (_callBack != null) _callBack(true);
			Debug.Log("참가 성공");
		}
		catch (Exception e)
		{
			Debug.Log("참가 실패");
			if (_callBack != null) _callBack(false);
			//Chat.instance.ShowMessage($"소켓에러 : {e.Message}");
		}
	}

    private void Awake()
    {
		CGameManager.Instance.m_Network = this;
    }

    private void Start()
    {
		//ConnectToServer();
	}

	public void JoinServer() 
	{
		ConnectToServer();
	}

	void Update()
	{
		if (socketReady && stream.DataAvailable)
		{
			string data = reader.ReadLine();
			if (data != null)
				OnIncomingData(data);
		}
	}

	//받은 데이터 처리
	public void OnIncomingData(string _data) 
	{
		Debug.Log("전송받음 " + _data);

		char[] delimiters = { '+' };
		string[] packit = _data.Split(delimiters);

		foreach (var it in packit) 
		{
			Debug.Log(it);
		}

		//switch (packit[0] as CUtility.ESendToMobile) 
		CGameManager.Instance.m_AppMgr.ReciveData((CUtility.ESendToMobile)int.Parse(packit[0]),
			packit[1]);


		//switch ((CUtility.ESendToMobile)int.Parse(packit[0])) 
		//{
		//	case CUtility.ESendToMobile.LOCK:
		//		CUtility.CLock lockObj = JsonUtility.FromJson(packit[1], typeof(CUtility.CLock)) as CUtility.CLock;
		//		var lockSolver = CGameManager.Instance.m_AppMgr.m_AnalogueLockSolver;
		//		lockSolver.GetLockData(lockObj);
		//		break;
		//
		//	default: 
		//		
		//		break;
		//
		//}
		
	}



	void FunctionExecuter(string _data) 
	{
		var param = new object[] { 10 };
		this.GetType().GetMethod("dfdf").Invoke(this, param);
	}

	public void SendAnything() 
	{
		if (!socketReady) return;
		Debug.Log("전송시작");
		writer.WriteLine("전   송  했   다");
		writer.Flush();
	}

	public void SendFunction(string _data)
	{
		if (!socketReady) return;
		Debug.Log("문열어");
		writer.WriteLine(_data);
		writer.Flush();
	}


	private void OnApplicationQuit()
    {
		Debug.Log("연결 종료");
		if (socket == null) return;
		socket.Close();
	}
}

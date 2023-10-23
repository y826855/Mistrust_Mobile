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
		// �̹� ����Ǿ��ٸ� �Լ� ����
		if (socketReady) return;

		// �⺻ ȣ��Ʈ/ ��Ʈ��ȣ
#if UNITY_EDITOR
		string ip = Client_IP;
#endif
		//string ip = "NONE";
		int port = 7777;

		// ���� ����
		try
		{
			socket = new TcpClient(ip, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;

			if (_callBack != null) _callBack(true);
			Debug.Log("���� ����");
		}
		catch (Exception e)
		{
			Debug.Log("���� ����");
			if (_callBack != null) _callBack(false);
			//Chat.instance.ShowMessage($"���Ͽ��� : {e.Message}");
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

	//���� ������ ó��
	public void OnIncomingData(string _data) 
	{
		Debug.Log("���۹��� " + _data);

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
		Debug.Log("���۽���");
		writer.WriteLine("��   ��  ��   ��");
		writer.Flush();
	}

	public void SendFunction(string _data)
	{
		if (!socketReady) return;
		Debug.Log("������");
		writer.WriteLine(_data);
		writer.Flush();
	}


	private void OnApplicationQuit()
    {
		Debug.Log("���� ����");
		if (socket == null) return;
		socket.Close();
	}
}

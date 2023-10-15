using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System;
using TMPro;

public class C_PC_Client : MonoBehaviour
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

	public void ConnectToServer()
	{
		// �̹� ����Ǿ��ٸ� �Լ� ����
		if (socketReady) return;

		// �⺻ ȣ��Ʈ/ ��Ʈ��ȣ
		string ip = Client_IP;
		int port = 7777;

		// ���� ����
		try
		{
			socket = new TcpClient(ip, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
			Debug.Log("���� ����");
		}
		catch (Exception e)
		{
			Debug.Log("���� ����");
			//Chat.instance.ShowMessage($"���Ͽ��� : {e.Message}");
		}
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

	void OnIncomingData(string _data) 
	{
		Debug.Log("���� : " + _data);
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

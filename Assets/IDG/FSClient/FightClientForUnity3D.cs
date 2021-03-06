﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IDG;
using IDG ;
using IDG.MobileInput;
using UnityEngine.UI;
/// <summary>
/// 【战斗客户端Unity接口】管理【战斗客户端】并在每帧进行解析消息
/// </summary>
public class FightClientForUnity3D : MonoBehaviour {
    
    public FSClient client;
   // public string serverIp;
    //public static FightClientForUnity3D instance;
    public List<JoyStick> joySticks;
    public List<NetButton> netButtons;
    public Camera mainCamera;

    
    // public static FightClientForUnity3D Instance
    // {
    //     get { return instance; }
    // }
    // Use this for initialization
    void Awake () {
        joySticks=new List<JoyStick>();
        joySticks.AddRange(GetComponentsInChildren<JoyStick>());
        netButtons = new List<NetButton>();
        netButtons.AddRange(GetComponentsInChildren<NetButton>());
        client = new FSClient();
   
        client.unityClient = this;
      //   client.Connect("127.0.0.1", 12345,10);
        client.Connect(GameUser.user.fightRoom.ip, int.Parse(GameUser.user.fightRoom.port),10,GetComponentsInChildren<IGameManager>());
      //  client.Connect("172.16.252.231", 12345,10);
        // foreach (JoyStick joyStick in joySticks)
        // {
        //     InputCenter.Instance.AddJoyStick(joyStick.frameKey,joyStick.GetInfo());
        // }
       
        //V2 v2 = new V2(1, 0);
        //for (int i =0; i <= 360; i+=30)
        //{
        //   // Debug.Log("sin"+i+":"+MathR.SinAngle(new Ratio(i)).ToFloat());
        //   // Debug.Log("cos" + i + ":" + MathR.CosAngle(new Ratio(i)).ToFloat());
        //}

        // InputCenter.Instance.framUpdate += FrameUpdate;
        //  V2 v2 = new V2(-1,-1);
        //  Debug.Log(v2.ToRotation());
        //  Debug.LogError((10 & 2) == 2);
        // Debug.LogError(V2.left+V2.up);
        //Ratio r = new Ratio(16);
        //Debug.Log(r.SQR(r));
        //Debug.Log(r.InvSqrt(16));

        // V2 v2 = new V2(-5, 99);
        // Debug.Log("(-5, 99)" + v2.x * v2.x + "," + v2.y * v2.y);
        //  Debug.Log("normalized"+v2.normalized);
      
        // for (FixedNumber i = FixedNumber.Zero; i < new FixedNumber(360); i+=new FixedNumber(0.1f))
        // {
        //     Debug.LogError(i+"du to direction= "+(new Vector3(0,360,0)- i.ToUnityRotation().eulerAngles));
        // }
        //Fixed.RotationLerp(Fixed.Zero,new Fixed(150),new Fixed(1));
    }
	

  
    
	// Update is called once per frame
     private void FixedUpdate() {
       
       if (client.MessageList.Count > 0)
       {
           client.ParseMessage(client.MessageList.Dequeue());
       }
       
        
        CommitKey();
    }
   
   
    public void CommitKey()
    {
        if(client==null||client.inputCenter==null)return;
        // if (Input.GetKey(UnityEngine.KeyCode.J))
        // {
        //     key |= IDG.KeyNum.Attack;
        // }
        // InputCenter.Instance.
        //  client.inputCenter.SetKey(Input.GetKey(KeyCode.J),KeyNum.Attack);
        foreach (var btn in netButtons)
        {
            client.inputCenter.SetKey(btn.isDown, btn.key);
        }
        foreach (var joy in joySticks)
        {
              client.inputCenter.SetJoyStick(joy.key,joy.GetInfo());
        }
        
        // InputCenter.Instance.SetKey(Input.GetKey(KeyCode.A),KeyNum.Left);
        // InputCenter.Instance.SetKey(Input.GetKey(KeyCode.D),KeyNum.Right);
        // InputCenter.Instance.SetKey(Input.GetKey(KeyCode.W),KeyNum.Up);
        // InputCenter.Instance.SetKey(Input.GetKey(KeyCode.S),KeyNum.Down);
        
    }
    public void OnDestroy()
    {
        client.Stop();
    }
}

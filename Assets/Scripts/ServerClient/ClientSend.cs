using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
   private static void SendTCPData(Packet _packet)
   {
       _packet.WriteLength();
       MyClient.instance.tcp.SendData(_packet);
   }

   #region Packets
    public static void WelcomeReceived()
    {
        using(Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(MyClient.instance.myId);
            _packet.Write(UIManager.instance.username.text);
        
            SendTCPData(_packet);
        }
    }
   #endregion
}

using System.Collections.Generic;
using Godot;

public partial class ServerNode : Node
{
   
   private UdpServer _server = new UdpServer();
   private List<PacketPeerUdp> _peers = new List<PacketPeerUdp>();
   const ushort PORT = 4242;

   public override void _Ready()
   {
	  base._Ready();
	  _server.Listen(PORT);
     GD.Print($"{_server.GetLocalPort()} ");
   }

	public override void _Process(double delta)
	{
		base._Process(delta);
	  _server.Poll();
	  if(_server.IsConnectionAvailable())
	  {
		 PacketPeerUdp peer = _server.TakeConnection();
		 byte[] packet = peer.GetPacket();
		 GD.Print($"Accepted Peer: {peer.GetPacketIP()}:{peer.GetPacketPort()}");
		 GD.Print($"Received Data: {packet.GetStringFromUtf8()}");
		 // Reply so it knows we received the message.
		 peer.PutPacket(packet);
		 // Keep a reference so we can keep contacting the remote peer.
		 _peers.Add(peer);
	  }
	  //foreach(var peer in _peers){}
   }

}

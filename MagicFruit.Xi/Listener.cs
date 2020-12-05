using MagicFruit.Xi.Packet;
using MagicFruit.Xi.Res;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MagicFruit.Xi
{
    public class Listener
    {
        private readonly IPAddress _ip;
        private readonly int _port;

        public Listener(int port, IPAddress ip = null)
        {
            _ip = ip ?? IPAddress.Any;
            _port = port;
        }


        public HealthUpdate HealthUpdate;


        public async Task Start()
        {
            await Task.Run(() => {
                using (var listener = new UdpClient(_port))
                {
                    var endPoint = new IPEndPoint(_ip, _port);

                    while (true)
                    {
                        var packet = listener.Receive(ref endPoint);

                        switch ((PacketType)packet[0])
                        {
                            // case PacketType.CharacterBuff:
                            //     if (packet[4] != 0x9) break;
                            //
                            //     // Not entirely sure how to tell which character ID this is for atm
                            //     // var character = MapPacket<CharacterBuff>(packet);
                            //
                            //     break;

                            case PacketType.CharacterHealth:
                                var c = MapPacket<CharacterHealth>(packet);

                                HealthUpdate(new PartyMember(c.Id).Update(
                                    c.Index,
                                    c.MainJob,
                                    c.MainJobLevel,
                                    c.SubJob,
                                    c.SubJobLevel,
                                    c.HP,
                                    c.HPPercent,
                                    c.MP,
                                    c.MPPercent,
                                    Zone.unknown
                                ));

                                break;

                            case PacketType.PartyMemberUpdate:
                                var member = MapPacket<PartyMemberUpdate>(packet);

                                HealthUpdate(new PartyMember(member.Id).Update(
                                    member.Index,
                                    member.MainJob,
                                    member.MainJobLevel,
                                    member.SubJob,
                                    member.SubJobLevel,
                                    member.HP,
                                    member.HPPercent,
                                    member.MP,
                                    member.MPPercent,
                                    member.Zone
                                ));

                                break;

                                // case PacketType.PartyBuff:
                                //     MapPacket<PartyBuff>(packet);
                                //     break;
                        }
                    }
                }
            });
        }

        private static T MapPacket<T>(byte[] input)
        {
            var handle = GCHandle.Alloc(input, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(
                    handle.AddrOfPinnedObject(),
                    typeof(T)
                );
            }
            finally
            {
                handle.Free();
            }
        }
    }

    public delegate void HealthUpdate(PartyMember member);

    internal enum PacketType : byte
    { 
        CharacterBuff = 0x63,
        PartyMemberUpdate = 0xDD,
        CharacterHealth = 0xDF,
        PartyBuff = 0x76,
    }
}

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using MagicFruit.Xi.Packet;

namespace Playing
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new UdpClient(19766);
            var endPoint = new IPEndPoint(IPAddress.Any, 19766);

            try
            {
                while (true)
                {
                    var packet = listener.Receive(ref endPoint);

                    if (packet[0] == 0x63 && packet[4] == 0x9)
                    {
                        var toon = MapPacket<CharacterBuff>(packet);

                        Console.WriteLine($"CB Buffs: " + string.Join(" ", toon.Select(buff => buff.ToString()).ToArray()));
                    }

                    if (packet[0] == 0xDD)
                    {
                        var toon = MapPacket<PartyMemberUpdate>(packet);
                        Console.WriteLine($"PM Id: {toon.Id} Index: {toon.Index} Name: {toon.Name}");
                    }

                    if (packet[0] == 0xDF)
                    {
                        var toon = MapPacket<CharacterHealth>(packet);
                        Console.WriteLine(
                            $"CH Id: {toon.Id} {toon.MainJob}/{toon.SubJob} HP: {toon.HP} {toon.HPPercent}% MP: {toon.MP} {toon.MPPercent}% TP: {toon.TP}");
                    }

                    if (packet[0] == 0x76)
                    {
                        var partyMembers = MapPacket<PartyBuff>(packet);

                        foreach (var member in partyMembers)
                        {
                            Console.WriteLine($"PB Id: {member.Id} Index: {member.Index}");
                            Console.WriteLine("Buffs: " + string.Join(" ", member.Select(buff => buff.ToString()).ToArray()));
                        }
                    }
                }
            }
            finally
            {
                listener.Close();
                Thread.Sleep(TimeSpan.FromSeconds(0.3));
            }
        }

        static T MapPacket<T>(byte[] input)
        {
            var handle = GCHandle.Alloc(input, GCHandleType.Pinned);
            try
            {
                return (T) Marshal.PtrToStructure(
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
}

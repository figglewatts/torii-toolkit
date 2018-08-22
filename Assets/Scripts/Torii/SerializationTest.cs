using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProtoBuf;
using UnityEngine;

[ProtoContract]
public class SerializationTest 
{
    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(2)]
    public float Number { get; set; }

    [ProtoMember(3)]
    public List<string> Strings { get; set; }

    public SerializationTest()
    {
        Strings = new List<string>();
    }
}

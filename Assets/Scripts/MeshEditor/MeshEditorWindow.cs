using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MeshEditorWindow
{
    [DllImport("MeshEditor")]
    public static extern int ping();
    
    [DllImport("MeshEditor", EntryPoint = "InitConfig2")]
    public static extern void InitConfig(ref Config conf);
    
    public void Show()
    {
        var config = new Config();
        
        InitConfig(ref config);
        Debug.Log(ping());
        Debug.Log(config.processorCount);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct MSR
{
    [MarshalAs(UnmanagedType.U8)]
    public System.UInt64 data;
    public int adress;
}

[StructLayout(LayoutKind.Sequential)]
public struct ConfCounter
{
    public int adress;
    public int number;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public MSR[] config;

    [MarshalAs(UnmanagedType.U4)]
    public uint configCount;
}

[StructLayout(LayoutKind.Sequential), Serializable]
public struct Config
{
    public int processorCount;
    

    [MarshalAs(UnmanagedType.U4)]
    public uint countersCount;

    [MarshalAs(UnmanagedType.Bool)]
    public bool printToScreen;

    [MarshalAs(UnmanagedType.LPWStr)]
    public string activityName;
}

using System;
using System.Collections.Generic;
using System.Net;

namespace Install
{
    public enum OperatingSystem
    {
        UnkownOperatingSystem,
        MCheetah,
        MPuma,
        MJaguar,
        MPanther,
        MTiger,
        MLeopard,
        MSnowLeropard,
        MLion,
        MMountainLion,
        MMavericks,
        MYosemite,
        MElCapitan,
        MSierra,
        MHighSierra,
        MMojave,
        MCatalina,
        MBigSur,
        W2000,
        WXp,
        WXp64BitEdition,
        WServer2003,
        WServer2003R2,
        WVista,
        WServer2008,
        WServer2008R2,
        W7,
        WServer2012,
        W8,
        WServer2012R2,
        W8P1,
        WServer2016,
        WServer2019,
        W10,
        Linux
    }

    public enum Manufacturer
    {
        Intel,
        AdvancedMicroDevices,
        Apple,
        Windows,
        OpenSource
    }

    public enum CoreArchitecture
    {
        X86,
        Mips,
        Alpha,
        PowerPc,
        Arm,
        Ia64,
        X64
    }

    public enum Architecture
    {
        
    }

    public enum Socket
    {
        
    }

    public enum Foundry
    {
        
    }

    public enum IntegratedGraphics
    {
        
    }

    public abstract class CPU
    {
        //physical attributes
        private string Name;
        private Foundry CpuFoundry;
        private int TransistorCount;
        private int DieSize;
        private int Package;
        private int TCaseMax;
        private int ProcessSize;
        private Socket Socket;
        //cores
        private int NumberOfCores;
        private int NumberOfThreads;
        private int NumberOfSmps;
        private bool HasIntegratedGraphics;
        private IntegratedGraphics IGPU;
        //cache
        private int L1Cache;
        private int L2Cache;
        private int L3Cache;
        //features
        private string[] AvailableFeatures;
        //performance
        private int Frequency;
        private int TurboClock;
        private int BaseClock;
        private int Multiplier;
        private bool MultiplierUnlocked;
        private int TDP;
        //architecture
        private string Market;
        private string ProductionStatus;
        private DateTime ReleaseDate;
        private string Codename;
        private string Generation;
        private string PartNumber;
        private string MemorySupport;
        private bool SupportsECC;
        private int PCIExpressGenSupport;
        //other
        private Manufacturer Manufacturer;
        private CoreArchitecture CoreArchitecture;

        public CPU(string name)
        {
            Name = name;
        }

        public CPU ComputerLookup(string name)
        {
            return null;
        }
    }

    public enum OperatingSystemGeneral
    {
        Windows,
        MacOsx,
        Linux,
        UnknownOperatingSystemGeneral
    }
    
    public class Computer
    {
        private OperatingSystemGeneral OSGeneral;
        private OperatingSystem OS;
        private string DefaultDirectory;
        private string UserName;
        public string OsPathSeperator;
        public Computer()
        {
            UserName = System.Environment.UserName;
            PlatformID platformId = System.Environment.OSVersion.Platform;
            Version platformVersion = System.Environment.OSVersion.Version;
            if (platformId == PlatformID.MacOSX)
            {
                OSGeneral = OperatingSystemGeneral.MacOsx;
                OS = MacosNameEquivalence(platformVersion);
                DefaultDirectory = "/Users/" + UserName + "/Library/Application Support";
                OsPathSeperator = "/";
            }
            else if (platformId == PlatformID.Win32NT)
            {
                OSGeneral = OperatingSystemGeneral.Windows;
                OS = WindowsVersionEquivalence(platformVersion);
                DefaultDirectory = "C:\\" + UserName + "\\AppData\\Local";
                OsPathSeperator = "\\";
            }
            else if (platformId == PlatformID.Unix)
            {
                OSGeneral = OperatingSystemGeneral.Linux;
                OS = OperatingSystem.Linux;
                DefaultDirectory = "~/.thunderbird";
                OsPathSeperator = "/";
            }
            else
            {
                OSGeneral = OperatingSystemGeneral.UnknownOperatingSystemGeneral;
                OS = OperatingSystem.UnkownOperatingSystem;
            }
        }
        
        public OperatingSystem MacosNameEquivalence(Version macOsVersion)
        {
            Dictionary<int, OperatingSystem> macosNameEquivalenceDictionary = new Dictionary<int, OperatingSystem>();
            macosNameEquivalenceDictionary.Add(6, OperatingSystem.MJaguar);
            macosNameEquivalenceDictionary.Add(7, OperatingSystem.MPanther);
            macosNameEquivalenceDictionary.Add(8, OperatingSystem.MTiger);
            macosNameEquivalenceDictionary.Add(9, OperatingSystem.MLeopard);
            macosNameEquivalenceDictionary.Add(10, OperatingSystem.MSnowLeropard);
            macosNameEquivalenceDictionary.Add(11, OperatingSystem.MLion);
            macosNameEquivalenceDictionary.Add(12, OperatingSystem.MMountainLion);
            macosNameEquivalenceDictionary.Add(13, OperatingSystem.MMavericks);
            macosNameEquivalenceDictionary.Add(14, OperatingSystem.MYosemite);
            macosNameEquivalenceDictionary.Add(15, OperatingSystem.MElCapitan);
            macosNameEquivalenceDictionary.Add(16, OperatingSystem.MSierra);
            macosNameEquivalenceDictionary.Add(17, OperatingSystem.MHighSierra);
            macosNameEquivalenceDictionary.Add(18, OperatingSystem.MMojave);
            macosNameEquivalenceDictionary.Add(19, OperatingSystem.MCatalina);
            macosNameEquivalenceDictionary.Add(20, OperatingSystem.MBigSur);
            OperatingSystem system;
            if (macOsVersion.Major >= 6)
                system = macosNameEquivalenceDictionary[macOsVersion.Major];
            else if (macOsVersion.Major >= 1 && macOsVersion.MajorRevision >= 4 && macOsVersion.Minor >= 1)
                system = OperatingSystem.MPuma;
            else if (macOsVersion.Major >= 1 && macOsVersion.MajorRevision >= 3 && macOsVersion.Minor >= 1)
                system = OperatingSystem.MCheetah;
            else
                system = OperatingSystem.UnkownOperatingSystem;
            return system;
        }

        public OperatingSystem WindowsVersionEquivalence(Version windowsVersion)
        {
            OperatingSystem system;
            Dictionary<double, OperatingSystem> windowsVersionEquivalenceDictionary = new Dictionary<double, OperatingSystem>();
            windowsVersionEquivalenceDictionary.Add(5.0, OperatingSystem.W2000);
            windowsVersionEquivalenceDictionary.Add(5.1, OperatingSystem.WXp);
            windowsVersionEquivalenceDictionary.Add(5.2, OperatingSystem.WXp64BitEdition);
            //windowsVersionEquivalenceDictionary.Add(5.2, OperatingSystem.WServer2003);
            //windowsVersionEquivalenceDictionary.Add(5.2, OperatingSystem.WServer2003R2);
            windowsVersionEquivalenceDictionary.Add(6.0, OperatingSystem.WVista);
            //windowsVersionEquivalenceDictionary.Add(6.1, OperatingSystem.WServer2008R2);
            windowsVersionEquivalenceDictionary.Add(6.1, OperatingSystem.W7);
            //windowsVersionEquivalenceDictionary.Add(6.2, OperatingSystem.WServer2012);
            windowsVersionEquivalenceDictionary.Add(6.2, OperatingSystem.W8);
            //windowsVersionEquivalenceDictionary.Add(6.3, OperatingSystem.WServer2003R2);
            windowsVersionEquivalenceDictionary.Add(6.3, OperatingSystem.W8P1);
            //windowsVersionEquivalenceDictionary.Add(10.0, OperatingSystem.WServer2016);
            //windowsVersionEquivalenceDictionary.Add(10.0, OperatingSystem.WServer2019);
            windowsVersionEquivalenceDictionary.Add(10.0, OperatingSystem.W10);
            try
            {
                system = windowsVersionEquivalenceDictionary[windowsVersion.Major + windowsVersion.Minor];
            }
            catch (Exception e)
            {
                system = OperatingSystem.UnkownOperatingSystem;
            }

            return system;
        }

        public string GetDefaultDirectory()
        {
            return DefaultDirectory;
        }

        public bool IsWindows()
        {
            return OSGeneral is OperatingSystemGeneral.Windows;
        }

        public bool IsMacOs()
        {
            return OSGeneral is OperatingSystemGeneral.MacOsx;
        }

        public bool IsLinux()
        {
            return OSGeneral is OperatingSystemGeneral.Linux;
        }
    }
}
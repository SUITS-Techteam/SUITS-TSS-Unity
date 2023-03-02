using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSS.Msgs
{
    [System.Serializable]
    public class TSSMsg
    {
        public List<GPSMsg> GPS;
        public List<IMUMsg> IMU;
        public List<EVAMsg> EVA;
        public List<UIAMsg> UIA;
    }

    [System.Serializable]
    public class GPSMsg
    {
        public int id;
        public string device;
        public int mode;
        public string time;
        public double ept;
        public double lat;
        public double lon;
        public double alt;
        public double epx;
        public double epy;
        public double epv;
        public double track;
        public double speed;
        public double climb;
        public double eps;
        public double epc;
    }

    [System.Serializable]
    public class IMUMsg
    {
        public int id;
        public double heading;
        public double accel_x;
        public double accel_y;
        public double accel_z;
        public double gyro_x;
        public double gyro_y;
        public double gyro_z;
        public double mag_x;
        public double mag_y;
        public double mag_z;
    }

    [System.Serializable]
    public class EVAMsg
    {
        public int id;
        public int room;
        public bool isRunning;
        public bool isPaused;
        public double time;
        public string timer;
        public string startedAt;
        public double heart_bpm;
        public double p_sub;
        public double p_suit;
        public double t_sub;
        public double v_fan;
        public double p_o2;
        public double rate_o2;
        public double batteryPercent;
        public double cap_battery;
        public double battery_out;
        public double p_h2o_g;
        public double p_h2o_l;
        public double p_sop;
        public double rate_sop;
        public string t_battery;
        public double t_oxygenPrimary;
        public double t_oxygenSec;
        public double ox_primary;
        public double ox_secondary;
        public string t_oxygen;
        public double cap_water;
        public string t_water;
        public string createdAt;
        public string updatedAt;
    }

    [System.Serializable]
    public class UIAMsg
    {
        public int id;
        public int room;
        public string started_at;
        public bool emu1;
        public bool ev1_supply;
        public bool emu1_O2;
        public bool emu2;
        public bool ev2_supply;
        public bool ev_waste;
        public bool emu2_O2;
        public bool O2_vent;
        public bool depress_pump;
        public string createdAt;
        public string updatedAt;
    }
}
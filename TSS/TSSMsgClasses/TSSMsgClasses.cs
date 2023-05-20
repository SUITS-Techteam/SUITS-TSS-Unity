using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSS.Msgs
{
    [System.Serializable]
    public class TSSMsg
    {
        public GPSMsg gpsMsg;
        public IMUMsg imuMsg;
        public SimulationStates simulationStates;
        public SimulationFailures simulationFailures;
        public UIAMsg uiaMsg;
        public SpecMsg specMsg;
        public RoverMsg roverMsg;
    }

    [System.Serializable]
    public class GPSMsg
    {
        public int mode;
        public string fix_status;
        public string time;
        public float ept;
        public float lat;
        public float lon;
        public float alt;
        public float epx;
        public float epy;
        public float epv;
        public float track;
        public float speed;
        public float climb;
        public float eps;
        public float epc;
    }

    [System.Serializable]
    public class IMUMsg
    {
        public string user_guid;
        public float heading;
        public float accel_x;
        public float accel_y;
        public float accel_z;
        public float gyro_x;
        public float gyro_y;
        public float gyro_z;
        public float mag_x;
        public float mag_y;
        public float mag_z;
    }

    [System.Serializable]
    public class SimulationStates
    {
        public int room_id;
        public bool isRunning;
        public bool isPaused;
        public float time;
        public string timer;
        public float primary_oxygen;
        public float secondary_oxygen;
        public float suit_pressure;
        public float sub_pressure;
        public float o2_pressure;
        public float o2_rate;
        public float h2o_gas_pressure;
        public float h2o_liquid_pressure;
        public float sop_pressure;
        public float sop_rate;
        public float heart_rate;
        public float fan_tachometer;
        public float battery_capacity;
        public float temperature;
        public float battery_time_left;
        public float o2_time_left;
        public float h2o_time_left;
        public float battery_percentage;
        public float battery_output;
        public float oxygen_primary_time;
        public float oxygen_secondary_time;
        public float water_capacity;
    }

    [System.Serializable]
    public class SimulationFailures
    {
        public string started_at;
        public bool o2_error;
        public bool pump_error;
        public bool power_error;
        public bool fan_error;
    }

    [System.Serializable]
    public class UIAMsg
    {
        public int room_id;
        public string started_at;
        public bool emu1_pwr_switch;
        public bool ev1_supply_switch;
        public bool emu1_water_waste;
        public bool emu1_o2_supply_switch;
        public bool o2_vent_switch;
        public bool depress_pump_switch;
        // public string createdAt;
        // public string updatedAt;
    }

    [System.Serializable]
    public class UIAState
    {
        public bool emu1_is_booted;
        public float uia_supply_pressure;
        public float water_level;
        public float airlock_pressure;
        public bool depress_pump_fault;
    }

    [System.Serializable]
    public class SpecMsg
    {
        public float SiO2;
        public float TiO2;
        public float Al2O3;
        public float FeO;
        public float MnO;
        public float MgO;
        public float CaO;
        public float K2O;
        public float P2O3;
    }
    
    [System.Serializable]
    public class RoverMsg
    {
        public float lat;
        public float lon;

        public string navigation_status;    // either "NAVIGATING" or "NOT_NAVIGATING"
        public float goal_lat;
        public float goal_lon;
    }
    
    [System.Serializable]
    public class HMDInfo
    {
        public string team_name;
        public string username;
        public string university;
        public string user_guid;

        public HMDInfo(string team_name, string username, string university, string user_guid)
        {
            this.team_name = team_name;
            this.username = username;
            this.university = university;
            this.user_guid = user_guid;
        }

    }

    // The following are all for the HMD registration
    [System.Serializable]
    public class HMDRegistration
    {
        public string MSGTYPE = "DATA";
        public HMDRegistrationBlob BLOB;

        public HMDRegistration(HMDInfo hmd_info)
        {
            this.BLOB = new HMDRegistrationBlob(hmd_info);
        }
    }

    [System.Serializable]
    public class HMDRegistrationBlob
    {
        public string DATATYPE = "HMD";
        public HMDInfo DATA;

        public HMDRegistrationBlob(HMDInfo hmd_info)
        {
            this.DATA = hmd_info;
        }
    }


    // The following are for messages sent to the TSS by the user
    [System.Serializable]
    public class RoverNavigateMsg
    {
        public RoverField rover;
        public RoverNavigateMsg(float goal_lat, float goal_lon)
        {
            this.rover = new RoverField(goal_lat, goal_lon);
        }

        [System.Serializable]
        public class RoverField
        {
            public string cmd;
            public float goal_lat;
            public float goal_lon;
            public RoverField(float goal_lat, float goal_lon)
            {
                this.cmd = "navigate";
                this.goal_lat = goal_lat;
                this.goal_lon = goal_lon;
            }
        }

    }

    [System.Serializable]
    public class RoverRecallMsg
    {
        public RoverField rover;

        public RoverRecallMsg()
        {
            this.rover = new RoverField();
        }

        [System.Serializable]
        public class RoverField
        {
            public string cmd;
            public RoverField()
            {
                this.cmd = "recall";
            }
        }

    }
}

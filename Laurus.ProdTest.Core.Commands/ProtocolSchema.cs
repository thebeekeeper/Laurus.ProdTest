using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtTester.Core.Enum
{
    public class ProtocolSchema
    {
        public enum Commands
        {
            TY_CMD_REQUEST_DATA = 0,
            TY_CMD_BEEP = 1,
            TY_CMD_ILLUMINATE = 2,
            TY_CMD_CANCEL_ALARM = 3,
            TY_CMD_KEYFINDER = 4, //ignores alarm settings, will ight & beep.
            TY_CMD_ENABLE_ALARM = 5, //setting global disc alarms on
            TY_CMD_DISABLE_ALARM = 6, //setting global disc alarms off
            TY_CMD_SET_BEEP_ENABLED = 7, //setting when disc alarms, it should beep.
            TY_CMD_SET_BEEP_DISABLED = 8, //setting when disc alarms, it should not beep.
            TY_CMD_SET_LIGHT_ENABLED = 9, //setting when disc alarms, it should light up.
            TY_CMD_SET_LIGHT_DISABLED = 10,
            TY_CMD_SLEEP_UNTIL_MOTION = 11,
            TY_CMD_GET_VERSION = 12,
            TY_CMD_DO_DIAGNOSTIC = 13,
            TY_CMD_RESET_TO_FACTORY = 14,
            TY_CMD_USER_DISCONNECT = 15,
            TY_CMD_DO_ALARM = 16,
            TY_CMD_SET_ALARM_THRESHOLD = 17,
            TY_CMD_SET_ALARM_PROXIMITY_ENABLED = 18,
            TY_CMD_SET_ALARM_PROXIMITY_DISABLED = 19,
            TY_CMD_SET_ALARM_MOVEMENT_ENABLED = 20,
            TY_CMD_SET_ALARM_MOVEMENT_DISABLED = 21
        }

        //when receiving command, reply with ack, then command back.
        public static readonly int TY_ACK = 127;

        public enum Events
        {
            TY_EVT_MOTION_DETECTED = 126, //sent when disc gets motion event.
            TY_EVT_CANCEL_ALARM = 125,
            TY_EVT_PHONEFINDER = 124,
            TY_EVT_CHARGING = 123, //depricated.
            TY_EVT_NOT_CHARGING = 122, //depricated.
            TY_EVT_RESET_TO_FACTORY = 121,
            TY_EVT_USER_DISCONNECT = 120 //should only be sent if connected and user does key sequence.  
        }
    }
}
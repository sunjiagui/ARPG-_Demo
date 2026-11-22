using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///条件ID（枚举）
    ///</summary>
    public enum TriggerID
    {
        NoHealth,
        SawTarget,
        ReachAttakeRange,
        CompletePatrol,
        KilledTarget,
        WithoutTargetRange,//看不见目标
        ExitAttakeRange//逃出攻击距离



    }
}


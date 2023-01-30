﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myd.Platform.Demo
{
    //这里涉及坐标的数值需要/10, 除时间类型
    public interface Constants
    {
        const float Gravity = 90f; //重力

        const float HalfGravThreshold = 4f;
        const float MaxFall = -16; //普通最大下落速度
        const float FastMaxFall = -24f;  //快速最大下落速度
        const float FastMaxAccel = 30f; //快速下落加速度
        //最大移动速度
        const float MaxRun = 9f;
        //Hold情况下的最大移动速度
        const float HoldingMaxRun = 7f;
        //空气阻力
        const float AirMult = 0.065f;
        //移动加速度
        const float RunAccel = 100f;
        //移动减速度
        const float RunReduce = 40f;
        //
        const float JumpSpeed = 10.5f;  //最大跳跃速度
        const float VarJumpTime = 0.2f; //跳跃持续时间(跳起时,会持续响应跳跃按键[VarJumpTime]秒,影响跳跃的最高高度);

        const float JumpGraceTime = 0.1f;//土狼时间

        const float WallSpeedRetentionTime = .06f; //撞墙以后可以允许的保持速度的时间


        #region Dash相关参数
        const float DashSpeed = 24f;           //冲刺速度
        const float EndDashSpeed = 16f;        //结束冲刺速度
        const float EndDashUpMult = .75f;       //如果向上冲刺，阻力。
        const float DashTime = .15f;            //冲刺时间
        const float DashCooldown = .2f;         //冲刺冷却时间，
        const float DashRefillCooldown = .1f;   //冲刺重新装填时间
        const int DashHJumpThruNudge = 6;       //
        const int DashCornerCorrection = 4;     //水平Dash时，遇到阻挡物的可纠正像素值
        const int DashVFloorSnapDist = 3;       //DashAttacking下的地面吸附像素值
        const float DashAttackTime = .3f;       //
        #endregion
    }
}

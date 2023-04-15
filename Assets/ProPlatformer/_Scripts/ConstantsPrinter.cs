using System;
using Myd.Common;

namespace Myd.Platform
{
    public static class ConstantsPrinter
    {
        // 打印所有常量值
        public static void Print()
        {
            string s = "\n---------------------------------------------------" +
                       "\n Gravity[重力]:" + Constants.Gravity +
                       "\n HalfGravThreshold[滞空时间阈值]:" + Constants.HalfGravThreshold +
                       "\n MaxFall[普通最大下落速度]:" + Constants.MaxFall +
                       "\n FastMaxFall[快速最大下落速度]:" + Constants.FastMaxFall +
                       "\n FastMaxAccel[快速下落加速度]:" + Constants.FastMaxAccel +
                       "\n MaxRun[最大移动速度]:" + Constants.MaxRun +
                       "\n HoldingMaxRun[Hold情况下的最大移动速度]:" + Constants.HoldingMaxRun +
                       "\n AirMult[空气阻力]:" + Constants.AirMult +
                       "\n RunAccel[移动加速度]:" + Constants.RunAccel +
                       "\n RunReduce[移动减速速度]:" + Constants.RunReduce +
                       "\n JumpSpeed[最大跳跃速度]:" + Constants.JumpSpeed +
                       "\n VarJumpTime[跳跃持续时间]:" + Constants.VarJumpTime +
                       "\n JumpHBoost[退离墙壁的力]:" + Constants.JumpHBoost +
                       "\n JumpGraceTime[土狼时间]:" + Constants.JumpGraceTime +
                       "\n WallJumpCheckDist[墙上跳跃检测距离]:" + Constants.WallJumpCheckDist +
                       "\n WallJumpForceTime[墙上跳跃强制时间]:" + Constants.WallJumpForceTime +
                       "\n WallJumpHSpeed[从墙上跳开的水平速度]:" + Constants.WallJumpHSpeed +
                       "\n SuperJumpSpeed[超级跳速度]:" + Constants.SuperJumpSpeed +
                       "\n SuperJumpH[超级跳水平速度]:" + Constants.SuperJumpH +
                       "\n SuperWallJumpSpeed[超级墙跳速度]:" + Constants.SuperWallJumpSpeed +
                       "\n SuperWallJumpVarTime[超级墙跳水平速度]:" + Constants.SuperWallJumpVarTime +
                       "\n SuperWallJumpForceTime[超级墙跳强制时间]:" + Constants.SuperWallJumpForceTime +
                       "\n SuperWallJumpH[超级墙跳 跳开的水平速度]:" + Constants.SuperWallJumpH +
                       "\n WallSpeedRetentionTime[撞墙以后可以允许的保持速度的时间]:" + Constants.WallSpeedRetentionTime +
                       "\n WallSlideTime[墙壁滑行时间]:" + Constants.WallSlideTime +
                       "\n WallSlideStartMax[墙壁下滑最大初始速度]:" + Constants.WallSlideStartMax +
                       "\n DashSpeed[冲刺速度]:" + Constants.DashSpeed +
                       "\n EndDashSpeed:[结束冲刺速度]" + Constants.EndDashSpeed +
                       "\n EndDashUpMult[如果向上冲刺，阻力]:" + Constants.EndDashUpMult +
                       "\n DashTime[冲刺时间]:" + Constants.DashTime +
                       "\n DashCoolDown[冲刺冷却时间]:" + Constants.DashCooldown +
                       "\n DashRefillCooldown[冲刺重新装填时间]:" + Constants.DashRefillCooldown +
                       //"\n DashHJumpThruNudge[]:" + Constants.DashHJumpThruNudge +
                       "\n DashCornerCorrection[水平Dash时，遇到阻挡物的可纠正像素值]:" + Constants.DashCornerCorrection +
                       "\n DashVFloorSnapDist[DashAttacking下的地面吸附像素值]:" + Constants.DashVFloorSnapDist +
                       "\n DashAttackTime[冲刺后可攻击时间]:" + Constants.DashAttackTime +
                       "\n MaxDashes[连续冲刺最大数]:" + Constants.MaxDashes +
                       "\n DashFreezeTime[冲刺开始冻帧时间]:" + Constants.DashFreezeTime +
                       "\n ClimbMaxStamina[最大耐力]:" + Constants.ClimbMaxStamina +
                       "\n ClimbUpCost[向上爬的耐力消耗]:" + Constants.ClimbUpCost +
                       "\n ClimbStillCost[爬着不动耐力消耗]:" + Constants.ClimbStillCost +
                       "\n ClimbJumpCost[爬着跳跃耐力消耗]:" + Constants.ClimbJumpCost +
                       "\n ClimbCheckDist[攀爬检查像素值]:" + Constants.ClimbCheckDist +
                       "\n ClimbUpCheckDist[向上攀爬检查像素值]:" + Constants.ClimbUpCheckDist +
                       "\n ClimbNoMoveTime[攀爬保持静止时间]:" + Constants.ClimbNoMoveTime +
                       "\n ClimbTiredThreshold[表现疲惫的阈值]:" + Constants.ClimbTiredThreshold +
                       "\n ClimbUpSpeed[上爬速度]:" + Constants.ClimbUpSpeed +
                       "\n ClimbDownSpeed[下爬速度]:" + Constants.ClimbDownSpeed +
                       "\n ClimbSlipSpeed[下滑速度]:" + Constants.ClimbSlipSpeed +
                       "\n ClimbAccel[下滑加速度]:" + Constants.ClimbAccel +
                       "\n ClimbGrabYMult[攀爬时抓取导致的Y轴速度衰减]:" + Constants.ClimbGrabYMult +
                       "\n ClimbHopY[Hop的Y轴速度]:" + Constants.ClimbHopY +
                       "\n ClimbHopX[Hop的X轴速度]:" + Constants.ClimbHopX +
                       "\n ClimbHopForceTime[Hop时间]:" + Constants.ClimbHopForceTime +
                       "\n ClimbJumpBoostTime[WallBoost时间]:" + Constants.ClimbJumpBoostTime +
                       "\n ClimbHopNoWindTime[Wind情况下,Hop会无风0.3秒]:" + Constants.ClimbHopNoWindTime +
                       "\n DuckFriction[低头摩擦]:" + Constants.DuckFriction +
                       "\n DuckSuperJumpXMult[低头超级跳X轴速度衰减]:" + Constants.DuckSuperJumpXMult +
                       "\n DuckSuperJumpYMult[低头超级跳Y轴速度衰减]:" + Constants.DuckSuperJumpYMult +
                       "\n UpwardCornerCorrection[向上移动，X轴上边缘校正的最大距离]:" + Constants.UpwardCornerCorrection +
                       "\n LaunchedMinSpeedSq[最小航速]:" + Constants.LaunchedMinSpeedSq +
                       "\n---------------------------------------------------";

            Logging.Log(s);
        }
    }
}
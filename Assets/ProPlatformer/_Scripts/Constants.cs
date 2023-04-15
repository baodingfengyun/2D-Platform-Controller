namespace Myd.Platform
{
    //这里涉及坐标的数值需要/10, 除时间类型
    //主要模仿对象 Celeste 是一个像素游戏, 最小单位是 1 像素, 相当于 0.1单位.
    public static class Constants
    {

        public static bool EnableWallSlide = true;
        public static bool EnableJumpGrace = true;
        public static bool EnableWallBoost = true;

        public static float Gravity = 90f;          //重力(相当于 900 像素每秒)

        public static float HalfGravThreshold = 4f; //滞空时间阈值
        public static float MaxFall = -16;          //普通最大下落速度
        public static float FastMaxFall = -24f;     //快速最大下落速度
        public static float FastMaxAccel = 30f;     //快速下落加速度
        
        public static float MaxRun = 9f;            //最大移动速度
        public static float HoldingMaxRun = 7f;     //Hold情况下的最大移动速度
        
        public static float AirMult = 0.65f;        //空气阻力
        public static float RunAccel = 100f;        //移动加速度
        public static float RunReduce = 40f;        //移动减速度
        public static float JumpSpeed = 10.5f;      //最大跳跃速度

        public static float VarJumpTime = 0.2f;     //跳跃持续时间(跳起时,会持续响应
                                                    //跳跃按键[VarJumpTime]秒,
                                                    //影响跳跃的最高高度)

        public static float JumpHBoost = 4f;        //退离墙壁的力
        public static float JumpGraceTime = 0.1f;   //土狼时间

        #region WallJump
        public static float WallJumpCheckDist = 0.3f;   //墙上跳跃检测距离
        public static float WallJumpForceTime = .16f;   //墙上跳跃强制时间
        public static float WallJumpHSpeed = MaxRun + JumpHBoost;   //从墙上跳开的水平速度

        #endregion

        #region SuperWallJump
        public static float SuperJumpSpeed = JumpSpeed;     //超级跳速度
        public static float SuperJumpH = 26f;               //超级跳水平速度
        public static float SuperWallJumpSpeed = 16f;       //超级墙跳速度
        public static float SuperWallJumpVarTime = .25f;    //超级墙跳水平速度
        public static float SuperWallJumpForceTime = .2f;   //超级墙跳强制时间
        public static float SuperWallJumpH = MaxRun + JumpHBoost* 2;    //超级墙跳
                                                            //跳开的水平速度
        #endregion
        #region WallSlide
        public static float WallSpeedRetentionTime = .06f;  //撞墙以后可以允许的保持速度的时间
        public static float WallSlideTime = 1.2f;           //墙壁滑行时间
        public static float WallSlideStartMax = -2f;        //墙壁下滑最大初始速度


        #endregion

        #region Dash相关参数
        public static float DashSpeed = 24f;            //冲刺速度
        public static float EndDashSpeed = 16f;         //结束冲刺速度
        public static float EndDashUpMult = .75f;       //如果向上冲刺，阻力。
        public static float DashTime = .15f;            //冲刺时间
        public static float DashCooldown = .2f;         //冲刺冷却时间，
        public static float DashRefillCooldown = .1f;   //冲刺重新装填时间
        public static int DashHJumpThruNudge = 6;       //
        public static int DashCornerCorrection = 4;     //水平Dash时，遇到阻挡物的可纠正像素值
        public static int DashVFloorSnapDist = 3;       //DashAttacking下的地面吸附像素值
        public static float DashAttackTime = .3f;       //冲刺后可攻击时间
        public static int MaxDashes = 1;                //连续冲刺最大数
        public static float DashFreezeTime = .05f;      //冲刺开始冻帧时间
        #endregion

        #region Climb参数
        public static float ClimbMaxStamina = 110;      //最大耐力
        public static float ClimbUpCost = 100 / 2.2f;   //向上爬的耐力消耗
        public static float ClimbStillCost = 100 / 10f; //爬着不动耐力消耗
        public static float ClimbJumpCost = 110 / 4f;   //爬着跳跃耐力消耗
        public static int ClimbCheckDist = 2;           //攀爬检查像素值
        public static int ClimbUpCheckDist = 2;         //向上攀爬检查像素值
        public static float ClimbNoMoveTime = .1f;      //攀爬保持静止时间
        public static float ClimbTiredThreshold = 20f;  //表现疲惫的阈值
        public static float ClimbUpSpeed = 4.5f;        //上爬速度
        public static float ClimbDownSpeed = -8f;       //下爬速度
        public static float ClimbSlipSpeed = -3f;       //下滑速度
        public static float ClimbAccel = 90f;           //下滑加速度
        public static float ClimbGrabYMult = .2f;       //攀爬时抓取导致的Y轴速度衰减
        public static float ClimbHopY = 12f;            //Hop的Y轴速度
        public static float ClimbHopX = 10f;            //Hop的X轴速度
        public static float ClimbHopForceTime = .2f;    //Hop时间
        public static float ClimbJumpBoostTime = .2f;   //WallBoost时间
        public static float ClimbHopNoWindTime = .3f;   //Wind情况下,Hop会无风0.3秒
        #endregion

        #region Duck参数
        public static float DuckFriction = 50f;         //低头摩擦
        public static float DuckSuperJumpXMult = 1.25f; //低头超级跳X轴速度衰减
        public static float DuckSuperJumpYMult = .5f;   //低头超级跳Y轴速度衰减
        #endregion

        #region Corner Correct
        public static int UpwardCornerCorrection = 4; //向上移动，X轴上边缘校正的最大距离
        #endregion

        public static float LaunchedMinSpeedSq = 196; //最小航速

        #region 层级
        public static string LAYER_GROUND = "Ground";
        #endregion

    }
}

﻿using System;
using System.Collections;
using Myd.Common;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 爬墙状态
    /// </summary>
    public class ClimbState : BaseActionState
    {
        public ClimbState(PlayerController context) : base(EActionState.Climb, context)
        {
        }

        public override IEnumerator Coroutine()
        {
            yield return null;
        }

        public override bool IsCoroutine()
        {
            return false;
        }

        //进入状态
        public override void OnBegin()
        {
            this.onStateTime = 0f;
            this.onStateFrames = 0;

            // 速度调整
            ctx.Speed.x = 0;
            ctx.Speed.y *= Constants.ClimbGrabYMult; //0.2
            //TODO 其他参数
            ctx.WallSlideTimer = Constants.WallSlideTime; //1.2
            ctx.WallBoost?.ResetTime();
            ctx.ClimbNoMoveTimer = Constants.ClimbNoMoveTime; //0.1

            //两个像素的吸附功能
            ctx.ClimbSnap();
            //TODO 表现
        }

        public override void OnEnd()
        {
            //TODO
            if (this.onStateTime > 0f || this.onStateFrames > 0)
            {
                Logging.Log("ClimbState 时间(秒):" + this.onStateTime + ", 帧数:" + this.onStateFrames);
            }
        }

        public override EActionState Update(float deltaTime)
        {
            this.onStateTime += deltaTime;
            this.onStateFrames++;
            ctx.ClimbNoMoveTimer -= deltaTime;

            //处理跳跃
            #region 攀爬中的跳跃
            if (GameInput.Jump.Pressed() && (!ctx.Ducking || ctx.CanUnDuck))
            {
                if (ctx.MoveX == -(int)ctx.Facing)
                    ctx.WallJump(-(int)ctx.Facing);
                else
                    ctx.ClimbJump();

                return EActionState.Normal;
            }
            if (ctx.CanDash)
            {
                return this.ctx.Dash();
            }
            #endregion

            //放开抓取键,则回到Normal状态
            #region 结束攀爬
            if (!GameInput.Grab.Checked())
            {
                //Speed += LiftBoost;
                //Play(Sfxs.char_mad_grab_letgo);
                return EActionState.Normal;
            }
            #endregion

            //检测前面的墙面是否存在
            #region 碰撞检测
            if (!ctx.CollideCheck(ctx.Position, Vector2.right * (int)ctx.Facing))
            {
                //Climbed over ledge?
                if (ctx.Speed.y < 0)
                {
                    //if (ctx.WallBoosting)
                    //{
                        //    Speed += LiftBoost;
                        //    Play(Sfxs.char_mad_grab_letgo);
                    //}
                    //else
                    {
                        ClimbHop(); //自动翻越墙面
                    }
                }

                return EActionState.Normal;
            }
            #endregion

            #region 攀爬
            {
                //Climbing
                float target = 0;
                bool trySlip = false;
                if (ctx.ClimbNoMoveTimer <= 0)
                {
                    if (false)//(ClimbBlocker.Check(Scene, this, Position + Vector2.UnitX * (int)Facing))  
                    {
                        //trySlip = true;
                    }
                    else if (ctx.MoveY == 1)
                    {
                        //往上爬
                        target = Constants.ClimbUpSpeed;
                        //向上攀爬的移动限制,顶上有碰撞或者SlipCheck
                        if (ctx.CollideCheck(ctx.Position, Vector2.up))
                        {
                            Debug.Log("=======ClimbSlip_Type1");
                            ctx.Speed.y = Mathf.Min(ctx.Speed.y, 0);
                            target = 0;
                            trySlip = true;
                        }
                        //如果在上面0.6米处存在障碍，且前上方0.1米处没有阻碍，依然不允许向上
                        else if (ctx.ClimbHopBlockedCheck() && ctx.SlipCheck(0.1f)){
                            Debug.Log("=======ClimbSlip_Type2");
                            ctx.Speed.y = Mathf.Min(ctx.Speed.y, 0);
                            target = 0;
                            trySlip = true;
                        }
                        //如果前上方没有阻碍, 则进行ClimbHop
                        else if (ctx.SlipCheck())
                        {
                            //Hopping
                            ClimbHop();
                            return EActionState.Normal;
                        }
                    }
                    else if (ctx.MoveY == -1)
                    {
                        //往下爬
                        target = Constants.ClimbDownSpeed;

                        if (ctx.OnGround)
                        {
                            ctx.Speed.y = Mathf.Max(ctx.Speed.y, 0);    //落地时,Y轴速度>=0
                            target = 0;
                        }
                        else
                        {
                            //创建WallSlide粒子效果
                            ctx.PlayWallSlideEffect(Vector2.right * (int)ctx.Facing);
                        }
                    }
                    else
                    {
                        trySlip = true;
                    }
                }
                else
                {
                    trySlip = true;
                }

                //滑行
                if (trySlip && ctx.SlipCheck())
                {
                    Debug.Log("=======ClimbSlip_Type4");
                    target = Constants.ClimbSlipSpeed;
                }
                ctx.Speed.y = Mathf.MoveTowards(ctx.Speed.y, target, Constants.ClimbAccel * deltaTime);
            }
            #endregion

            //TrySlip导致的下滑在碰到底部的时候,停止下滑
            #region 特殊处理
            if (ctx.MoveY != -1 && ctx.Speed.y < 0 && !ctx.CollideCheck(ctx.Position, new Vector2((int)ctx.Facing, -1)))
            {
                ctx.Speed.y = 0;
            }
            #endregion
            //TODO Stamina
            return state;
        }

        private void ClimbHop()
        {
            Debug.Log("=====ClimbHop");
            //播放Hop的精灵动画
            //playFootstepOnLand = 0.5f;

            //获取目标的落脚点
            bool hit = ctx.CollideCheck(ctx.Position, Vector2.right * (int)ctx.Facing);
            if (hit)
            {
                ctx.HopWaitX = (int)ctx.Facing;
                ctx.HopWaitXSpeed = (int)ctx.Facing * Constants.ClimbHopX;
            }
            //ctx.ClimbHopSolid = ctx.CollideClimbHop((int)ctx.Facing);
            //if (ctx.ClimbHopSolid)
            //{
            //    //climbHopSolidPosition = climbHopSolid.Position;
            //    ctx.HopWaitX = (int)ctx.Facing;
            //    ctx.HopWaitXSpeed = (int)ctx.Facing * Constants.ClimbHopX;
            //}
            else
            {
                ctx.HopWaitX = 0;
                ctx.Speed.x = (int)ctx.Facing * Constants.ClimbHopX;
            }

            ctx.Speed.y = Math.Max(ctx.Speed.y, Constants.ClimbHopY);
            ctx.ForceMoveX = 0;
            ctx.ForceMoveXTimer = Constants.ClimbHopForceTime;
        } 
    }
}

using System;
using Myd.Common;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 尾部快照
    /// </summary>
    public class TrailSnapshot : MonoBehaviour
    {
        // 缩放
        public Vector2 SpriteScale;
        public int Index;
        // 颜色
        public Color Color;
        // 进度
        public float Percent;
        // 持续总时间
        public float Duration;
        public bool Drawn;
        public bool UseRawDeltaTime;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        private Action onRemoved;
        public void Init(int index, Vector2 position, Sprite sprite, Vector2 scale, Color color, 
            float duration, int depth, bool frozenUpdate, bool useRawDeltaTime, Action onRemoved)
        {
            this.Index = index;
            this.SpriteScale = scale;
            this.Color = color;
            this.Percent = 0.0f;
            this.Duration = duration;
            this.spriteRenderer.sortingOrder = depth;
            this.Drawn = false;
            this.UseRawDeltaTime = useRawDeltaTime;
            this.spriteRenderer.color = color;
            this.spriteRenderer.sprite = sprite;
            this.transform.position = position;
            this.transform.localScale = scale;
            this.onRemoved = onRemoved;
        }

        //更新
        private void Update()
        {
            OnUpdate();
            OnRender();
        }

        //数据更新
        private void OnUpdate()
        {
            if ((double)this.Duration <= 0.0)
            {
                if (!this.Drawn)
                    return;
                Removed();
            }
            else
            {
                if ((double)this.Percent >= 1.0)
                {
                    Removed();
                }
                this.Percent += Time.deltaTime / this.Duration;
            }
        }

        //渲染效果
        private void OnRender()
        {
            float num = (double)this.Duration > 0.0 ? (float)(0.75 * (1.0 - (double)Ease.CubeOut(this.Percent))) : 1f;
            this.spriteRenderer.color = this.Color * num;
        }

        //清理相关
        private void Removed()
        {
            onRemoved?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
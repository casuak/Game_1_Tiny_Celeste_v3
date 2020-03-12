using System.Collections.Generic;
using Casuak.Extension._01_UnityComponent;
using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._02_Dash;
using TinyCeleste._02_Modules._03_Player._01_Action._04_Face;
using TinyCeleste._02_Modules._08_Proxy;
using UnityEngine;

namespace TinyCeleste._02_Modules._03_Player._02_Other._04_Hair
{
    public class HairFlow : EntityComponent
    {
        // 头发流
        public List<SpriteRenderer> hairList;

        // 历史位置记录数量
        public int positionCount = 5;

        // 历史位置列表
        public List<Vector2> positionList;
        
        // 视为玩家发生位移的最小移动距离
        public float minDistance;

        // 此属性仅在此System调用之后发生才有效
        private Vector2 lastPosition => positionList[positionList.Count - 2];
        public bool isPlayerMoved => (lastPosition - (Vector2) transform.localPosition).magnitude > minDistance;

        private new Transform transform;
        
        private Face face;
        private DashCount dashCount;
        private HairSprite hairSprite;
        
        private void Awake()
        {
            positionList = new List<Vector2>();

            transform = GetComponentNotNull<C_Transform2DProxy>().transform;
            face = GetComponentNotNull<Face>();
            dashCount = GetComponentNotNull<DashCount>();
            hairSprite = GetComponentNotNull<HairSprite>();
            
            for (int i = 0; i < positionCount; i++)
            {
                positionList.Add(transform.localPosition);
            }
        }

        // 重置头发流的位置
        public void ResetPlace()
        {
            foreach (var sr in hairList)
            {
                sr.transform.localPosition = transform.localPosition;
            }

            positionList.Clear();
            for (int i = 0; i < positionCount; i++)
            {
                positionList.Add(transform.localPosition);
            }
        }

        /// <summary>
        /// 设置每一个头发的颜色
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            foreach (var sr in hairList)
            {
                sr.color = color;
            }
        }

        /// <summary>
        /// 设置每一个头发的x轴朝向
        /// </summary>
        /// <param name="x">1, -1</param>
        public void SetXScale(int x)
        {
            foreach (var sr in hairList)
            {
                sr.transform.SetScaleX(x);
            }
        }

        public void HairFlowSystem()
        {
            // 调整HairFlow的朝向
            SetXScale((int) face.faceEnum);

            // 当前帧位置
            Vector2 currentPos = transform.localPosition;

            // 更新历史位置列表(每帧更新1次，最多保留n帧之前的位置)
            positionList.RemoveAt(0);
            positionList.Add(currentPos);

            // 依次设置每个头发的位置
            // 每隔deltaIndex，将下一个历史位置设置给下一个头发
            float deltaIndex = (float) positionList.Count / hairList.Count;
            for (int j = 0; j < hairList.Count; j++)
            {
                // 0 ~ m_PositionList.Count
                int index = Mathf.CeilToInt((j + 1) * deltaIndex) - 1;
                // 头发的顺序为从大到小，位置的顺序为从远（旧）到近（新）
                hairList[hairList.Count - j - 1].transform.localPosition = positionList[index];
            }
        }

        public void HairColorSystem()
        {
            hairSprite.SetColor(dashCount.currentColor);
            SetColor(dashCount.currentColor);
        }
    }
}
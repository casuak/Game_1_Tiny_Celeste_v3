using TinyCeleste._01_Framework;
using TinyCeleste._04_Extension._01_UnityComponent;
using UnityEngine;

namespace TinyCeleste._02_Modules._08_Proxy
{
    /// <summary>
    /// 多数Entity组件所在的Transform并不发生移动
    /// 移动的Transform Root则在某个子物体上
    /// 适用于2D游戏（Z轴坐标保持不变）
    ///
    /// 谈几点我对与Transform的看法
    /// 1、一般来说，对于一个有多个子物体组成的复杂物体（如Player），关键的Transform组件通常只有1 ~ 3个
    /// 2、不具有物理意义，仅仅作为一个Parent将同类或同属的子物体组织在一起，例如在GameManger底下放置的各种单例
    /// 3、通常不发生位移或很少发生，如移动平台的根Transform，一般不发生变化，但具有物理意义（对于路径点而言）
    /// 4、常见的会发生移动的Transform，且具有代表性
    /// </summary>
    public class C_Transform2DProxy : EntityComponent
    {
        public new Transform transform;

        public Vector2 pos
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.SetPositionXY(value);
            }
        }

        public void Translate(Vector2 pos)
        {
            transform.Translate(-pos);
        }
    }
}
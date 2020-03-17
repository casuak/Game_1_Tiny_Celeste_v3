using System.Collections.Generic;
using TinyCeleste._01_Framework;
using TinyCeleste._05_MyTool._06_Math;
using UnityEngine;

namespace TinyCeleste._02_Modules._05_Env._01_Map
{
    public partial class E_Map : Entity
    {
        // 网格参数
        public Grid grid;

        public Vector2 gridSize => grid.cellSize;
        public Vector2 gridGap => grid.cellGap;
        public Vector2 gridCenter => grid.transform.position;
        
        // 预制体
        public List<E_MapElement> mapElementList = new List<E_MapElement>();

        // 存储临时物体（如编辑模式下笔头）
        public Transform mapElementHolder;

        // 归一化的世界坐标(落入间隔为1的整数坐标)
        public Vector2Int WorldToGrid(Vector2 worldPos)
        {
            return Tool_Grid.WorldToGrid(worldPos, gridCenter, gridSize, gridGap);
        }

        public Vector2 GridToWorld(Vector2Int gridPos)
        {
            return Tool_Grid.GridToWorld(gridPos, gridCenter, gridSize, gridGap);
        }

        // 将坐标归入邻近的方块中心坐标
        public Vector2 WorldToSquareCenter(Vector2 worldPos)
        {
            return Tool_Grid.WorldToGridCenter(worldPos, gridCenter, gridSize, gridGap);
        }
    }
}
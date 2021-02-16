﻿using System;
using System.Collections.Generic;
using UnityEngine;


namespace SOScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelTile")]
    public class LevelTileSo : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
        public Transform visual;
        public int width;
        public int height;
        public LevelTheme levelTheme;

        public enum LevelTheme
        {
            TEST = 0,
            FOREST = 1,
            CAVE = 2
        }
        
        public static Dir GetNextDir(Dir dir){
           switch (dir)
           {
               case Dir.DOWN: return Dir.LEFT;
               case Dir.UP:   return Dir.RIGHT;   
               case Dir.LEFT: return Dir.UP;
               case Dir.RIGHT:return Dir.DOWN;
               default:
                   throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
           }
        }
        
        

        public enum Dir
        {
            DOWN = 0,
            UP = 1,
            LEFT = 2,
            RIGHT =3
        }


        public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
        {
            List<Vector2Int> gridPositionList = new List<Vector2Int>();
            switch (dir)
            {
                case Dir.DOWN:
                case Dir.UP:
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y <height; y++)
                        {
                            gridPositionList.Add(offset + new Vector2Int(x,y));
                        }
                        
                    }
                    break;
                case Dir.LEFT:
                case Dir.RIGHT:
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y <height; y++)
                        {
                            gridPositionList.Add(offset +new Vector2Int(x,y));
                            
                        }
                        
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
            return gridPositionList;
        }
        

    }
}

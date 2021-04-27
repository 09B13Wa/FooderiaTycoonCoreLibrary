using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace GameCore
{
    public class Map
    {
        public int maxX;
        public int maxY;
        public int maxZ;
        public List<Restaurant> Restaurants;
        public Dictionary<int, Customer> Customers;
        private List<Node> Nodes;

        public Map(int height, int width, string mapData)
        {
            //TODO:Loading
            throw new NotImplementedException();
        }
        
        public Map(int height, int width, int vertical, string mapData)
        {
            //TODO:Loading
            throw new NotImplementedException();
        }
        
        public Map(int height, int width, NewGame settings)
        {
            maxX = width;
            maxY = height;
            maxZ = -1;
        }
        
        public Map(int height, int width, int vertical, NewGame settings)
        {
            maxX = width;
            maxY = height;
            maxZ = vertical;
        }

        private TileData[,] GenerateNoiseMap()
        {
            throw new NotImplementedException();
        }

        public void GenerateMap()
        {
            
        }

        public void InitializeMap()
        {
            
        }

        private bool CheckMapStringValidity()
        {
            throw new NotImplementedException();
        }

        private void InitializeChildren()
        {
            
        }
    }
}
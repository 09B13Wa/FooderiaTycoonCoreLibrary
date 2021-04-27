using System;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;
using Random = System.Random;

namespace GameCore
{
    public enum TileKinds
    {
        ResidentialBuilding,
        WorkBuilding,
        PublicTransportHub,
        Park,
        BuildableAreas,
    }
    public struct TileData
    {
        public TileKinds TileKind { get; }
        public int NumberOfResidents { get; }
        public int NumberOfJobs { get; }
        public int Attractivity { get; }
        public Color RepresentedColor { get; set; }
        public int Height { get; set; }

        
        public TileData(TileKinds tileKind, int attractivity)
        { 
            Random rng = new Random();
            TileKind = tileKind;
            Attractivity = attractivity;
            if (tileKind == TileKinds.ResidentialBuilding)
            {
                NumberOfResidents = rng.Next(2,100);
                NumberOfJobs = 0;
                RepresentedColor = Color.DarkGreen;
                Height = NumberOfResidents / 5;
            }
            else if (tileKind == TileKinds.WorkBuilding)
            {
                NumberOfJobs = rng.Next(2,100);
                NumberOfResidents = 0;
                RepresentedColor = Color.Orange;
                Height = NumberOfJobs / 2;
            }
            else if (tileKind == TileKinds.PublicTransportHub)
            {
                NumberOfJobs = 0;
                NumberOfResidents = 0;
                Attractivity *= 5;
                RepresentedColor = Color.Blue;
                Height = 2;
            }
            else if (tileKind == TileKinds.Park)
            {
                NumberOfJobs = 0;
                NumberOfResidents = 0;
                Attractivity *= 2;
                RepresentedColor = Color.Lime;
                Height = 1;
            }
            else if (tileKind == TileKinds.BuildableAreas)
            {
                NumberOfJobs = 0;
                NumberOfResidents = 0;
                RepresentedColor = Color.DarkMagenta;
                Height = 5;
            }
            else
            {
                throw new ArgumentException($"Error: Uknown tile kind. Was {tileKind} but was expecting" +
                                            " either ResidentialBuilding, WorkBuilding, PublicTransportHub, Park, BuildableArea");
            }
            

        }
    }

    public class Tile
    { 
        public bool IsClaimable;
        public Plot PlotOnTile;
        public List<FTRegion> FtRegions;
    }
}
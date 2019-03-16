﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace YnABMC
{
    public struct MapColour
    {
        public string Colour { get; set; }
        public MapColour(int red, int green, int blue)
        {
            Colour = "(" + red / 20 + "," + green / 20 + "," + blue / 20 + ")";
        }
    }

    public partial class Form1 : Form
    {
        //string Version = "Dev 0.3.0.8";
        string Version = "Alpha 0.3.1";
        string GameVersions = "1.2,2.0";
        string FolderPath = "", BmpFilePath = "", ProjectName = "", AuthorName = "", ModID = "";
        bool Lua = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Yet (not) Another Bit Map Converter by Zobtzler - " + Version;
        }

#region MapValues
        
#region Terrain
        MapColour Grass = new MapColour(32, 192, 64); //1,9,3
        MapColour Plains = new MapColour(192, 224, 0); //9,11,0
        MapColour Desert = new MapColour(224, 128, 0); //11,6,0
        MapColour Tundra = new MapColour(192, 220, 192); //9,11,9
        MapColour Snow = new MapColour(255, 255, 255); //12,12,12
        MapColour Coast = new MapColour(0, 160, 192); //0,8,9
        MapColour Ocean = new MapColour(64, 64, 192); //3,3,9
        MapColour Lake = new MapColour(0, 96, 128); //0,4,6
#endregion

#region Feratures and Plots
        MapColour Mountain = new MapColour(128, 128, 128); //6,6,6
        MapColour Hills = new MapColour(192, 192, 192); //9,9,9
        MapColour Volcano = new MapColour(255, 0, 0); //12,0,0
        
        MapColour Ice = new MapColour(255, 255, 255); //12,12,12
        MapColour Jungle = new MapColour(224, 192, 0); //11,9,0
        MapColour Marsh = new MapColour(0, 160, 192); //0,8,9
        MapColour Oasis = new MapColour(0, 160, 192); //0,8,9
        MapColour FloodPlains = new MapColour(192, 224, 0); //9,11,0
        MapColour Woods = new MapColour(192, 128, 64); //9,6,3
        MapColour Reef = new MapColour(224, 192, 0); //11,9,0
        MapColour PlainsFloodPlains = new MapColour(32, 192, 64); //1,9,3
        MapColour GeothermalFissure = new MapColour(0, 96, 128); //0,4,6
#endregion

#region Natural Wonders

#region Vanilla
        MapColour BarrierReef = new MapColour(255, 0, 0); //12,0,0
        MapColour CliffsDover = new MapColour(224, 32, 0); //11,1,0
        MapColour CraterLake = new MapColour(192, 32, 0); //9,1,0
        MapColour DeadSea = new MapColour(160, 32, 0); //8,1,0
        MapColour Everest = new MapColour(128, 0, 0); //6,0,0
        MapColour Galapagos = new MapColour(64, 32, 0); //3,1,0
        MapColour Kilimanjaro = new MapColour(0, 0, 0); //0,0,0
        MapColour Pantanal = new MapColour(0, 128, 128); //0,6,6
        MapColour Piopiotahi = new MapColour(0, 255, 0); //0,12,0
        MapColour TorresDelPaine = new MapColour(0, 224, 0); //0,11,0
        MapColour Tsingy = new MapColour(0, 192, 0); //0,9,0
        MapColour Yosemite = new MapColour(0, 160, 0); //0,8,0
#endregion

#region Vikings
        MapColour Eyjafjallajokull = new MapColour(0, 64, 0); //0,3,0
        MapColour GiantsCauseway = new MapColour(0, 0, 255); //0,0,12
        MapColour Lysefjorden = new MapColour(0, 96, 128); //0,4,6
#endregion

#region Australia
        MapColour Uluru = new MapColour(0, 128, 0); //0,6,0
#endregion

#region Khmer and Indonesia
        MapColour HaLongBay = new MapColour(0, 96, 0); //0,4,0
#endregion

#region Rise and Fall
        MapColour DelicateArch = new MapColour(0, 0, 192); //0,0,9
        MapColour EyeOfTheSahara = new MapColour(0, 0, 128); //0,0,6
        MapColour LakeRetba = new MapColour(0, 0, 64); //0,0,3
        MapColour Matterhorn = new MapColour(32, 0, 64); //1,0,3
        MapColour Roraima = new MapColour(64, 64, 64); //3,3,3
        MapColour UbsunurHollow = new MapColour(192, 128, 64); //9,6,3
        MapColour ZhangyeDanxia = new MapColour(128, 128, 128); //6,6,6
#endregion

#region Gathering Storm
        MapColour ChocolateHills = new MapColour(64, 0, 0); //3,0,0
        MapColour DevilsTower = new MapColour(192, 0, 0); //9,0,0
        MapColour Gobustan = new MapColour(64, 64, 0); //3,0,0
        MapColour Ikkil = new MapColour(128, 64, 0); //6,3,0
        MapColour Pamukkale = new MapColour(192, 64, 0); //9,3,0
        MapColour Vesuvius = new MapColour(255, 64, 0); //12,3,0
        MapColour WhiteDesert = new MapColour(64, 96, 0); //3,4,0
#endregion

#region Mods

#endregion

#endregion

#region Rivers
        MapColour River0 = new MapColour(64, 64, 192); //3,3,9
        MapColour River1 = new MapColour(160, 64, 192); //8,3,9
        MapColour Cliffs = new MapColour(192, 128, 64); //9,6,3
#endregion

#region Continents
        MapColour Africa = new MapColour(255, 0, 0); //12,0,0
        MapColour Amasia = new MapColour(224, 32, 0); //11,1,0
        MapColour America = new MapColour(192, 32, 0); //10,1,0
        MapColour Antarctica = new MapColour(160, 32, 0); //8,1,0
        MapColour Arctica = new MapColour(128, 0, 0); //6,0,0
        MapColour Asia = new MapColour(64, 32, 0); //3,1,0
        MapColour Asiamerica = new MapColour(0, 0, 0); //0,0,0
        MapColour Atlantica = new MapColour(0, 128, 128); //0,6,6
        MapColour Atlantis = new MapColour(0, 255, 0); //0,12,0
        MapColour Australia = new MapColour(0, 224, 0); //0,11,0
        MapColour Avalonia = new MapColour(0, 192, 0); //0,9,0
        MapColour Azania = new MapColour(0, 160, 0); //0,8,0
        MapColour Baltica = new MapColour(0, 128, 0); //0,6,0
        MapColour Cimmeria = new MapColour(0, 96, 0); //0,4,0
        MapColour Columbia = new MapColour(0, 64, 0); //0,3,0
        MapColour CongoCraton = new MapColour(0, 96, 128); //0,4,6
        MapColour Euramerica = new MapColour(0, 0, 255); //0,0,12
        MapColour Europe = new MapColour(0, 0, 192); //0,0,9
        MapColour Gondwana = new MapColour(0, 0, 128); //0,0,6
        MapColour Kalaharia = new MapColour(0, 0, 64); //0,0,3
        MapColour Kazakstania = new MapColour(32, 0, 64); //1,0,3
        MapColour Kenorland = new MapColour(64, 64, 64); //3,3,3
        MapColour KumariKandam = new MapColour(192, 128, 64); //9,6,3
        MapColour Laurasia = new MapColour(128, 128, 128); //6,6,6
        MapColour Laurentia = new MapColour(255, 255, 0); //12,12,0
        MapColour Lemuria = new MapColour(224, 224, 0); //11,11,0
        MapColour Mu = new MapColour(192, 192, 0); //9,9,0
        MapColour Nena = new MapColour(160, 160, 0); //8,8,0
        MapColour NorthAmerica = new MapColour(128, 128, 0); //6,6,0
        MapColour Novapangaea = new MapColour(96, 96, 0); //4,4,0
        MapColour Nuna = new MapColour(64, 64, 0); //3,3,0
        MapColour Oceania = new MapColour(0, 64, 64); //0,3,3
        MapColour Pangaea = new MapColour(255, 0, 255); //12,0,12
        MapColour PangaeaUltima = new MapColour(224, 0, 192); //11,0,9
        MapColour Pannotia = new MapColour(192, 0, 192); //9,0,9
        MapColour Rodinia = new MapColour(160, 0, 192); //8,0,9
        MapColour Siberia = new MapColour(128, 0, 128); //6,0,6
        MapColour SouthAmerica = new MapColour(96, 0, 128); //4,0,6
        MapColour TerraAustralis = new MapColour(64, 0, 64); //3,0,3
        MapColour Ur = new MapColour(0, 32, 64); //0,1,3
        MapColour Vaalbara = new MapColour(0, 255, 255); //0,12,12
        MapColour Vendian = new MapColour(0, 192, 192); //0,9,9
#endregion

#region Resources
        MapColour Bananas = new MapColour(255, 0, 0); //12,0,0
        MapColour Cattle = new MapColour(224, 32, 0); //11,1,0
        MapColour Copper = new MapColour(192, 32, 0); //10,1,0
        MapColour Crabs = new MapColour(160, 32, 0); //8,1,0
        MapColour Deer = new MapColour(128, 0, 0); //6,0,0
        MapColour Fish = new MapColour(64, 32, 0); //3,1,0
        MapColour Rice = new MapColour(0, 0, 0); //0,0,0
        MapColour Sheep = new MapColour(0, 128, 128); //0,6,6
        MapColour Stone = new MapColour(0, 255, 0); //0,12,0
        MapColour Wheat = new MapColour(0, 224, 0); //0,11,0
        MapColour Citrus = new MapColour(0, 192, 0); //0,9,0
        MapColour Cocoa = new MapColour(0, 160, 0); //0,8,0
        MapColour Coffee = new MapColour(0, 128, 0); //0,6,0
        MapColour Cotton = new MapColour(0, 96, 0); //0,4,0
        MapColour Diamonds = new MapColour(0, 64, 0); //0,3,0
        MapColour Dyes = new MapColour(0, 96, 128); //0,4,6
        MapColour Furs = new MapColour(0, 0, 255); //0,0,12
        MapColour Gypsum = new MapColour(0, 0, 192); //0,0,9
        MapColour Incense = new MapColour(0, 0, 128); //0,0,6
        MapColour Ivory = new MapColour(0, 0, 64); //0,0,3
        MapColour Jade = new MapColour(32, 0, 64); //1,0,3
        MapColour Marble = new MapColour(64, 64, 64); //3,3,3
        MapColour Mercury = new MapColour(192, 128, 64); //9,6,3
        MapColour Pearls = new MapColour(128, 128, 128); //6,6,6
        MapColour Salt = new MapColour(255, 255, 0); //12,12,0
        MapColour Silk = new MapColour(224, 224, 0); //11,11,0
        MapColour Silver = new MapColour(192, 192, 0); //9,9,0
        MapColour Spices = new MapColour(160, 160, 0); //8,8,0
        MapColour Sugar = new MapColour(128, 128, 0); //6,6,0
        MapColour Tea = new MapColour(96, 96, 0); //4,4,0
        MapColour Tobacco = new MapColour(64, 64, 0); //3,3,0
        MapColour Truffles = new MapColour(0, 64, 64); //0,3,3
        MapColour Whales = new MapColour(255, 0, 255); //12,0,12
        MapColour Wine = new MapColour(224, 0, 192); //11,0,9
        MapColour Aluminium = new MapColour(192, 0, 192); //9,0,9
        MapColour Coal = new MapColour(160, 0, 192); //8,0,9
        MapColour Horses = new MapColour(128, 0, 128); //6,0,6
        MapColour Iron = new MapColour(96, 0, 128); //4,0,6
        MapColour Niter = new MapColour(64, 0, 64); //3,0,3
        MapColour Oil = new MapColour(0, 32, 64); //0,1,3
        MapColour Uranium = new MapColour(0, 255, 255); //0,12,12
        MapColour AntiquitySite = new MapColour(0, 192, 192); //0,9,9
        MapColour Shipwreck = new MapColour(192, 192, 192); //9,9,9
        MapColour Amber = new MapColour(255, 251, 224); //12,12,11
        MapColour Olives = new MapColour(224, 224, 224); //11,11,11
        MapColour Turtles = new MapColour(160, 160, 160); //8,8,8
#endregion

#endregion

#region Files

#region Lua
        string  LuaFile = "",
                LuaStart = "include \"MapEnums\"\ninclude \"MapUtilities\"\ninclude \"MountainsCliffs\"\ninclude \"RiversLakes\"\n" +
                                "include \"FeatureGenerator\"\ninclude \"TerrainGenerator\"\ninclude \"NaturalWonderGenerator\"\n" +
                                "include \"ResourceGenerator\"\ninclude \"AssignStartingPlots\"\n\nlocal g_iW = ",
                LuaEndStart = "\nlocal g_iFlags = {}\nlocal g_continentsFrac = nil\n\n" +
                                "function GetMapInitData(worldSize)\n\treturn {\n\t\tWidth = g_iW,\n\t\tHeight = g_iH,\n\t\tWrapX = ",
                LuaEndWrap = ",\n\t\tWrapY = false,\n\t};\nend\n\nfunction GetNaturalWonders()\n\tlocal NaturalWonders = {}\n",
                LuaGenMap = "\n\treturn NaturalWonders\nend\n\nfunction GenerateMap()\n\tprint(\"Calling Map Generator\");\n\t" +
                                "GenerateImportedMap(GetMap(), GetCiv6DataToConvert(), GetNaturalWonders(), g_iW, g_iH);\nend\n\n" +
                                "function GetCiv6DataToConvert()\n\treturn {}\nend\n\nfunction GetMap()\n\tlocal MapToConvert = {}\n\t" +
                                "for i = 0, g_iW - 1, 1 do\n\t\tMapToConvert[i] = {}\n\tend\n\n",
                LuaEndMap = "\n\treturn MapToConvert\nend\n\n";
#endregion

#region Database
        string  Config = "",
                ConfigMap = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameInfo>\n\t<Maps>\n",
                ConfigParameters = "\t</Maps>\n\t<Parameters>\n",
                ConfigMapSupport = "\t</Parameters>\n\t<MapSupportedValues>\n",
                ConfigEnd = "\t</MapSupportedValues>\n</GameInfo>";
        string  NatWond = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<NaturalWonderPosition>",
                NatWondEnd = "\n\t</NaturalWonderPosition>\n</GameData>",
                NatWondTemp = "";
        string  ExtraPlacement = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<ExtraPlacement>",
                VolcanoString = "",
                ExtraPlacementEnd = "\n\t</ExtraPlacement>\n</GameData>";

#endregion

#endregion

#region Everything Else
        bool  PathFound = false;

        public Form1()
        {
            InitializeComponent();
            Size = new Size(600, 493);
            AdvancedOptions.Visible = true;
        }

        private void AdvancedOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (!AdvancedOptions.Checked)
            {
                SelectLua.Enabled = false;
                SelectLua.Visible = false;
                Size = new Size(600, 493);
            }
            else
            {
                SelectLua.Enabled = true;
                SelectLua.Visible = true;
                Size = new Size(764, 493);
            }
        }

        private void SelectSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFDSource = new OpenFileDialog
            {
                Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*",
                FilterIndex = 1
            };
            if (OFDSource.ShowDialog() == DialogResult.OK)
            {
                FolderPath = Path.GetDirectoryName(OFDSource.FileName);
                BmpFilePath = OFDSource.FileName;
                PathFound = true;
                Lua = false;
            }
        }

        private void SelectLua_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFDLua = new OpenFileDialog
            {
                Filter = "LOG Files (*.log)|*.log|All files(*.*)|*.* ",
                FilterIndex = 1
            };
            if (OFDLua.ShowDialog() == DialogResult.OK)
            {
                FolderPath = Path.GetDirectoryName(OFDLua.FileName);
                BmpFilePath = OFDLua.FileName;
                PathFound = true;
                Lua = true;
            }
        }

        private void ProjectText_TextChanged(object sender, EventArgs e)
        {
            AuthorName = new string(AuthorText.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            ProjectName = new string(ProjectText.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            ProjectName = AuthorName + "_" + ProjectName;
        }

        private void AuthorText_TextChanged(object sender, EventArgs e)
        {
            AuthorName = new string(AuthorText.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            ProjectName = new string(ProjectText.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            ProjectName = AuthorName + "_" + ProjectName;
        }

        private void ModIDValue_TextChanged(object sender, EventArgs e)
        {
            ModID = ModIDValue.Text;
        }

        private void ModIDRandom_Click(object sender, EventArgs e)
        {
            var chars = "abcdef0123456789";
            var random = new Random();
            ModID = "";
            for (int i = 0; i < 32; i++)
            {
                ModID = ModID + chars[random.Next(chars.Length)];
                if (i == 7 || i == 11 || i == 15 || i == 19) ModID = ModID + "-";
            }
            ModIDValue.Text = ModID;
        }
#endregion

#region Checkboxes
        private void RNFRules_CheckedChanged(object sender, EventArgs e)
        {
            if (!RNFRules.Checked && !GSRules.Checked)
            {
                STDRules.Checked = true;
                STDRules.Enabled = false;
            }
            else STDRules.Enabled = true;
        }

        private void GSRules_CheckedChanged(object sender, EventArgs e)
        {
            if (!RNFRules.Checked && !GSRules.Checked)
            {
                STDRules.Checked = true;
                STDRules.Enabled = false;
            }
            else STDRules.Enabled = true;
        }
        private void RiversEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (!RiversImport.Checked && !RiversEmpty.Checked)
            {
                RiversGenerate.Checked = true;
                RiversGenerate.Enabled = false;
            }
            else RiversGenerate.Enabled = true;
        }

        private void RiversImport_CheckedChanged(object sender, EventArgs e)
        {
            if (!RiversImport.Checked && !RiversEmpty.Checked)
            {
                RiversGenerate.Checked = true;
                RiversGenerate.Enabled = false;
            }
            else RiversGenerate.Enabled = true;
        }

        private void ContinentsImport_CheckedChanged(object sender, EventArgs e)
        {
            if (!ContinentsImport.Checked)
            {
                ContinentsGenerate.Checked = true;
                ContinentsGenerate.Enabled = false;
            }
            else ContinentsGenerate.Enabled = true;
        }

        private void WondersImport_CheckedChanged(object sender, EventArgs e)
        {
            if (!WondersImport.Checked && !WondersEmpty.Checked)
            {
                WondersGenerate.Checked = true;
                WondersGenerate.Enabled = false;
            }
            else WondersGenerate.Enabled = true;
        }

        private void WondersEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (!WondersImport.Checked && !WondersEmpty.Checked)
            {
                WondersGenerate.Checked = true;
                WondersGenerate.Enabled = false;
            }
            else WondersGenerate.Enabled = true;
        }

        private void FeaturesEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (!FeaturesImport.Checked && !FeaturesEmpty.Checked)
            {
                FeaturesGenerate.Checked = true;
                FeaturesGenerate.Enabled = false;
            }
            else FeaturesGenerate.Enabled = true;
        }

        private void FeaturesImport_CheckedChanged(object sender, EventArgs e)
        {
            if (!FeaturesImport.Checked && !FeaturesEmpty.Checked)
            {
                FeaturesGenerate.Checked = true;
                FeaturesGenerate.Enabled = false;
            }
            else FeaturesGenerate.Enabled = true;
        }

        private void ResourcesImport_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResourcesImport.Checked && !ResourcesEmpty.Checked)
            {
                ResourcesGenerate.Checked = true;
                ResourcesGenerate.Enabled = false;
            }
            else ResourcesGenerate.Enabled = true;
        }

        private void ResourcesEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (!ResourcesImport.Checked && !ResourcesEmpty.Checked)
            {
                ResourcesGenerate.Checked = true;
                ResourcesGenerate.Enabled = false;
            }
            else ResourcesGenerate.Enabled = true;
        }
#endregion

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private void GenerateMap_Click(object sender, EventArgs e)
        {

            if (PathFound && ProjectText.Text.Length > 0 && AuthorText.Text.Length > 0)
            {
#region Timer
                timer.Interval = 5000;
                timer.Tick += Timer_Tick;
                timer.Start();
                GenerateMap.Enabled = false;
                int MapH = 0, MapW = 0, Center = 3, OffsetX = 6, OffsetY = 5;
#endregion

#region LUA

#region BMP Generated
                if (!Lua)
                {
                    Bitmap Map = new Bitmap(BmpFilePath);
                    MapH = (Map.Height - 3) / 5;
                    MapW = (Map.Width - 4) / 6;
                    bool[,] WaterArray = new bool[MapW, MapH];
                    bool[,] HasHills = new bool[MapW, MapH];
                    for (int i = 0; i < MapW; i++)
                    {
                        for (int j = 0; j < MapH; j++)
                        {
                            WaterArray[i,j] = false;
                            HasHills[i, j] = false;
                        }
                    }

                    for (int y = 0; y < MapH; y++)
                    {
                        string EndCliffs = "", InvertedLines = "", CurrentPlot = "";
                        int MapY = Map.Height - ((y * OffsetY) + Center) - 1;
                        for (int x = MapW - 1; x > -1; x--)
                        {
#region Variables etc
                            int MapX = x * OffsetX + Center + (y % 2) * Center;
                            string CurrentLine = "MapToConvert[" + x + "][" + y + "]={";
                            Color MapTerrain = Map.GetPixel(MapX + 2, MapY + 1);
                            Color MapHills = Map.GetPixel(MapX + 2, MapY);
                            Color MapPlotFeature = Map.GetPixel(MapX, MapY);
                            Color MapRiverE = Map.GetPixel(MapX + 3, MapY);
                            Color MapRiverSE = Map.GetPixel(MapX + 2, MapY + 2);
                            Color MapRiverSW = Map.GetPixel(MapX - 2, MapY + 2);
                            Color MapResource = Map.GetPixel(MapX - 2, MapY - 2);
                            Color MapContinent = Map.GetPixel(MapX - 2, MapY + 1);
                            Color MapWonder = Map.GetPixel(MapX - 2, MapY - 1);
                            string MatchTerrain = "(" + MapTerrain.R / 20 + "," + MapTerrain.G / 20 + "," + MapTerrain.B / 20 + ")";
                            string MatchHills = "(" + MapHills.R / 20 + "," + MapHills.G / 20 + "," + MapHills.B / 20 + ")";
                            string MatchPlotFeature = "(" + MapPlotFeature.R / 20 + "," + MapPlotFeature.G / 20 + "," + MapPlotFeature.B / 20 + ")";
                            string MatchRiverE = "(" + MapRiverE.R / 20 + "," + MapRiverE.G / 20 + "," + MapRiverE.B / 20 + ")";
                            string MatchRiverSE = "(" + MapRiverSE.R / 20 + "," + MapRiverSE.G / 20 + "," + MapRiverSE.B / 20 + ")";
                            string MatchRiverSW = "(" + MapRiverSW.R / 20 + "," + MapRiverSW.G / 20 + "," + MapRiverSW.B / 20 + ")";
                            string MatchResource = "(" + MapResource.R / 20 + "," + MapResource.G / 20 + "," + MapResource.B / 20 + ")";
                            string MatchContinent = "(" + MapContinent.R / 20 + "," + MapContinent.G / 20 + "," + MapContinent.B / 20 + ")";
                            string MatchWonder = "(" + MapWonder.R / 20 + "," + MapWonder.G / 20 + "," + MapWonder.B / 20 + ")";
#endregion

#region Terrain
                            if (MatchTerrain == Grass.Colour) CurrentPlot = "TERRAIN_GRASS";
                            else if (MatchTerrain == Plains.Colour) CurrentPlot = "TERRAIN_PLAINS";
                            else if (MatchTerrain == Desert.Colour) CurrentPlot = "TERRAIN_DESERT";
                            else if (MatchTerrain == Tundra.Colour) CurrentPlot = "TERRAIN_TUNDRA";
                            else if (MatchTerrain == Snow.Colour) CurrentPlot = "TERRAIN_SNOW";
                            else if (MatchTerrain == Lake.Colour)
                            {
                                CurrentPlot = "TERRAIN_COAST";
                                WaterArray[x, y] = false; //This is only used for cliffs, and cliffs do not generate in lakes (this is actually false, but I'm not gonna update this yet)
                            }
                            else if (MatchTerrain == Coast.Colour)
                            {
                                CurrentPlot = "TERRAIN_COAST";
                                WaterArray[x, y] = true;
                            }
                            else
                            {
                                CurrentPlot = "TERRAIN_OCEAN";
                                WaterArray[x, y] = true;
                            }
                            if ((MatchPlotFeature == Mountain.Colour || MatchPlotFeature == Volcano.Colour) && (MatchTerrain != Lake.Colour || MatchTerrain != Coast.Colour || MatchTerrain != Ocean.Colour)) CurrentPlot += "_MOUNTAIN";
                            else if (MatchHills == Hills.Colour && (MatchTerrain != Lake.Colour || MatchTerrain != Coast.Colour || MatchTerrain != Ocean.Colour))
                            {
                                CurrentPlot += "_HILLS";
                                HasHills[x, y] = true;
                            }
                            CurrentLine += "\"" + CurrentPlot + "\",";
#endregion

#region Natural Wonders
                            if (MatchWonder == BarrierReef.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_BARRIER_REEF\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == CliffsDover.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_CLIFFS_DOVER\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == CraterLake.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_CRATER_LAKE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == DeadSea.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_DEAD_SEA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Everest.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_EVEREST\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Galapagos.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_GALAPAGOS\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Kilimanjaro.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_KILIMANJARO\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Pantanal.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_PANTANAL\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Piopiotahi.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_PIOPIOTAHI\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == TorresDelPaine.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_TORRES_DEL_PAINE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Tsingy.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_TSINGY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Yosemite.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_YOSEMITE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Uluru.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_ULURU\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == HaLongBay.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_HA_LONG_BAY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Eyjafjallajokull.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_EYJAFJALLAJOKULL\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Lysefjorden.Colour && MatchTerrain != Lake.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_LYSEFJORDEN\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == GiantsCauseway.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_GIANTS_CAUSEWAY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == DelicateArch.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_DELICATE_ARCH\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == EyeOfTheSahara.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_EYE_OF_THE_SAHARA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == LakeRetba.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_LAKE_RETBA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Matterhorn.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_MATTERHORN\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Roraima.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_RORAIMA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == UbsunurHollow.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_UBSUNUR_HOLLOW\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == ZhangyeDanxia.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_ZHANGYE_DANXIA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == ChocolateHills.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_CHOCOLATEHILLS\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == DevilsTower.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_DEVILSTOWER\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Gobustan.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_GOBUSTAN\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Ikkil.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_IKKIL\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Pamukkale.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_PAMUKKALE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == Vesuvius.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_VESUVIUS\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            else if (MatchWonder == WhiteDesert.Colour) NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" " +
                                    "FeatureType = \"FEATURE_WHITEDESERT\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                            //Repeat for future NWs
#endregion

#region Features
                            if (MatchPlotFeature == FloodPlains.Colour && (MatchTerrain == Desert.Colour || MatchTerrain == Grass.Colour))
                            {
                                if (MatchTerrain == Desert.Colour) CurrentLine += "\"FEATURE_FLOODPLAINS\",";
                                else if (MatchTerrain == Grass.Colour) CurrentLine += "\"FEATURE_FLOODPLAINS_GRASSLAND\",";
                            }
                            else if (MatchPlotFeature == PlainsFloodPlains.Colour && MatchTerrain == Plains.Colour) CurrentLine = "\"FEATURE_FLOODPLAINS_PLAINS\",";
                            else if (MatchPlotFeature == Ice.Colour && (MatchTerrain == Ocean.Colour || MatchTerrain == Coast.Colour)) CurrentLine += "\"FEATURE_ICE\",";
                            else if (MatchPlotFeature == Jungle.Colour && MatchTerrain != Coast.Colour) CurrentLine += "\"FEATURE_JUNGLE\",";
                            else if (MatchPlotFeature == Woods.Colour) CurrentLine += "\"FEATURE_FOREST\",";
                            else if (MatchPlotFeature == GeothermalFissure.Colour && MatchTerrain != Lake.Colour) CurrentLine += "\"FEATURE_GEOTHERMAL_FISSURE\",";
                            else if (MatchPlotFeature == Oasis.Colour && MatchTerrain == Desert.Colour) CurrentLine += "\"FEATURE_OASIS\",";
                            else if (MatchPlotFeature == Marsh.Colour && MatchTerrain == Grass.Colour) CurrentLine += "\"FEATURE_MARSH\",";
                            else if (MatchPlotFeature == Reef.Colour && MatchTerrain == Coast.Colour) CurrentLine += "\"FEATURE_REEF\",";
                            else if (MatchPlotFeature == Volcano.Colour)
                            {
                                CurrentLine += "-1,";
                                VolcanoString += "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X=\"" + x + "\" Y=\"" + y + "\" RuleSet=\"RULESET_EXPANSION_2\" TerrainType=\"" + CurrentPlot + "\" FeatureType=\"FEATURE_VOLCANO\" />";
                            }
                            else CurrentLine += "-1,";

#endregion

#region Continent
                            if (MatchTerrain != Ocean.Colour && MatchTerrain != Coast.Colour && MatchTerrain != Lake.Colour)
                            {
                                if (MatchContinent == Africa.Colour) CurrentLine += "\"CONTINENT_AFRICA\",{{";
                                else if (MatchContinent == Amasia.Colour) CurrentLine += "\"CONTINENT_AMASIA\",{{";
                                else if (MatchContinent == America.Colour) CurrentLine += "\"CONTINENT_AMERICA\",{{";
                                else if (MatchContinent == Antarctica.Colour) CurrentLine += "\"CONTINENT_ANTARCTICA\",{{";
                                else if (MatchContinent == Arctica.Colour) CurrentLine += "\"CONTINENT_ARCTICA\",{{";
                                else if (MatchContinent == Asia.Colour) CurrentLine += "\"CONTINENT_ASIA\",{{";
                                else if (MatchContinent == Asiamerica.Colour) CurrentLine += "\"CONTINENT_ASIAMERICA\",{{";
                                else if (MatchContinent == Atlantica.Colour) CurrentLine += "\"CONTINENT_ATLANTICA\",{{";
                                else if (MatchContinent == Atlantis.Colour) CurrentLine += "\"CONTINENT_ATLANTIS\",{{";
                                else if (MatchContinent == Australia.Colour) CurrentLine += "\"CONTINENT_AUSTRALIA\",{{";
                                else if (MatchContinent == Avalonia.Colour) CurrentLine += "\"CONTINENT_AVALONIA\",{{";
                                else if (MatchContinent == Azania.Colour) CurrentLine += "\"CONTINENT_AZANIA\",{{";
                                else if (MatchContinent == Baltica.Colour) CurrentLine += "\"CONTINENT_BALTICA\",{{";
                                else if (MatchContinent == Cimmeria.Colour) CurrentLine += "\"CONTINENT_CIMMERIA\",{{";
                                else if (MatchContinent == Columbia.Colour) CurrentLine += "\"CONTINENT_COLUMBIA\",{{";
                                else if (MatchContinent == CongoCraton.Colour) CurrentLine += "\"CONTINENT_CONGO_CRATON\",{{";
                                else if (MatchContinent == Euramerica.Colour) CurrentLine += "\"CONTINENT_EURAMERICA\",{{";
                                else if (MatchContinent == Europe.Colour) CurrentLine += "\"CONTINENT_EUROPE\",{{";
                                else if (MatchContinent == Gondwana.Colour) CurrentLine += "\"CONTINENT_GONDWANA\",{{";
                                else if (MatchContinent == Kalaharia.Colour) CurrentLine += "\"CONTINENT_KALAHARIA\",{{";
                                else if (MatchContinent == Kazakstania.Colour) CurrentLine += "\"CONTINENT_KAZAKHSTANIA\",{{";
                                else if (MatchContinent == Kenorland.Colour) CurrentLine += "\"CONTINENT_KERNORLAND\",{{";
                                else if (MatchContinent == KumariKandam.Colour) CurrentLine += "\"CONTINENT_KUMARI_KANDAM\",{{";
                                else if (MatchContinent == Laurasia.Colour) CurrentLine += "\"CONTINENT_LAURASIA\",{{";
                                else if (MatchContinent == Laurentia.Colour) CurrentLine += "\"CONTINENT_LAURENTIA\",{{";
                                else if (MatchContinent == Lemuria.Colour) CurrentLine += "\"CONTINENT_LEMURIA\",{{";
                                else if (MatchContinent == Mu.Colour) CurrentLine += "\"CONTINENT_MU\",{{";
                                else if (MatchContinent == Nena.Colour) CurrentLine += "\"CONTINENT_NENA\",{{";
                                else if (MatchContinent == NorthAmerica.Colour) CurrentLine += "\"CONTINENT_NORTH_AMERICA\",{{";
                                else if (MatchContinent == Novapangaea.Colour) CurrentLine += "\"CONTINENT_NOVOPANGAEA\",{{";
                                else if (MatchContinent == Nuna.Colour) CurrentLine += "\"CONTINENT_NUNA\",{{";
                                else if (MatchContinent == Oceania.Colour) CurrentLine += "\"CONTINENT_OCEANIA\",{{";
                                else if (MatchContinent == Pangaea.Colour) CurrentLine += "\"CONTINENT_PANGAEA\",{{";
                                else if (MatchContinent == PangaeaUltima.Colour) CurrentLine += "\"CONTINENT_PANGAEA_ULTIMA\",{{";
                                else if (MatchContinent == Pannotia.Colour) CurrentLine += "\"CONTINENT_PANNOTIA\",{{";
                                else if (MatchContinent == Rodinia.Colour) CurrentLine += "\"CONTINENT_RODINIA\",{{";
                                else if (MatchContinent == Siberia.Colour) CurrentLine += "\"CONTINENT_SIBERIA\",{{";
                                else if (MatchContinent == SouthAmerica.Colour) CurrentLine += "\"CONTINENT_SOUTH_AMERICA\",{{";
                                else if (MatchContinent == TerraAustralis.Colour) CurrentLine += "\"CONTINENT_TERRA_AUSTRALIS\",{{";
                                else if (MatchContinent == Ur.Colour) CurrentLine += "\"CONTINENT_UR\",{{";
                                else if (MatchContinent == Vaalbara.Colour) CurrentLine += "\"CONTINENT_VAALBARA\",{{";
                                else if (MatchContinent == Vendian.Colour) CurrentLine += "\"CONTINENT_VENDIAN\",{{";
                                else CurrentLine += "\"CONTINENT_ZEALANDIA\",{{";
                            }
                            else CurrentLine += "-1,{{";
#endregion

#region Rivers
                            if (MatchRiverSW == River0.Colour) CurrentLine += "1,2},{";
                            else if (MatchRiverSW == River1.Colour) CurrentLine += "1,5},{";
                            else CurrentLine += "0,-1},{";
                            if (MatchRiverE == River0.Colour) CurrentLine += "1,0},{";
                            else if (MatchRiverE == River1.Colour) CurrentLine += "1,3},{";
                            else CurrentLine += "0,-1},{";
                            if (MatchRiverSE == River0.Colour) CurrentLine += "1,1}},{";
                            else if (MatchRiverSE == River1.Colour) CurrentLine += "1,4}},{";
                            else CurrentLine += "0,-1}},{";
#endregion

#region Resources
                            //Bonus
                            if (MatchResource == Bananas.Colour) CurrentLine += "\"RESOURCE_BANANAS\",1},{";
                            else if (MatchResource == Cattle.Colour) CurrentLine += "\"RESOURCE_CATTLE\",1},{";
                            else if (MatchResource == Copper.Colour) CurrentLine += "\"RESOURCE_COPPER\",1},{";
                            else if (MatchResource == Crabs.Colour) CurrentLine += "\"RESOURCE_CRABS\",1},{";
                            else if (MatchResource == Deer.Colour) CurrentLine += "\"RESOURCE_DEER\",1},{";
                            else if (MatchResource == Fish.Colour) CurrentLine += "\"RESOURCE_FISH\",1},{";
                            else if (MatchResource == Rice.Colour) CurrentLine += "\"RESOURCE_RICE\",1},{";
                            else if (MatchResource == Sheep.Colour) CurrentLine += "\"RESOURCE_SHEEP\",1},{";
                            else if (MatchResource == Stone.Colour) CurrentLine += "\"RESOURCE_STONE\",1},{";
                            else if (MatchResource == Wheat.Colour) CurrentLine += "\"RESOURCE_WHEAT\",1},{";
                            else if (MatchResource == Citrus.Colour)  CurrentLine += "\"RESOURCE_CITRUS\",1},{";
                            else if (MatchResource == Cocoa.Colour) CurrentLine += "\"RESOURCE_COCOA\",1},{";
                            else if (MatchResource == Coffee.Colour) CurrentLine += "\"RESOURCE_COFFEE\",1},{";
                            else if (MatchResource == Cotton.Colour) CurrentLine += "\"RESOURCE_COTTON\",1},{";
                            else if (MatchResource == Diamonds.Colour) CurrentLine += "\"RESOURCE_DIAMONDS\",1},{";
                            else if (MatchResource == Dyes.Colour && MatchTerrain != Lake.Colour) CurrentLine += "\"RESOURCE_DYES\",1},{";
                            else if (MatchResource == Furs.Colour) CurrentLine += "\"RESOURCE_FURS\",1},{";
                            else if (MatchResource == Gypsum.Colour) CurrentLine += "\"RESOURCE_GYPSUM\",1},{";
                            else if (MatchResource == Incense.Colour) CurrentLine += "\"RESOURCE_INCENSE\",1},{";
                            else if (MatchResource == Ivory.Colour) CurrentLine += "\"RESOURCE_IVORY\",1},{";
                            else if (MatchResource == Jade.Colour) CurrentLine += "\"RESOURCE_JADE\",1},{";
                            else if (MatchResource == Marble.Colour) CurrentLine += "\"RESOURCE_MARBLE\",1},{";
                            else if (MatchResource == Mercury.Colour) CurrentLine += "\"RESOURCE_MERCURY\",1},{";
                            else if (MatchResource == Pearls.Colour) CurrentLine += "\"RESOURCE_PEARLS\",1},{";
                            else if (MatchResource == Salt.Colour) CurrentLine += "\"RESOURCE_SALT\",1},{";
                            else if (MatchResource == Silk.Colour) CurrentLine += "\"RESOURCE_SILK\",1},{";
                            else if (MatchResource == Silver.Colour) CurrentLine += "\"RESOURCE_SILVER\",1},{";
                            else if (MatchResource == Spices.Colour) CurrentLine += "\"RESOURCE_SPICES\",1},{";
                            else if (MatchResource == Sugar.Colour) CurrentLine += "\"RESOURCE_SUGAR\",1},{";
                            else if (MatchResource == Tea.Colour) CurrentLine += "\"RESOURCE_TEA\",1},{";
                            else if (MatchResource == Tobacco.Colour) CurrentLine += "\"RESOURCE_TOBACCO\",1},{";
                            else if (MatchResource == Truffles.Colour) CurrentLine += "\"RESOURCE_TRUFFLES\",1},{";
                            else if (MatchResource == Whales.Colour) CurrentLine += "\"RESOURCE_WHALES\",1},{";
                            else if (MatchResource == Wine.Colour) CurrentLine += "\"RESOURCE_WINE\",1},{";
                            else if (MatchResource == Aluminium.Colour) CurrentLine += "\"RESOURCE_ALUMINUM\",1},{";
                            else if (MatchResource == Coal.Colour) CurrentLine += "\"RESOURCE_COAL\",1},{";
                            else if (MatchResource == Horses.Colour) CurrentLine += "\"RESOURCE_HORSES\",1},{";
                            else if (MatchResource == Iron.Colour) CurrentLine += "\"RESOURCE_IRON\",1},{";
                            else if (MatchResource == Niter.Colour) CurrentLine += "\"RESOURCE_NITER\",1},{";
                            else if (MatchResource == Oil.Colour) CurrentLine += "\"RESOURCE_OIL\",1},{";
                            else if (MatchResource == Uranium.Colour) CurrentLine += "\"RESOURCE_URANIUM\",1},{";
                            else if (MatchResource == AntiquitySite.Colour) CurrentLine += "\"RESOURCE_ANTIQUITY_SITE\",1},{";
                            else if (MatchResource == Shipwreck.Colour) CurrentLine += "\"RESOURCE_SHIPWRECK\",1},{";
                            else if (MatchResource == Amber.Colour) CurrentLine += "\"RESOURCE_AMBER\",1},{";
                            else if (MatchResource == Olives.Colour) CurrentLine += "\"RESOURCE_OLIVES\",1},{";
                            else if (MatchResource == Turtles.Colour) CurrentLine += "\"RESOURCE_TURTLES\",1},{";
                            else CurrentLine += "-1,1},{";
#endregion

#region Cliffs
                            if (CliffsImport.Checked)
                            {
                                if (MatchRiverSW == Cliffs.Colour) CurrentLine += "1,";
                                else CurrentLine += "0,";
                                if (MatchRiverE == Cliffs.Colour) CurrentLine += "1,";
                                else CurrentLine += "0,";
                                if (MatchRiverSE == Cliffs.Colour) CurrentLine += "1}}";
                                else CurrentLine += "0}}";
                            }
                            else
                            {
                                if (y % 2 == 1)
                                {
                                    if (x < MapW - 1)
                                    {
                                        if (y > 0) CurrentLine += Cliff_Generator(WaterArray[x, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[x, y - 1]) + "," +
                                                Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + "," +
                                                Cliff_Generator(WaterArray[x + 1, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y - 1]) + "}}";
                                        else CurrentLine += "0," + Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + ",0}}";
                                    }
                                    if (x == 0)
                                    {
                                        if (y > 0) EndCliffs += Cliff_Generator(WaterArray[MapW - 1, y - 1], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[MapW - 1, y - 1]) + "," +
                                                Cliff_Generator(WaterArray[0, y], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[0, y]) + "," +
                                                Cliff_Generator(WaterArray[0, y - 1], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[0, y - 1]) + "}}";
                                        else EndCliffs += "0," + Cliff_Generator(WaterArray[0, y], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[0, y]) + ",0}}";
                                    }
                                }
                                else
                                {
                                    if (x < MapW - 1 && x > 0)
                                    {
                                        if (y > 0) CurrentLine += Cliff_Generator(WaterArray[x - 1, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[x - 1, y - 1]) + "," +
                                                Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + "," +
                                                Cliff_Generator(WaterArray[x, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[x, y - 1]) + "}}";
                                        else CurrentLine += "0," + Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + ",0}}";
                                    }
                                    if (x == 0)
                                    {
                                        if (y > 0)
                                        {
                                            CurrentLine += Cliff_Generator(WaterArray[MapW - 1, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[MapW - 1, y - 1]) + "," +
                                                Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + "," +
                                                Cliff_Generator(WaterArray[x, y - 1], HasHills[x, y], WaterArray[x, y], HasHills[x, y - 1]) + "}}";
                                            EndCliffs += Cliff_Generator(WaterArray[MapW - 2, y - 1], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[MapW - 2, y - 1]) + "," +
                                                Cliff_Generator(WaterArray[0, y], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[0, y]) + "," +
                                                Cliff_Generator(WaterArray[MapW - 1, y - 1], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[MapW - 1, y - 1]) + "}}";
                                        }
                                        else
                                        {
                                            CurrentLine += "0," + Cliff_Generator(WaterArray[x + 1, y], HasHills[x, y], WaterArray[x, y], HasHills[x + 1, y]) + ",0}}";
                                            EndCliffs += "0," + Cliff_Generator(WaterArray[0, y], HasHills[MapW - 1, y], WaterArray[MapW - 1, y], HasHills[0, y]) + ",0}}";
                                        }
                                    }
                                }
                            }
#endregion
                            if (x == 0 && !CliffsImport.Checked) InvertedLines = CurrentLine + InvertedLines + EndCliffs + "\n";
                            else InvertedLines = "\n" + CurrentLine + InvertedLines;
                        }
                        NatWond += NatWondTemp;
                        NatWondTemp = "";
                        ExtraPlacement += VolcanoString;
                        VolcanoString = "";
                        LuaGenMap += InvertedLines;
                    }
                }
#endregion

#region LUA Generated
                else
                {
                    string LuaLine = "";
                    System.IO.StreamReader file = new System.IO.StreamReader(BmpFilePath);
                    bool Civ5 = false, Civ6 = false;
                    bool[,] WaterArray = new bool[256, 256];
                    bool[,] HasHills = new bool[256, 256];
                    for (int i = 0; i < MapW; i++)
                    {
                        for (int j = 0; j < MapH; j++)
                        {
                            WaterArray[i, j] = false;
                            HasHills[i, j] = false;
                        }
                    }
                    List<string> Civ5Lines = new List<string>();
                    List<string> Civ5LinesY = new List<string>();
                    bool FirstLine = true,  LastColumn = true;
                    string LuaTemp = "", LuaCliffsEnd = "";
                    while ((LuaLine = file.ReadLine()) != null)
                    {
                        string[] Parts = LuaLine.Split(' ');
                        const string ConvertCode = @"MapToConvert.+", YnAMP = @"YnAMP_InGame:", WBP = @"WorldBuilderPlacement:", InGame = @"\[\d+\.\d+\] InGame:";
                        Match ynamp = Regex.Match(LuaLine, YnAMP);
                        Match wbp = Regex.Match(LuaLine, WBP);
                        Match ingame = Regex.Match(LuaLine, InGame);
                        Match convertcode = Regex.Match(LuaLine, ConvertCode);
                        try
                        {
#region civ 6
                            if (convertcode.Success)
                            {
                                if (!Civ5 && (ynamp.Success || wbp.Success))
                                {
                                    Civ6 = true;
                                    string MapArray = convertcode.Groups[0].Value;
                                    int Position = 0;
                                    int CoordX = 0, CoordY = 0;
                                    bool ReachedNum = false;
                                    foreach (char c in MapArray)
                                    {
                                        int foo = c - '0';
                                        if (Position == 0 && foo >= 0 && foo <= 9)
                                        {
                                            ReachedNum = true;
                                            CoordX = CoordX * 10 + foo;
                                        }
                                        else if (Position == 0 && ReachedNum)
                                        {
                                            Position = 1;
                                            ReachedNum = false;
                                        }
                                        if (Position == 1 && foo >= 0 && foo <= 9)
                                        {
                                            ReachedNum = true;
                                            CoordY = CoordY * 10 + foo;
                                        }
                                        else if (Position == 1 && ReachedNum)
                                        {
                                            Position = 2;
                                            ReachedNum = false;
                                        }
                                    }
                                    MapH = CoordY + 1;
                                    const string NW = "\"FEATURE_.+?\"";
                                    const string T = "\"TERRAIN_.+?\"";
                                    const string H = "\"TERRAIN_.+?HILLS\"";
                                    const string W = "\"TERRAIN_(OCEAN|COAST)\"";
                                    const string C = "(0|1),(0|1),(0|1)}}";
                                    const string L = " -- Lake";
                                    const string Volc = "\"FEATURE_VOLCANO\"";
                                    const string VolcHill = "HILLS";
                                    Match match = Regex.Match(MapArray, @NW);
                                    Match terr = Regex.Match(MapArray, @T);
                                    Match hills = Regex.Match(MapArray, @H);
                                    Match water = Regex.Match(MapArray, @W);
                                    Match cliffs = Regex.Match(MapArray, @C);
                                    Match lake = Regex.Match(MapArray, @L);
                                    Match volcano = Regex.Match(MapArray, @Volc);
                                    Match volcanohill = Regex.Match(MapArray, VolcHill);
                                    if (match.Success && terr.Success)
                                    {
                                        if (Civ6Wonder(match.Groups[0].Value) != null)
                                        {
                                            NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X=\"" + CoordX + "\" Y=\"" + CoordY + "\" FeatureType=" +
                                                match.Groups[0].Value + " TerrainType=" + terr.Groups[0].Value + " />" + NatWondTemp;
                                            MapArray = MapArray.Replace(match.Groups[0].Value, "-1");
                                        }
                                        else if (volcano.Success)
                                        {
                                            VolcanoString = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X=\"" + CoordX + "\" Y=\"" + CoordY + "\" RuleSet=\"RULESET_EXPANSION_2\" " +
                                                "TerrainType=" + terr.Groups[0].Value + " FeatureType=\"FEATURE_VOLCANO\" />";
                                            MapArray = MapArray.Replace(volcano.Groups[0].Value, "-1");
                                            if (volcanohill.Success)
                                            {
                                                MapArray = MapArray.Replace(volcanohill.Groups[0].Value, "MOUNTAIN");
                                                VolcanoString = VolcanoString.Replace("HILLS", "MOUNTAIN");
                                            }
                                        }
                                    }
                                    if (LastColumn) MapW = CoordX + 1;
                                    if (hills.Success) HasHills[CoordX, CoordY] = true;
                                    else if (water.Success) if (!lake.Success) WaterArray[CoordX, CoordY] = true;
                                    if (lake.Success) MapArray = MapArray.Replace(lake.Groups[0].Value, "");
                                    LuaCliffsEnd = "";
                                    if (CliffsGenerate.Checked)
                                    {
#region Cliffs
                                        if (cliffs.Success)
                                        {
                                            MapArray = MapArray.Replace(cliffs.Groups[0].Value, "");

                                            if (CoordY % 2 == 1)
                                            {
                                                if (CoordX < MapW - 1)
                                                {
                                                    if (CoordY > 0) MapArray += Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX + 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY - 1]) + "}}";
                                                    else MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                                }
                                                if (CoordX == 0)
                                                {
                                                    if (CoordY > 0) LuaCliffsEnd += Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 1, CoordY - 1]) + "," +
                                                            Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + "," +
                                                            Cliff_Generator(WaterArray[0, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY - 1]) + "}}";
                                                    else LuaCliffsEnd += "0," + Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + ",0}}";
                                                }
                                            }
                                            else
                                            {
                                                if (CoordX < MapW - 1 && CoordX > 0)
                                                {
                                                    if (CoordY > 0) MapArray += Cliff_Generator(WaterArray[CoordX - 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX - 1, CoordY - 1]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "}}";
                                                    else MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                                }
                                                if (CoordX == 0)
                                                {
                                                    if (CoordY > 0)
                                                    {
                                                        MapArray += Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[MapW - 1, CoordY - 1]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                            Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "}}";
                                                        LuaCliffsEnd += Cliff_Generator(WaterArray[MapW - 2, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 2, CoordY - 1]) + "," +
                                                            Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + "," +
                                                            Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 1, CoordY - 1]) + "}}";
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                                        LuaCliffsEnd += "0," + Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + ",0}}";
                                                    }
                                                }
                                            }

                                        }
#endregion
                                    }
                                    LuaTemp = "\n" + MapArray + LuaTemp;
                                    LastColumn = false;
                                    ExtraPlacement += VolcanoString;
                                    VolcanoString = "";
                                    if (CoordX == 0)
                                    {
                                        NatWond += NatWondTemp;
                                        NatWondTemp = "";
                                        LuaGenMap += LuaTemp + LuaCliffsEnd;
                                        LuaTemp = "";
                                    }
                                }
#endregion

#region civ 5
                                else if (ingame.Success && !Civ6)
                                {
                                    Civ5 = true;
                                    int NumberPosition = 13, Multiplier = 1, CoordX = 0, CoordY = 0;
                                    int[] IntArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                    string ReversedString = Reverse(convertcode.Groups[0].Value);
                                    foreach (char c in ReversedString)
                                    {
                                        int foo = c - '0';
                                        if (foo >= 0 && foo <= 9)
                                        {
                                            IntArray[NumberPosition] += foo * Multiplier;
                                            Multiplier = Multiplier * 10;
                                        }
                                        if (c == '-')
                                        {
                                            IntArray[NumberPosition] *= -1;
                                            Multiplier = 1;
                                        }
                                        if (c == ',' || c == ']')
                                        {
                                            NumberPosition--;
                                            Multiplier = 1;
                                        }
                                    }
                                    CoordX = IntArray[0];
                                    CoordY = IntArray[1];
                                    if (IntArray[2] < 5) IntArray[2] = IntArray[2] * 3 + 2 - IntArray[3];
                                    else if (IntArray[2] > 4)
                                    {
                                        IntArray[2] = IntArray[2] + 10;
                                        IntArray[5] = 0;
                                    }
                                    const string L = "-- Lake";
                                    Match lake = Regex.Match(convertcode.Groups[0].Value, @L);
                                    if (IntArray[3] == 1) HasHills[IntArray[0], IntArray[1]] = true;
                                    if (IntArray[3] == 3 && !lake.Success) WaterArray[IntArray[0], IntArray[1]] = true;
                                    if (FirstLine)
                                    {
                                        MapW = CoordX + 1;
                                        FirstLine = false;
                                    }
                                    MapH = CoordY + 1;
                                    if (Civ5Wonder(IntArray[4]) != null)
                                    {
                                        NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + CoordX + "\" Y = \"" + CoordY + "\" FeatureType = \"" +
                                            Civ5Wonder(IntArray[4]) + "\" TerrainType = \"" + Civ6Terrain(IntArray[2]) + "\" />" + NatWondTemp;
                                        IntArray[4] = -1;
                                    }
                                    string MapArray = "MapToConvert[" + IntArray[0] + "][" + IntArray[1] + "]={\"" + Civ6Terrain(IntArray[2]) + "\"," + Civ6Feature(IntArray[4]) + "," +
                                        Civ6Continent(IntArray[5]) + ",{{" + IntArray[6] + "," + IntArray[7] + "},{" + IntArray[8] + "," + IntArray[9] + "},{" + IntArray[10] + "," +
                                        IntArray[11] + "}},{" + Civ6Resource(IntArray[12]) + "," + IntArray[13] + "},{";
                                    LuaCliffsEnd = "";
                                    if (CliffsGenerate.Checked)
                                    {
#region Cliffs
                                        if (CoordY % 2 == 1)
                                        {
                                            if (CoordX < MapW - 1)
                                            {
                                                if (CoordY > 0) MapArray += Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX + 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY - 1]) + "}}";
                                                else MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                            }
                                            if (CoordX == 0)
                                            {
                                                if (CoordY > 0) LuaCliffsEnd += Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 1, CoordY - 1]) + "," +
                                                        Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + "," +
                                                        Cliff_Generator(WaterArray[0, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY - 1]) + "}}";
                                                else LuaCliffsEnd += "0," + Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + ",0}}";
                                            }
                                        }
                                        else
                                        {
                                            if (CoordX < MapW - 1 && CoordX > 0)
                                            {
                                                if (CoordY > 0) MapArray += Cliff_Generator(WaterArray[CoordX - 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX - 1, CoordY - 1]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "}}";
                                                else MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                            }
                                            if (CoordX == 0)
                                            {
                                                if (CoordY > 0)
                                                {
                                                    MapArray += Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[MapW - 1, CoordY - 1]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + "," +
                                                        Cliff_Generator(WaterArray[CoordX, CoordY - 1], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX, CoordY - 1]) + "}}";
                                                    LuaCliffsEnd += Cliff_Generator(WaterArray[MapW - 2, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 2, CoordY - 1]) + "," +
                                                        Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + "," +
                                                        Cliff_Generator(WaterArray[MapW - 1, CoordY - 1], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[MapW - 1, CoordY - 1]) + "}}";
                                                }
                                                else
                                                {
                                                    MapArray += "0," + Cliff_Generator(WaterArray[CoordX + 1, CoordY], HasHills[CoordX, CoordY], WaterArray[CoordX, CoordY], HasHills[CoordX + 1, CoordY]) + ",0}}";
                                                    LuaCliffsEnd += "0," + Cliff_Generator(WaterArray[0, CoordY], HasHills[MapW - 1, CoordY], WaterArray[MapW - 1, CoordY], HasHills[0, CoordY]) + ",0}}";
                                                }
                                            }
                                        }
                                    }
                                    if (CliffsImport.Checked) MapArray += "0,0,0}}";
#endregion
                                    LuaTemp = "\n" + MapArray + LuaTemp;
                                    LastColumn = false;
                                    if (CoordX == 0)
                                    {
                                        NatWond += NatWondTemp;
                                        NatWondTemp = "";
                                        LuaGenMap += LuaTemp + LuaCliffsEnd;
                                        LuaTemp = "";
                                        LastColumn = true;
                                    }
                                }
#endregion
                                else if (Civ6 || Civ5) break;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
#endregion
                string Wrapped = "" + WrapX.Checked;
                LuaFile = LuaStart + MapW + "\nlocal g_iH = " + MapH + LuaEndStart + Wrapped.ToLower() + LuaEndWrap + LuaGenMap + LuaEndMap;
                DirectoryInfo LuaDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Lua");
                File.Create(FolderPath + "\\" + ProjectName + "\\Lua\\" + ProjectName + "_Map.lua").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Lua\\" + ProjectName + "_Map.lua", LuaFile);
                DirectoryInfo MapDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Map");
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\NaturalWonders.xml", NatWond + NatWondEnd);
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\ExtraPlacement.xml", ExtraPlacement + ExtraPlacementEnd);
#endregion

#region Config

#region Config Values
                if (STDRules.Checked) ConfigMap += "\t\t<Row File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                if (RNFRules.Checked) ConfigMap += "\t\t<Row Domain=\"Maps:Expansion1Maps\" File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                if (GSRules.Checked) ConfigMap += "\t\t<Row Domain=\"Maps:Expansion2Maps\" File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
#region Rivers
                if (OneSelected(RiversGenerate.Checked, RiversImport.Checked, RiversEmpty.Checked)) ConfigParameters += ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT",
                    DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 0, 231);
                else ConfigParameters += ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT",
                    DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 1, 231);
#endregion

#region Continents
                if (ContinentsGenerate.Checked && !ContinentsImport.Checked || !ContinentsGenerate.Checked && ContinentsImport.Checked) ConfigParameters += ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT",
                    DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 0, 232);
                else ConfigParameters += ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT",
                    DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 1, 232);
#endregion

#region Natural Wonders
                if (OneSelected(WondersGenerate.Checked, WondersImport.Checked, WondersEmpty.Checked)) ConfigParameters += ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT",
                    DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 0, 244);
                else ConfigParameters += ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT",
                    DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 1, 244);
#endregion

#region Features
                if (OneSelected(FeaturesGenerate.Checked, FeaturesImport.Checked, FeaturesEmpty.Checked))
                {
                    ConfigParameters += ParameterRow(ProjectName, "FeaturesPlacement", "FEATURES_PLACEMENT", DefaultPlacement(FeaturesImport.Checked, FeaturesGenerate.Checked, FeaturesEmpty.Checked), "FeaturesPlacement", 0, 0, 245);
                    ConfigParameters += ParameterRow(ProjectName, "Rainfall", "RAINFALL", "2", "rainfall", 0, 1, 250);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "FeaturesPlacement", "FEATURES_PLACEMENT", DefaultPlacement(FeaturesImport.Checked, FeaturesGenerate.Checked, FeaturesEmpty.Checked), "FeaturesPlacement", 0, 1, 245);
                    ConfigParameters += ParameterRow(ProjectName, "Rainfall", "RAINFALL", "2", "rainfall", 0, 1, 250);
                }
#endregion

#region Resources
                if (OneSelected(ResourcesGenerate.Checked, ResourcesImport.Checked, ResourcesEmpty.Checked))
                {
                    ConfigParameters += ParameterRow(ProjectName, "ResourcesPlacement", "RESOURCES_PLACEMENT", DefaultPlacement(ResourcesImport.Checked, ResourcesGenerate.Checked, ResourcesEmpty.Checked), "ResourcesPlacement", 0, 0, 269);
                    ConfigParameters += ParameterRow(ProjectName, "Resources", "RESOURCES", "2", "resources", 0, 1, 270);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "ResourcesPlacement", "RESOURCES_PLACEMENT", DefaultPlacement(ResourcesImport.Checked, ResourcesGenerate.Checked, ResourcesEmpty.Checked), "ResourcesPlacement", 0, 1, 269);
                    ConfigParameters += ParameterRow(ProjectName, "Resources", "RESOURCES", "2", "resources", 0, 1, 270);
                }
#endregion

#region TSL
                if (TSLEnable.Checked)
                {
                    ConfigParameters += ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 280);
                    ConfigParameters += ParameterRow(ProjectName, "CivilizationPlacement", "CIVILIZATION_PLACEMENT", "PLACEMENT_TSL", "CivilizationPlacement", 0, 1, 280);
                    ConfigParameters += ParameterRow(ProjectName, "ForceTSL", "FORCE_TSL", "FORCE_TSL_OFF", "ForceTSL", 0, 1, 281);
                    ConfigParameters += ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 285);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 280);
                    ConfigParameters += ParameterRow(ProjectName, "CivilizationPlacement", "CIVILIZATION_PLACEMENT", "PLACEMENT_DEFAULT", "CivilizationPlacement", 0, 0, 280);
                    ConfigParameters += ParameterRow(ProjectName, "ForceTSL", "FORCE_TSL", "FORCE_TSL_OFF", "ForceTSL", 0, 1, 281);
                    ConfigParameters += ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 285);
                }
#endregion

#region RCN
                if (RCNEnable.Checked)
                {
                    ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"AutoCityNaming\" Name=\"LOC_MAP_AUTO_CITY_NAMING_NAME\" Description=\"\" Domain=\"bool\" DefaultValue=\"1\" ConfigurationGroup=\"Map\" " +
                                        "ConfigurationId=\"AutoCityNaming\" GroupId=\"MapOptions\" Visible=\"1\" SortIndex=\"2000\"/>";
                }
#endregion

                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapSize\" Name=\"LOC_MAP_SIZE\" Description=\"\" Domain=\"StandardMapSizes\" " +
                    "DefaultValue=\"" + SizeOfMap(MapW, MapH) + "\" ConfigurationGroup=\"Map\" ConfigurationId=\"MAP_SIZE\" GroupId=\"MapOptions\" Hash=\"1\" Visible=\"0\" SortIndex=\"225\"/>\n";
                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"HideSize\" Name=\"HideSize\" Description=\"\" Domain=\"bool\" " +
                    "DefaultValue=\"1\" ConfigurationGroup=\"Map\" ConfigurationId=\"HideSize\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";
                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapName\" Name=\"MapName\" Description=\"\" Domain=\"text\" " +
                    "DefaultValue=\"" + ProjectName + "_Map\" ConfigurationGroup=\"Map\" ConfigurationId=\"MapName\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";

#region Map Supported Values
                if (!RiversGenerate.Checked || !RiversImport.Checked || !RiversEmpty.Checked) ConfigMapSupport += MapConfig_Generator(RiversGenerate.Checked, ProjectName, "Rivers", "DEFAULT") +
                        MapConfig_Generator(RiversImport.Checked, ProjectName, "Rivers", "IMPORT") + MapConfig_Generator(RiversEmpty.Checked, ProjectName, "Rivers", "EMPTY");
                if (!ContinentsGenerate.Checked || !ContinentsImport.Checked) ConfigMapSupport += MapConfig_Generator(ContinentsGenerate.Checked, ProjectName, "Continents", "DEFAULT") +
                        MapConfig_Generator(ContinentsImport.Checked, ProjectName, "Continents", "IMPORT");
                if (!WondersGenerate.Checked || !WondersImport.Checked || !WondersEmpty.Checked) ConfigMapSupport += MapConfig_Generator(WondersGenerate.Checked, ProjectName, "NaturalWonders", "DEFAULT") +
                        MapConfig_Generator(WondersImport.Checked, ProjectName, "NaturalWonders", "IMPORT") + MapConfig_Generator(WondersEmpty.Checked, ProjectName, "NaturalWonders", "EMPTY");
                if (!FeaturesGenerate.Checked || !FeaturesImport.Checked || !FeaturesEmpty.Checked) ConfigMapSupport += MapConfig_Generator(FeaturesGenerate.Checked, ProjectName, "Features", "DEFAULT") +
                        MapConfig_Generator(FeaturesImport.Checked, ProjectName, "Features", "IMPORT") + MapConfig_Generator(FeaturesEmpty.Checked, ProjectName, "Features", "EMPTY");
                if (!ResourcesGenerate.Checked || !ResourcesImport.Checked || !ResourcesEmpty.Checked) ConfigMapSupport += MapConfig_Generator(ResourcesGenerate.Checked, ProjectName, "Resources", "DEFAULT") +
                        MapConfig_Generator(ResourcesImport.Checked, ProjectName, "Resources", "IMPORT") + MapConfig_Generator(ResourcesEmpty.Checked, ProjectName, "Resources", "EMPTY");
#endregion
                Config = ConfigMap + ConfigParameters + ConfigMapSupport + ConfigEnd;
                DirectoryInfo ConfigDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Config");
                File.Create(FolderPath + "\\" + ProjectName + "\\Config\\Config.xml").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Config\\Config.xml", Config);
#endregion

#region Config Text
                string ConfigText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<LocalizedText>\n\t\t<Replace Tag=\"LOC_" + ProjectName + "_Map_NAME\" Language=\"en_US\">\n" +
                                    "\t\t\t<Text>" + ProjectText.Text + "</Text>\n\t\t</Replace>\n\t\t<Replace Tag=\"LOC_" + ProjectName + "_Map_DESC\" Language=\"en_US\"\n>" +
                                    "\t\t\t<Text>" + ProjectText.Text + " (" + MapW + "x" + MapH + ") by " + AuthorText.Text + "</Text>\n\t\t</Replace>\n\t</LocalizedText>\n</GameData>";
                File.Create(FolderPath + "\\" + ProjectName + "\\Config\\Config_Text.xml").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Config\\Config_Text.xml", ConfigText);
#endregion

#endregion

#region TSL, Real City Naming
                string TSLText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<!-- True Start Locations. You have to fill this in manually -->" +
                                 "\n\t<StartPosition>\n\t\t<!--<Replace MapName=\"" + ProjectName + "_Map\" Civilization =\"CIVILIZATION_AMERICA\"	X=\"0\" Y=\"0\" />-->" +
                                 "\n\t</StartPosition>\n\n\t<!-- Real City Naming. You have to fill this in manually -->\n\t<!--\n\t\t* CityLocaleName must have a matching entry in MapText.xml\n\t\t" +
                                 "* Area is by default 1 (i.e. a radius of 1, i.e. one center tile and six surrounding tiles.\n\t\t  A radius of 2 adds yet another 12 surrounding tiles. A radius of 0 is only 1 tile)\n\t\t" +
                                 "* Civilization should only have a value if a name applies to only one civ. I.e. a tile can\n\t\t  have multiple entries for different civs, such as \"London\" as a standard value for London,\n\t\t  but \"Londres\" for France or \"Londinium\" for Rome\n\t-->" +
                                 "\n\t<CityMap>\n\t\t<!--<Replace MapName=\"" + ProjectName + "_Map\" X=\"0\" Y=\"0\" CityLocaleName=\"LOC_CITY_NAME_" + ProjectName + 
                                 "_Map_SOME_CITY_NAME\" Area=\"1\" Civilization=\"CIVILIZATION_AMERICA\"/>-->\n\t</CityMap>\n</GameData>";
                if (!File.Exists(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml"))
                {
                    File.Create(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml").Dispose();
                    System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml", TSLText);
                }

                string RCNText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<!-- You have to fill this in manually -->" +
                                 "\n\t<LocalizedText>\n\t\t<!--<Replace Tag=\"LOC_CITY_NAME_" + ProjectName + "_Map_SOME_CITY_NAME\" Text=\"Some City Name\" Language=\"en_US\" />-->" +
                                 "\n\t</LocalizedText>\n</GameData>";
                if (!File.Exists(FolderPath + "\\" + ProjectName + "\\Map\\MapText.xml"))
                {
                    File.Create(FolderPath + "\\" + ProjectName + "\\Map\\MapText.xml").Dispose();
                    System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\MapText.xml", RCNText);
                }
#endregion

#region Mod Info
                string ModInfo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Mod id=\"" + ModID + "\" version = \"1\">\n\t<Properties>\n\t\t<Name>" + AuthorText.Text + " - " + ProjectText.Text + "</Name>" +
                                 "\n\t\t<Description>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Civ 6 Map Maker</Description>" +
                                 "\n\t\t<Teaser>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Civ 6 Map Maker</Teaser>\n\t\t<Authors>" +
                                 AuthorText.Text + "</Authors>\n\t<CompatibleVersions>" + GameVersions + "</CompatibleVersions>\n\t</Properties>\n\t<Dependencies>\n\t\t<Mod id=\"36e88483-48fe-4545-b85f-bafc50dde315\" title=\"Yet (not) Another Maps Pack\"/>\n\t</Dependencies>" +
                                 "\n\t<FrontEndActions>\n\t\t<UpdateDatabase id=\"" + ProjectName + "_SETTING\">\n\t\t\t<File>Config/Config.xml</File>\n\t\t</UpdateDatabase>" +
                                 "\n\t\t<UpdateText id=\"NewAction\">\n\t\t\t<File>Config/Config_Text.xml</File>\n\t\t</UpdateText>\n\t</FrontEndActions>" +
                                 "\n\t<InGameActions>\n\t\t<ImportFiles id=\"" + ProjectName + "_IMPORT\">\n\t\t\t<File>Lua/" + ProjectName + "_Map.lua</File>\n\t\t</ImportFiles>" +
                                 "\n\t\t<UpdateDatabase id=\"NewAction\">\n\t\t\t<File>Map/Map.xml</File>\n\t\t\t<File>Map/NaturalWonders.xml</File>\n\t\t\t<File>Map/ExtraPlacement.xml</File>\n\t\t</UpdateDatabase>" +
                                 "\n\t\t<UpdateText id=\"YNAMP_DEFAULT_LOC\">\n\t\t\t<File>Map/MapText.xml</File>\n\t\t</UpdateText>\n\t</InGameActions>\n\t<Files>\n\t\t<File>Config/Config.xml</File>" +
                                 "\n\t\t<File>Config/Config_Text.xml</File>\n\t\t<File>Map/Map.xml</File>\n\t\t<File>Map/MapText.xml</File>\n\t\t<File>Map/NaturalWonders.xml</File>\n\t\t<File>Map/ExtraPlacement.xml</File>\n\t\t<File>Lua/" +
                                 ProjectName + "_Map.lua</File>\n\t</Files>\n</Mod>";
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\" + ProjectName + ".modinfo", ModInfo);
                Application.Restart();
#endregion
            }
        }

#region Methods
        public string ParameterRow(string ProjectName, string ParameterID, string ParameterName, string DefaultValue, string ConfigurationID, int Hash, int Visible, int SortIndex)
        {
            if (Visible == 1 && Hash == 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" +
                    ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" +
                    ConfigurationID + "\"	GroupId=\"MapOptions\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 1 && Hash < 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" +
                    ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" +
                    ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 0 && Hash == 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" +
                    ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" +
                    ConfigurationID + "\"	GroupId=\"MapOptions\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 0 && Hash < 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" +
                    ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" +
                    ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" +
                ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" +
                ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
        }

        public bool OneSelected(bool first, bool second, bool third)
        {
            if (first && !second && !third || !first && second && !third || !first && !second && third) return true;
            return false;
        }

        public string DefaultPlacement(bool first, bool second, bool third)
        {
            if (first) return "PLACEMENT_IMPORT";
            if (second) return "PLACEMENT_DEFAULT";
            if (third) return "PLACEMENT_EMPTY";
            return "PLACEMENT_DEFAULT";
        }

        public string SizeOfMap(int x, int y)
        {
            if (x * y < 1500) return "MAPSIZE_DUEL";
            if (x * y < 2700) return "MAPSIZE_TINY";
            if (x * y < 3900) return "MAPSIZE_SMALL";
            if (x * y < 5100) return "MAPSIZE_STANDARD";
            if (x * y < 6200) return "MAPSIZE_LARGE";
            if (x * y < 8000) return "MAPSIZE_HUGE";
            if (x * y < 13000) return "MAPSIZE_ENORMOUS";
            if (x * y < 21000) return "MAPSIZE_GIANT";
            if (x * y >= 21000) return "MAPSIZE_LUDICROUS";
            return "MAPSIZE_STANDARD";
        }

        public string Reverse(string s)
        {
            char[] cArray = s.ToCharArray();
            Array.Reverse(cArray);
            return new string(cArray);
        }

        public string Civ6Wonder(string w)
        {
            if (w == "\"FEATURE_BARRIER_REEF\"" || w == "\"FEATURE_CLIFFS_DOVER\"" || w == "\"FEATURE_CRATER_LAKE\"" ||
                w == "\"FEATURE_DEAD_SEA\"" || w == "\"FEATURE_EVEREST\"" || w == "\"FEATURE_GALAPAGOS\"" || w == "\"FEATURE_KILIMANJARO\"" ||
                w == "\"FEATURE_PANTANAL\"" || w == "\"FEATURE_PIOPIOTAHI\"" || w == "\"FEATURE_TORRES_DEL_PAINE\"" ||
                w == "\"FEATURE_TSINGY\"" || w == "\"FEATURE_YOSEMITE\"" || w == "\"FEATURE_DELICATE_ARCH\"" || w == "\"FEATURE_EYE_OF_THE_SAHARA\"" ||
                w == "\"FEATURE_LAKE_RETBA\"" || w == "\"FEATURE_MATTERHORN\"" || w == "\"FEATURE_RORAIMA\"" ||
                w == "\"FEATURE_UBSUNUR_HOLLOW\"" || w == "\"FEATURE_ZHANGYE_DANXIA\"" || w == "\"FEATURE_HA_LONG_BAY\"" || w == "\"FEATURE_EYJAFJALLAJOKULL\"" ||
                w == "\"FEATURE_LYSEFJORDEN\"" || w == "\"FEATURE_GIANTS_CAUSEWAY\"" || w == "\"FEATURE_ULURU\"" || 
                w == "\"FEATURE_CHOCOLATEHILLS\"" || w == "\"FEATURE_DEVILSTOWER\"" || w == "\"FEATURE_GOBUSTAN\"" || w == "\"FEATURE_IKKIL\"" || 
                w == "\"FEATURE_PAMUKKALE\"" || w == "\"FEATURE_VESUVIUS\"" || w == "\"FEATURE_WHITEDESERT\"") return w;
            return null;
        }

        public string Civ6Terrain(int t)
        {
            if (t == 0) return "TERRAIN_GRASS";
            if (t == 1) return "TERRAIN_GRASS_HILLS";
            if (t == 2) return "TERRAIN_GRASS_MOUNTAIN";
            if (t == 3) return "TERRAIN_PLAINS";
            if (t == 4) return "TERRAIN_PLAINS_HILLS";
            if (t == 5) return "TERRAIN_PLAINS_MOUNTAIN";
            if (t == 6) return "TERRAIN_DESERT";
            if (t == 7) return "TERRAIN_DESERT_HILLS";
            if (t == 8) return "TERRAIN_DESERT_MOUNTAIN";
            if (t == 9) return "TERRAIN_TUNDRA";
            if (t == 10) return "TERRAIN_TUNDRA_HILLS";
            if (t == 11) return "TERRAIN_TUNDRA_MOUNTAIN";
            if (t == 12) return "TERRAIN_SNOW";
            if (t == 13) return "TERRAIN_SNOW_HILLS";
            if (t == 14) return "TERRAIN_SNOW_MOUNTAIN";
            if (t == 15) return "TERRAIN_COAST";
            if (t == 16) return "TERRAIN_OCEAN";
            return "TERRAIN_OCEAN";
        }

        public string Civ6Feature(int f)
        {
            if (f == 0) return "\"FEATURE_ICE\"";
            if (f == 1) return "\"FEATURE_JUNGLE\"";
            if (f == 2) return "\"FEATURE_MARSH\"";
            if (f == 3) return "\"FEATURE_OASIS\"";
            if (f == 4) return "\"FEATURE_FLOODPLAINS\"";
            if (f == 5) return "\"FEATURE_FOREST\"";
            if (f == 17) return "\"FEATURE_REEF\"";
            return "-1";
        }

        public string Civ5Wonder(int w)
        {
            if (w == 7) return "FEATURE_CRATER_LAKE";
            if (w == 8) return "FEATURE_MATTERHORN";
            if (w == 9) return "FEATURE_YOSEMITE";
            if (w == 10) return "FEATURE_BARRIER_REEF";
            if (w == 11) return "FEATURE_EYJAFJALLAJOKULL";
            if (w == 12) return "FEATURE_PIOPIOTAHI";
            if (w == 13) return "FEATURE_UBSUNUR_HOLLOW";
            if (w == 14) return "FEATURE_LAKE_RETBA";
            if (w == 15) return "FEATURE_TSINGY";
            if (w == 16) return "FEATURE_PANTANAL";
            if (w == 18) return "FEATURE_TORRES_DEL_PAINE";
            if (w == 19) return "FEATURE_DELICATE_ARCH";
            if (w == 20) return "FEATURE_EVEREST";
            if (w == 21) return "FEATURE_ULURU";
            if (w == 22) return "FEATURE_DEAD_SEA";
            if (w == 23) return "FEATURE_KILIMANJARO";
            if (w == 24) return "FEATURE_UBSUNUR_HOLLOW";
            return null;
            // probably needs to be looked through
        }

        public string Civ6Continent(int c)
        {
            if (c == 0) return "-1";
            if (c == 1) return "\"CONTINENT_AMERICA\"";
            if (c == 2) return "\"CONTINENT_ASIA\"";
            if (c == 3) return "\"CONTINENT_AFRICA\"";
            if (c == 4) return "\"CONTINENT_EUROPE\"";
            return "\"CONTINENT_ZEALANDIA\"";
        }

        public string Civ6Resource(int r)
        {
            if (r == 0) return "\"RESOURCE_IRON\"";
            if (r == 1) return "\"RESOURCE_HORSE\"";
            if (r == 2) return "\"RESOURCE_COAL\"";
            if (r == 3) return "\"RESOURCE_OIL\"";
            if (r == 4) return "\"RESOURCE_ALUMINUM\"";
            if (r == 5) return "\"RESOURCE_URANIUM\"";
            if (r == 6) return "\"RESOURCE_WHEAT\"";
            if (r == 7) return "\"RESOURCE_CATTLE\"";
            if (r == 8) return "\"RESOURCE_SHEEP\"";
            if (r == 9) return "\"RESOURCE_DEER\"";
            if (r == 10) return "\"RESOURCE_BANANAS\"";
            if (r == 11) return "\"RESOURCE_FISH\"";
            if (r == 12) return "\"RESOURCE_STONE\"";
            if (r == 13) return "\"RESOURCE_WHALES\"";
            if (r == 14) return "\"RESOURCE_PEARLS\"";
            if (r == 15) return "\"RESOURCE_SILVER\""; //Gold -> Niter
            if (r == 16) return "\"RESOURCE_SILVER\"";
            if (r == 17) return "\"RESOURCE_DIAMONDS\""; //Diamonds
            if (r == 18) return "\"RESOURCE_MARBLE\"";
            if (r == 19) return "\"RESOURCE_IVORY\"";
            if (r == 20) return "\"RESOURCE_FURS\"";
            if (r == 21) return "\"RESOURCE_DYES\"";
            if (r == 22) return "\"RESOURCE_SPICES\"";
            if (r == 23) return "\"RESOURCE_SILK\"";
            if (r == 24) return "\"RESOURCE_SUGAR\"";
            if (r == 25) return "\"RESOURCE_COTTON\"";
            if (r == 26) return "\"RESOURCE_WINE\"";
            if (r == 27) return "\"RESOURCE_INCENSE\"";
            if (r == 28) return "\"RESOURCE_JADE\""; //Jewelry
            if (r == 29) return "\"RESOURCE_DIAMONDS\""; //Porcelain
            if (r == 30) return "\"RESOURCE_COPPER\"";
            if (r == 31) return "\"RESOURCE_SALT\"";
            if (r == 32) return "\"RESOURCE_CRABS\"";
            if (r == 33) return "\"RESOURCE_TRUFFLES\"";
            if (r == 34) return "\"RESOURCE_CITRUS\"";
            if (r == 40) return "\"RESOURCE_FURS\""; //Bison to Furs
            if (r == 41) return "\"RESOURCE_COCOA\"";
            return "-1";
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            GenerateMap.Enabled = true;
            timer.Stop();
        }

        public string Cliff_Generator(bool Water, bool Hills, bool Water2, bool Hills2)
        {
            if (Water && Hills) return "1";
            else if (Hills2 && Water2) return "1";
            else return "0";
        }

        public string MapConfig_Generator(bool Setting, string Name, string Placement, string Method)
        {
            if (Setting) return "\t\t<Row Map = \"" + Name + "_Map.lua\" Domain = \"" + Placement + "Placement\" Value = \"PLACEMENT_" + Method + "\" />\n";
            return "";
        }
#endregion
    }
}

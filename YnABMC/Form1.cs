using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace YnABMC
{
    public partial class Form1 : Form
    {
        string Version = "0.1.2";
        string FolderPath = "", BmpFilePath = "", ProjectName = "", AuthorName = "", ModID = "";
        bool Lua = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Yet (not) Another Bit Map Converter - v" + Version;
        }

#region MapValues

#region Terrain
        string Grass = "(32,192,64)", Plains = "(192,224,0)", Desert = "(224,128,0)", Tundra = "(192,220,192)",
               Snow = "(255,255,255)", Coast = "(0,160,192)", Ocean = "(64,64,192)", Lake = "(0,96,128)";
#endregion

#region Feratures and Plots
        string Mountain = "(128,128,128)", Hills = "(192,192,192)";

        string Ice = "(255,255,255)", Jungle = "(224,192,0)", Marsh = "(0,160,192)", Oasis = "(0,160,192)",
               FloodPlains = "(192,224,0)", Woods = "(192,128,64)", Reef = "(224,192,0)"; //, Fallout = "(224,160,192)";
        #endregion

#region Natural Wonders
        string BarrierReef = "(255,0,0)", CliffsDover = "(224,32,0)", CraterLake = "(192,32,0)", DeadSea = "(160,32,0)",
               Everest = "(128,0,0)", Galapagos = "(64,32,0)", Kilimanjaro = "(0,0,0)", Pantanal = "(0,128,128)",

               Piopiotahi = "(0,255,0)", TorresDelPaine = "(0,224,0)", Tsingy = "(0,192,0)", Yosemite = "(0,160,0)",
               Uluru = "(0,128,0)", HaLongBay = "(0,96,0)", Eyjafjallajokull = "(0,64,0)", Lysefjorden = "(0,96,128)",

               GiantsCauseway = "(0,0,255)", DelicateArch = "(0,0,192)", EyeOfTheSahara = "(0,0,128)", LakeRetba = "(0,0,64)",
               Matterhorn = "(32,0,64)", Roraima = "(64,64,64)", UbsunurHollow = "(192,128,64)", ZhangyeDanxia = "(128,128,128)";
#endregion

#region Rivers
        string River0 = "(64,64,192)", River1 = "(160,64,192)", Cliffs = "(192,128,64)";
#endregion

#region Continents
        string Africa = "(255,0,0)", Amasia = "(224,32,0)", America = "(192,32,0)", Antarctica = "(160,32,0)",
               Arctica = "(128,0,0)", Asia = "(64,32,0)", Asiamerica = "(0,0,0)", Atlantica = "(0,128,128)",

               Atlantis = "(0,255,0)", Australia = "(0,224,0)", Avalonia = "(0,192,0)", Azania = "(0,160,0)",
               Baltica = "(0,128,0)", Cimmeria = "(0,96,0)", Columbia = "(0,64,0)", CongoCraton = "(0,96,128)",

               Euramerica = "(0,0,255)", Europe = "(0,0,192)", Gondwana = "(0,0,128)", Kalaharia = "(0,0,64)",
               Kazakstania = "(32,0,64)", Kenorland = "(64,64,64)", KumariKandam = "(192,128,64)", Laurasia = "(128,128,128)",

               Laurentia = "(255,255,0)", Lemuria = "(224,224,0)", Mu = "(192,192,0)", Nena = "(160,160,0)",
               NorthAmerica = "(128,128,0)", Novapangaea = "(96,96,0)", Nuna = "(64,64,0)", Oceania = "(0,64,64)",

               Pangaea = "(255,0,255)", PangaeaUltima = "(224,0,192)", Pannotia = "(192,0,192)", Rodinia = "(160,0,192)",
               Siberia = "(128,0,128)", SouthAmerica = "(96,0,128)", TerraAustralis = "(64,0,64)", Ur = "(0,32,64)",

               Vaalbara = "(0,255,255)", Vendian = "(0,192,192)";
#endregion

#region Resources
        string Bananas = "(255,0,0)", Cattle = "(224,32,0)", Copper = "(192,32,0)", Crabs = "(160,32,0)",
               Deer = "(128,0,0)", Fish = "(64,32,0)", Rice = "(0,0,0)", Sheep = "(0,128,128)",

               Stone = "(0,255,0)", Wheat = "(0,224,0)", Citrus = "(0,192,0)", Cocoa = "(0,160,0)",
               Coffee = "(0,128,0)", Cotton = "(0,96,0)", Diamonds = "(0,64,0)", Dyes = "(0,96,128)",

               Furs = "(0,0,255)", Gypsum = "(0,0,192)", Incense = "(0,0,128)", Ivory = "(0,0,64)",
               Jade = "(32,0,64)", Marble = "(64,64,64)", Mercury = "(192,128,64)", Pearls = "(128,128,128)",

               Salt = "(255,255,0)", Silk = "(224,224,0)", Silver = "(192,192,0)", Spices = "(160,160,0)",
               Sugar = "(128,128,0)", Tea = "(96,96,0)", Tobacco = "(64,64,0)", Truffles = "(0,64,64)",

               Whales = "(255,0,255)", Wine = "(224,0,192)", Aluminium = "(192,0,192)", Coal = "(160,0,192)",
               Horses = "(128,0,128)", Iron = "(96,0,128)", Niter = "(64,0,64)", Oil = "(0,32,64)",

               Uranium = "(0,255,255)", AntiquitySite = "(0,192,192)", Shipwreck = "(192,192,192)", Amber = "(255,251,240)",
               Olives = "(255,0,255)", Turtles = "(255,0,0)";
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
                /*CliffsGenerate.Enabled = true;
                CliffsImport.Enabled = true;
                CliffsGenerate.Checked = false;
                CliffsImport.Checked = true;*/
            }
            else
            {
                SelectLua.Enabled = true;
                SelectLua.Visible = true;
                Size = new Size(764, 493);
                /*CliffsGenerate.Enabled = false;
                CliffsImport.Enabled = false;
                CliffsGenerate.Checked = false;
                CliffsImport.Checked = false;*/
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
            if (!RNFRules.Checked)
            {
                STDRules.Checked = true;
                STDRules.Enabled = false;
            }
            else
            {
                STDRules.Enabled = true;
            }
        }
        private void RiversEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (!RiversImport.Checked && !RiversEmpty.Checked)
            {
                RiversGenerate.Checked = true;
                RiversGenerate.Enabled = false;
            }
            else
            {
                RiversGenerate.Enabled = true;
            }
        }

        private void RiversImport_CheckedChanged(object sender, EventArgs e)
        {

            if (!RiversImport.Checked && !RiversEmpty.Checked)
            {
                RiversGenerate.Checked = true;
                RiversGenerate.Enabled = false;
            }
            else
            {
                RiversGenerate.Enabled = true;
            }
        }

        private void ContinentsImport_CheckedChanged(object sender, EventArgs e)
        {

            if (!ContinentsImport.Checked)
            {
                ContinentsGenerate.Checked = true;
                ContinentsGenerate.Enabled = false;
            }
            else
            {
                ContinentsGenerate.Enabled = true;
            }
        }

        private void WondersImport_CheckedChanged(object sender, EventArgs e)
        {

            if (!WondersImport.Checked && !WondersEmpty.Checked)
            {
                WondersGenerate.Checked = true;
                WondersGenerate.Enabled = false;
            }
            else
            {
                WondersGenerate.Enabled = true;
            }
        }

        private void WondersEmpty_CheckedChanged(object sender, EventArgs e)
        {

            if (!WondersImport.Checked && !WondersEmpty.Checked)
            {
                WondersGenerate.Checked = true;
                WondersGenerate.Enabled = false;
            }
            else
            {
                WondersGenerate.Enabled = true;
            }
        }

        private void FeaturesEmpty_CheckedChanged(object sender, EventArgs e)
        {

            if (!FeaturesImport.Checked && !FeaturesEmpty.Checked)
            {
                FeaturesGenerate.Checked = true;
                FeaturesGenerate.Enabled = false;
            }
            else
            {
                FeaturesGenerate.Enabled = true;
            }
        }

        private void FeaturesImport_CheckedChanged(object sender, EventArgs e)
        {

            if (!FeaturesImport.Checked && !FeaturesEmpty.Checked)
            {
                FeaturesGenerate.Checked = true;
                FeaturesGenerate.Enabled = false;
            }
            else
            {
                FeaturesGenerate.Enabled = true;
            }
        }

        private void ResourcesImport_CheckedChanged(object sender, EventArgs e)
        {

            if (!ResourcesImport.Checked && !ResourcesEmpty.Checked)
            {
                ResourcesGenerate.Checked = true;
                ResourcesGenerate.Enabled = false;
            }
            else
            {
                ResourcesGenerate.Enabled = true;
            }
        }

        private void ResourcesEmpty_CheckedChanged(object sender, EventArgs e)
        {

            if (!ResourcesImport.Checked && !ResourcesEmpty.Checked)
            {
                ResourcesGenerate.Checked = true;
                ResourcesGenerate.Enabled = false;
            }
            else
            {
                ResourcesGenerate.Enabled = true;
            }
        }

        private void CliffsImport_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CliffsGenerate_CheckedChanged(object sender, EventArgs e)
        {

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
                            string Comment = "\t\t-- ";

                            Color MapTerrain = Map.GetPixel(MapX + 2, MapY + 1);
                            Color MapHills = Map.GetPixel(MapX + 2, MapY);
                            Color MapPlotFeature = Map.GetPixel(MapX, MapY);
                            Color MapRiverE = Map.GetPixel(MapX + 3, MapY);
                            Color MapRiverSE = Map.GetPixel(MapX + 2, MapY + 2);
                            Color MapRiverSW = Map.GetPixel(MapX - 2, MapY + 2);
                            Color MapResource = Map.GetPixel(MapX - 2, MapY - 2);
                            Color MapContinent = Map.GetPixel(MapX - 2, MapY + 1);
                            Color MapWonder = Map.GetPixel(MapX - 2, MapY - 1);

                            string MatchTerrain = "(" + MapTerrain.R + "," + MapTerrain.G + "," + MapTerrain.B + ")";
                            string MatchHills = "(" + MapHills.R + "," + MapHills.G + "," + MapHills.B + ")";
                            string MatchPlotFeature = "(" + MapPlotFeature.R + "," + MapPlotFeature.G + "," + MapPlotFeature.B + ")";
                            string MatchRiverE = "(" + MapRiverE.R + "," + MapRiverE.G + "," + MapRiverE.B + ")";
                            string MatchRiverSE = "(" + MapRiverSE.R + "," + MapRiverSE.G + "," + MapRiverSE.B + ")";
                            string MatchRiverSW = "(" + MapRiverSW.R + "," + MapRiverSW.G + "," + MapRiverSW.B + ")";
                            string MatchResource = "(" + MapResource.R + "," + MapResource.G + "," + MapResource.B + ")";
                            string MatchContinent = "(" + MapContinent.R + "," + MapContinent.G + "," + MapContinent.B + ")";
                            string MatchWonder = "(" + MapWonder.R + "," + MapWonder.G + "," + MapWonder.B + ")";
#endregion

#region Terrain
                            if (MatchTerrain == Grass)
                            {
                                if (MatchPlotFeature == Mountain)
                                {
                                    CurrentPlot = "TERRAIN_GRASS_MOUNTAIN";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Grassland (Mountain)";
                                }
                                else if (MatchHills == Hills)
                                {
                                    CurrentPlot = "TERRAIN_GRASS_HILLS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Grassland Hills";
                                    HasHills[x, y] = true;
                                }
                                else
                                {
                                    CurrentPlot = "TERRAIN_GRASS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Grassland";
                                }
                            }
                            else if (MatchTerrain == Plains)
                            {
                                if (MatchPlotFeature == Mountain)
                                {
                                    CurrentPlot = "TERRAIN_PLAINS_MOUNTAIN";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Plains (Mountain)";
                                }
                                else if (MatchHills == Hills)
                                {
                                    CurrentPlot = "TERRAIN_PLAINS_HILLS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Plains Hills";
                                    HasHills[x, y] = true;
                                }
                                else
                                {
                                    CurrentPlot = "TERRAIN_PLAINS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Plains";
                                }
                            }
                            else if (MatchTerrain == Desert)
                            {
                                if (MatchPlotFeature == Mountain)
                                {
                                    CurrentPlot = "TERRAIN_DESERT_MOUNTAIN";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Desert (Mountain)";
                                }
                                else if (MatchHills == Hills)
                                {
                                    CurrentPlot = "TERRAIN_DESERT_HILLS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Desert Hills";
                                    HasHills[x, y] = true;
                                }
                                else
                                {
                                    CurrentPlot = "TERRAIN_DESERT";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Desert";
                                }
                            }
                            else if (MatchTerrain == Tundra)
                            {
                                if (MatchPlotFeature == Mountain)
                                {
                                    CurrentPlot = "TERRAIN_TUNDRA_MOUNTAIN";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Tundra (Mountain)";
                                }
                                else if (MatchHills == Hills)
                                {
                                    CurrentPlot = "TERRAIN_TUNDRA_HILLS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Tundra Hills";
                                    HasHills[x, y] = true;
                                }
                                else
                                {
                                    CurrentPlot = "TERRAIN_TUNDRA";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Tundra";
                                }
                            }
                            else if (MatchTerrain == Snow)
                            {
                                if (MatchPlotFeature == Mountain)
                                {
                                    CurrentPlot = "TERRAIN_SNOW_MOUNTAIN";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Snow (Mountain)";
                                }
                                else if (MatchHills == Hills)
                                {
                                    CurrentPlot = "TERRAIN_SNOW_HILLS";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Snow Hills";
                                    HasHills[x, y] = true;
                                }
                                else
                                {
                                    CurrentPlot = "TERRAIN_SNOW";
                                    CurrentLine += "\"" + CurrentPlot + "\",";
                                    Comment += "Snow";
                                }
                            }
                            else if (MatchTerrain == Coast)
                            {
                                CurrentPlot = "TERRAIN_COAST";
                                CurrentLine += "\"" + CurrentPlot + "\",";
                                Comment += "Coast";
                                WaterArray[x, y] = true;
                            }
                            else if (MatchTerrain == Lake)
                            {
                                CurrentPlot = "TERRAIN_COAST";
                                CurrentLine += "\"" + CurrentPlot + "\",";
                                Comment += "Lake";
                                WaterArray[x, y] = false; //This is only used for cliffs, and cliffs do not generate in lakes
                            }
                            else
                            {
                                CurrentPlot = "TERRAIN_OCEAN";
                                CurrentLine += "\"" + CurrentPlot + "\",";
                                Comment += "Ocean";
                                WaterArray[x, y] = true;
                            }
#endregion

#region Natural Wonders
                            if (MatchWonder == BarrierReef)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_BARRIER_REEF\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_BARRIER_REEF\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == CliffsDover)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_CLIFFS_DOVER\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_CLIFFS_DOVER\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == CraterLake)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_CRATER_LAKE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_CRATER_LAKE\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == DeadSea)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_DEAD_SEA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_DEAD_SEA\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Everest)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_EVEREST\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_EVEREST\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Galapagos)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_GALAPAGOS\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_GALAPAGOS\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Kilimanjaro)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_KILIMANJARO\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_KILIMANJARO\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Pantanal)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_PANTANAL\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_PANTANAL\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Piopiotahi)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_PIOPIOTAHI\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_PIOPIOTAHI\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == TorresDelPaine)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_TORRES_DEL_PAINE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_TORRES_DEL_PAINE\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Tsingy)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_TSINGY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_TSINGY\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Yosemite)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_YOSEMITE\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                //LuaEndWrap += "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_YOSEMITE\"].Index] = { X = " + x + ", Y = " + y + "}";
                            }
                            else if (MatchWonder == Uluru)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_ULURU\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_ULURU\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_ULURU\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == HaLongBay)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_HA_LONG_BAY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_HA_LONG_BAY\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_HA_LONG_BAY\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == Eyjafjallajokull)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_EYJAFJALLAJOKULL\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_EYJAFJALLAJOKULL\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_EYJAFJALLAJOKULL\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == Lysefjorden && MatchTerrain != Lake)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_LYSEFJORDEN\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_LYSEFJORDEN\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_LYSEFJORDEN\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == GiantsCauseway)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_GIANTS_CAUSEWAY\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_GIANTS_CAUSEWAY\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_GIANTS_CAUSEWAY\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == DelicateArch)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_DELICATE_ARCH\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_DELICATE_ARCH\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_DELICATE_ARCH\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == EyeOfTheSahara)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_EYE_OF_THE_SAHARA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_EYE_OF_THE_SAHARA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_EYE_OF_THE_SAHARA\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == LakeRetba)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_LAKE_RETBA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_LAKE_RETBA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_LAKE_RETBA\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == Matterhorn)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_MATTERHORN\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_MATTERHORN\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_MATTERHORN\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == Roraima)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_RORAIMA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_RORAIMA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_RORAIMA\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == UbsunurHollow)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_UBSUNUR_HOLLOW\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_UBSUNUR_HOLLOW\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_UBSUNUR_HOLLOW\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
                            else if (MatchWonder == ZhangyeDanxia)
                            {
                                NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + x + "\" Y = \"" + y + "\" FeatureType = \"FEATURE_ZHANGYE_DANXIA\" TerrainType = \"" + CurrentPlot + "\" />" + NatWondTemp;
                                /*LuaEndWrap += "\n\tif GameInfo.Features[\"FEATURE_ZHANGYE_DANXIA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_ZHANGYE_DANXIA\"].Index] = " +
                                                    "{ X = " + x + ", Y = " + y + "}\n\tend";*/
                            }
#endregion

#region Features
                            if (MatchPlotFeature == FloodPlains && MatchTerrain == Desert)
                            {
                                CurrentLine += "\"FEATURE_FLOODPLAINS\",";
                                Comment += " with Flood Plains";
                            }
                            else if (MatchPlotFeature == Ice && (MatchTerrain == Ocean || MatchTerrain == Coast))
                            {
                                CurrentLine += "\"FEATURE_ICE\",";
                                Comment += " with Ice";
                            }
                            else if (MatchPlotFeature == Jungle && MatchTerrain != Coast)
                            {
                                CurrentLine += "\"FEATURE_JUNGLE\",";
                                Comment += " with Jungle";
                            }
                            else if (MatchPlotFeature == Woods)
                            {
                                CurrentLine += "\"FEATURE_FOREST\",";
                                Comment += " with Woods";
                            }
                            else if (MatchPlotFeature == Oasis && MatchTerrain == Desert)
                            {
                                CurrentLine += "\"FEATURE_OASIS\",";
                                Comment += " with Oasis";
                            }
                            else if (MatchPlotFeature == Marsh && MatchTerrain == Grass)
                            {
                                CurrentLine += "\"FEATURE_MARSH\",";
                                Comment += " with Marsh";
                            }
                            else if (MatchPlotFeature == Reef && MatchTerrain == Coast)
                            {
                                CurrentLine += "\"FEATURE_REEF\",";
                                Comment += " with Reef";
                            }
                            else
                            {
                                CurrentLine += "-1,";
                            }
#endregion

#region Continent
                            if ((MatchTerrain != Ocean || MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Africa)
                            {
                                CurrentLine += "\"CONTINENT_AFRICA\",{{";
                                Comment += " in Africa";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Amasia)
                            {
                                CurrentLine += "\"CONTINENT_AMASIA\",{{";
                                Comment += " in Amasia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == America)
                            {
                                CurrentLine += "\"CONTINENT_AMERICA\",{{";
                                Comment += " in America";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Antarctica)
                            {
                                CurrentLine += "\"CONTINENT_ANTARCTICA\",{{";
                                Comment += " in Antarctica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Arctica)
                            {
                                CurrentLine += "\"CONTINENT_ARCTICA\",{{";
                                Comment += " in Arctica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Asia)
                            {
                                CurrentLine += "\"CONTINENT_ASIA\",{{";
                                Comment += " in Asia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Asiamerica)
                            {
                                CurrentLine += "\"CONTINENT_ASIAMERICA\",{{";
                                Comment += " in Asiamerica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Atlantica)
                            {
                                CurrentLine += "\"CONTINENT_ATLANTICA\",{{";
                                Comment += " in Atlantica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Atlantis)
                            {
                                CurrentLine += "\"CONTINENT_ATLANTIS\",{{";
                                Comment += " in Atlantis";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Australia)
                            {
                                CurrentLine += "\"CONTINENT_AUSTRALIA\",{{";
                                Comment += " in Australia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Avalonia)
                            {
                                CurrentLine += "\"CONTINENT_AVALONIA\",{{";
                                Comment += " in Avalonia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Azania)
                            {
                                CurrentLine += "\"CONTINENT_AZANIA\",{{";
                                Comment += " in Azania";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Baltica)
                            {
                                CurrentLine += "\"CONTINENT_BALTICA\",{{";
                                Comment += " in Baltica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Cimmeria)
                            {
                                CurrentLine += "\"CONTINENT_CIMMERIA\",{{";
                                Comment += " in Cimmeria";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Columbia)
                            {
                                CurrentLine += "\"CONTINENT_COLUMBIA\",{{";
                                Comment += " in Columbia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == CongoCraton)
                            {
                                CurrentLine += "\"CONTINENT_CONGO_CRATON\",{{";
                                Comment += " in Congo Craton";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Euramerica)
                            {
                                CurrentLine += "\"CONTINENT_EURAMERICA\",{{";
                                Comment += " in Euramerica";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Europe)
                            {
                                CurrentLine += "\"CONTINENT_EUROPE\",{{";
                                Comment += " in Europe";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Gondwana)
                            {
                                CurrentLine += "\"CONTINENT_GONDWANA\",{{";
                                Comment += " in Gondwana";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Kalaharia)
                            {
                                CurrentLine += "\"CONTINENT_KALAHARIA\",{{";
                                Comment += " in Kalaharia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Kazakstania)
                            {
                                CurrentLine += "\"CONTINENT_KAZAKHSTANIA\",{{";
                                Comment += " in Kazakstania";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Kenorland)
                            {
                                CurrentLine += "\"CONTINENT_KERNORLAND\",{{";
                                Comment += " in Kenorland";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == KumariKandam)
                            {
                                CurrentLine += "\"CONTINENT_KUMARI_KANDAM\",{{";
                                Comment += " in Kumari Kandam";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Laurasia)
                            {
                                CurrentLine += "\"CONTINENT_LAURASIA\",{{";
                                Comment += " in Laurasia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Laurentia)
                            {
                                CurrentLine += "\"CONTINENT_LAURENTIA\",{{";
                                Comment += " in Laurentia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Lemuria)
                            {
                                CurrentLine += "\"CONTINENT_LEMURIA\",{{";
                                Comment += " in Lemuria";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Mu)
                            {
                                CurrentLine += "\"CONTINENT_MU\",{{";
                                Comment += " in Mu";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Nena)
                            {
                                CurrentLine += "\"CONTINENT_NENA\",{{";
                                Comment += " in Nena";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == NorthAmerica)
                            {
                                CurrentLine += "\"CONTINENT_NORTH_AMERICA\",{{";
                                Comment += " in North America";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Novapangaea)
                            {
                                CurrentLine += "\"CONTINENT_NOVOPANGAEA\",{{";
                                Comment += " in Novapangaea";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Nuna)
                            {
                                CurrentLine += "\"CONTINENT_NUNA\",{{";
                                Comment += " in Nuna";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Oceania)
                            {
                                CurrentLine += "\"CONTINENT_OCEANIA\",{{";
                                Comment += " in Oceania";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Pangaea)
                            {
                                CurrentLine += "\"CONTINENT_PANGAEA\",{{";
                                Comment += " in Pangaea";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == PangaeaUltima)
                            {
                                CurrentLine += "\"CONTINENT_PANGAEA_ULTIMA\",{{";
                                Comment += " in Pangaea Ultima";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Pannotia)
                            {
                                CurrentLine += "\"CONTINENT_PANNOTIA\",{{";
                                Comment += " in Pannotia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Rodinia)
                            {
                                CurrentLine += "\"CONTINENT_RODINIA\",{{";
                                Comment += " in Rodinia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Siberia)
                            {
                                CurrentLine += "\"CONTINENT_SIBERIA\",{{";
                                Comment += " in Siberia";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == SouthAmerica)
                            {
                                CurrentLine += "\"CONTINENT_SOUTH_AMERICA\",{{";
                                Comment += " in South America";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == TerraAustralis)
                            {
                                CurrentLine += "\"CONTINENT_TERRA_AUSTRALIS\",{{";
                                Comment += " in Terra Australis";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Ur)
                            {
                                CurrentLine += "\"CONTINENT_UR\",{{";
                                Comment += " in Ur";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Vaalbara)
                            {
                                CurrentLine += "\"CONTINENT_VAALBARA\",{{";
                                Comment += " in Vaalbara";
                            }
                            else if ((MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake) && MatchContinent == Vendian)
                            {
                                CurrentLine += "\"CONTINENT_VENDIAN\",{{";
                                Comment += " in Vendian";
                            }
                            else if (MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake)
                            {
                                CurrentLine += "\"CONTINENT_ZEALANDIA\",{{";
                                Comment += " in Zealandia";
                            }
                            else
                            {
                                CurrentLine += "-1,{{";
                            }
#endregion

#region Rivers
                            if (MatchRiverSW == River0)
                            {
                                CurrentLine += "1,2},{";
                                Comment += ", River to the Southwest heading Southeast";
                            }
                            else if (MatchRiverSW == River1)
                            {
                                CurrentLine += "1,5},{";
                                Comment += ", River to the Southwest heading Northwest";
                            }
                            else
                            {
                                CurrentLine += "0,-1},{";
                                Comment += "";
                            }

                            if (MatchRiverE == River0)
                            {
                                CurrentLine += "1,0},{";
                                Comment += ", River to the East heading North";
                            }
                            else if (MatchRiverE == River1)
                            {
                                CurrentLine += "1,3},{";
                                Comment += ", River to the East heading South";
                            }
                            else
                            {
                                CurrentLine += "0,-1},{";
                                Comment += "";
                            }

                            if (MatchRiverSE == River0)
                            {
                                CurrentLine += "1,1}},{";
                                Comment += ", River to the Southeast heading Northeast";
                            }
                            else if (MatchRiverSE == River1)
                            {
                                CurrentLine += "1,4}},{";
                                Comment += ", River to the Southeast heading Southwest";
                            }
                            else
                            {
                                CurrentLine += "0,-1}},{";
                            }
                            #endregion

#region Resources
                            //Bonus
                            if (MatchResource == Bananas && MatchTerrain != Ocean && MatchTerrain != Coast && MatchTerrain != Lake)
                            {
                                CurrentLine += "\"RESOURCE_BANANAS\",1},{";
                                Comment += ": Bananas";
                            }
                            else if (MatchResource == Cattle)
                            {
                                CurrentLine += "\"RESOURCE_CATTLE\",1},{";
                                Comment += ": Cattle";
                            }
                            else if (MatchResource == Copper)
                            {
                                CurrentLine += "\"RESOURCE_COPPER\",1},{";
                                Comment += ": Copper";
                            }
                            else if (MatchResource == Crabs)
                            {
                                CurrentLine += "\"RESOURCE_CRABS\",1},{";
                                Comment += ": Crabs";
                            }
                            else if (MatchResource == Deer)
                            {
                                CurrentLine += "\"RESOURCE_DEER\",1},{";
                                Comment += ": Deer";
                            }
                            else if (MatchResource == Fish)
                            {
                                CurrentLine += "\"RESOURCE_FISH\",1},{";
                                Comment += ": Fish";
                            }
                            else if (MatchResource == Rice)
                            {
                                CurrentLine += "\"RESOURCE_RICE\",1},{";
                                Comment += ": Rice";
                            }
                            else if (MatchResource == Sheep)
                            {
                                CurrentLine += "\"RESOURCE_SHEEP\",1},{";
                                Comment += ": Sheep";
                            }
                            else if (MatchResource == Stone)
                            {
                                CurrentLine += "\"RESOURCE_STONE\",1},{";
                                Comment += ": Stone";
                            }
                            else if (MatchResource == Wheat)
                            {
                                CurrentLine += "\"RESOURCE_WHEAT\",1},{";
                                Comment += ": Wheat";
                            }
                            //Luxury
                            else if (MatchResource == Citrus)
                            {
                                CurrentLine += "\"RESOURCE_CITRUS\",1},{";
                                Comment += ": Citrus";
                            }
                            else if (MatchResource == Cocoa)
                            {
                                CurrentLine += "\"RESOURCE_COCOA\",1},{";
                                Comment += ": Cocoa";
                            }
                            else if (MatchResource == Coffee)
                            {
                                CurrentLine += "\"RESOURCE_COFFEE\",1},{";
                                Comment += ": Coffee";
                            }
                            else if (MatchResource == Cotton)
                            {
                                CurrentLine += "\"RESOURCE_COTTON\",1},{";
                                Comment += ": Cotton";
                            }
                            else if (MatchResource == Diamonds)
                            {
                                CurrentLine += "\"RESOURCE_DIAMONDS\",1},{";
                                Comment += ": Diamonds";
                            }
                            else if (MatchResource == Dyes && MatchTerrain != Lake)
                            {
                                CurrentLine += "\"RESOURCE_DYES\",1},{";
                                Comment += ": Dyes";
                            }
                            else if (MatchResource == Furs)
                            {
                                CurrentLine += "\"RESOURCE_FURS\",1},{";
                                Comment += ": Furs";
                            }
                            else if (MatchResource == Gypsum)
                            {
                                CurrentLine += "\"RESOURCE_GYPSUM\",1},{";
                                Comment += ": Gypsum";
                            }
                            else if (MatchResource == Incense)
                            {
                                CurrentLine += "\"RESOURCE_INCENSE\",1},{";
                                Comment += ": Incense";
                            }
                            else if (MatchResource == Ivory)
                            {
                                CurrentLine += "\"RESOURCE_IVORY\",1},{";
                                Comment += ": Ivory";
                            }
                            else if (MatchResource == Jade)
                            {
                                CurrentLine += "\"RESOURCE_JADE\",1},{";
                                Comment += ": Jade";
                            }
                            else if (MatchResource == Marble)
                            {
                                CurrentLine += "\"RESOURCE_MARBLE\",1},{";
                                Comment += ": Marble";
                            }
                            else if (MatchResource == Mercury)
                            {
                                CurrentLine += "\"RESOURCE_MERCURY\",1},{";
                                Comment += ": Mercury";
                            }
                            else if (MatchResource == Pearls)
                            {
                                CurrentLine += "\"RESOURCE_PEARLS\",1},{";
                                Comment += ": Pearls";
                            }
                            else if (MatchResource == Salt)
                            {
                                CurrentLine += "\"RESOURCE_SALT\",1},{";
                                Comment += ": Salt";
                            }
                            else if (MatchResource == Silk)
                            {
                                CurrentLine += "\"RESOURCE_SILK\",1},{";
                                Comment += ": Silk";
                            }
                            else if (MatchResource == Silver)
                            {
                                CurrentLine += "\"RESOURCE_SILVER\",1},{";
                                Comment += ": Silver";
                            }
                            else if (MatchResource == Spices)
                            {
                                CurrentLine += "\"RESOURCE_SPICES\",1},{";
                                Comment += ": Spices";
                            }
                            else if (MatchResource == Sugar)
                            {
                                CurrentLine += "\"RESOURCE_SUGAR\",1},{";
                                Comment += ": Sugar";
                            }
                            else if (MatchResource == Tea)
                            {
                                CurrentLine += "\"RESOURCE_TEA\",1},{";
                                Comment += ": Tea";
                            }
                            else if (MatchResource == Tobacco)
                            {
                                CurrentLine += "\"RESOURCE_TOBACCO\",1},{";
                                Comment += ": Tobacco";
                            }
                            else if (MatchResource == Truffles)
                            {
                                CurrentLine += "\"RESOURCE_TRUFFLES\",1},{";
                                Comment += ": Truffles";
                            }
                            else if (MatchResource == Whales && (MatchTerrain == Ocean || MatchTerrain == Coast || MatchTerrain == Lake))
                            {
                                CurrentLine += "\"RESOURCE_WHALES\",1},{";
                                Comment += ": Whales";
                            }
                            else if (MatchResource == Wine)
                            {
                                CurrentLine += "\"RESOURCE_WINE\",1},{";
                                Comment += ": Wine";
                            }
                            //Strategic
                            else if (MatchResource == Aluminium)
                            {
                                CurrentLine += "\"RESOURCE_ALUMINUM\",1},{";
                                Comment += ": Aluminium";
                            }
                            else if (MatchResource == Coal)
                            {
                                CurrentLine += "\"RESOURCE_COAL\",1},{";
                                Comment += ": Coal";
                            }
                            else if (MatchResource == Horses)
                            {
                                CurrentLine += "\"RESOURCE_HORSES\",1},{";
                                Comment += ": Horses";
                            }
                            else if (MatchResource == Iron)
                            {
                                CurrentLine += "\"RESOURCE_IRON\",1},{";
                                Comment += ": Iron";
                            }
                            else if (MatchResource == Niter)
                            {
                                CurrentLine += "\"RESOURCE_NITER\",1},{";
                                Comment += ": Niter";
                            }
                            else if (MatchResource == Oil)
                            {
                                CurrentLine += "\"RESOURCE_OIL\",1},{";
                                Comment += ": Oil";
                            }
                            else if (MatchResource == Uranium)
                            {
                                CurrentLine += "\"RESOURCE_URANIUM\",1},{";
                                Comment += ": Uranium";
                            }
                            else if (MatchResource == AntiquitySite)
                            {
                                CurrentLine += "\"RESOURCE_ANTIQUITY_SITE\",1},{";
                                Comment += ": Antiquity Site";
                            }
                            else if (MatchResource == Shipwreck)
                            {
                                CurrentLine += "\"RESOURCE_SHIPWRECK\",1},{";
                                Comment += ": Shipwreck";
                            }
                            else if (MatchResource == Amber)
                            {
                                CurrentLine += "\"RESOURCE_AMBER\",1},{";
                                Comment += ": Amber";
                            }
                            else if (MatchResource == Olives)
                            {
                                CurrentLine += "\"RESOURCE_OLIVES\",1},{";
                                Comment += ": Olives";
                            }
                            else if (MatchResource == Turtles)
                            {
                                CurrentLine += "\"RESOURCE_TURTLES\",1},{";
                                Comment += ": Turtles";
                            }
                            else
                            {
                                CurrentLine += "-1,0},{";
                            }
#endregion

#region Cliffs

                            if (CliffsImport.Checked)
                            {
                                if (MatchRiverSW == Cliffs)
                                {
                                    CurrentLine += "1,";
                                    Comment += ", Cliffs to the Southwest";
                                }
                                else
                                {
                                    CurrentLine += "0,";
                                }
                                if (MatchRiverE == Cliffs)
                                {
                                    CurrentLine += "1,";
                                    Comment += ", Cliffs to the East";
                                }
                                else
                                {
                                    CurrentLine += "0,";
                                }
                                if (MatchRiverSE == Cliffs)
                                {
                                    CurrentLine += "1}}";
                                    Comment += ", Cliffs to the Southeast";
                                }
                                else
                                {
                                    CurrentLine += "0}}";
                                }
                            }
                            else
                            {
                                if (y % 2 == 1)
                                {
                                    if (x < MapW - 1)
                                    {
                                        if (y > 0)
                                        {
                                            if (WaterArray[x, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else if (HasHills[x, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else
                                            {
                                                CurrentLine += "0,";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (WaterArray[x + 1, y] && HasHills[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else if (HasHills[x + 1, y] && WaterArray[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[x + 1, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else if (HasHills[x + 1, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else
                                            {
                                                CurrentLine += "0}}";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0}}";
                                        }
                                    }
                                    if (x == 0)
                                    {
                                        if (y > 0)
                                        {
                                            if (WaterArray[MapW - 1, y - 1] && HasHills[MapW - 1, y])
                                            {
                                                EndCliffs += "1,";
                                            }
                                            else if (HasHills[MapW - 1, y - 1] && WaterArray[MapW - 1, y])
                                            {
                                                EndCliffs += "1,";
                                            }
                                            else
                                            {
                                                EndCliffs += "0,";
                                            }
                                        }
                                        else
                                        {
                                            EndCliffs += "0,";
                                        }

                                        if (WaterArray[0, y] && HasHills[MapW - 1, y])
                                        {
                                            EndCliffs += "1,";
                                        }
                                        else if (HasHills[0, y] && WaterArray[MapW - 1, y])
                                        {
                                            EndCliffs += "1,";
                                        }
                                        else
                                        {
                                            EndCliffs += "0,";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[0, y - 1] && HasHills[MapW - 1, y])
                                            {
                                                EndCliffs += "1}}";
                                            }
                                            else if (HasHills[0, y - 1] && WaterArray[MapW - 1, y])
                                            {
                                                EndCliffs += "1}}";
                                            }
                                            else
                                            {
                                                EndCliffs += "0}}";
                                            }
                                        }
                                        else
                                        {
                                            EndCliffs += "0}}";
                                        }
                                    }
                                }
                                else
                                {
                                    if (x < MapW - 1 && x > 0)
                                    {
                                        if (y > 0)
                                        {
                                            if (WaterArray[x - 1, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else if (HasHills[x - 1, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else
                                            {
                                                CurrentLine += "0,";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (WaterArray[x + 1, y] && HasHills[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else if (HasHills[x + 1, y] && WaterArray[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[x, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else if (HasHills[x, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else
                                            {
                                                CurrentLine += "0}}";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0}}";
                                        }
                                    }
                                    if (x == 0)
                                    {
                                        if (y > 0)
                                        {
                                            if (WaterArray[MapW - 1, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else if (HasHills[MapW - 1, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1,";
                                                Comment += ", Cliffs to the Southwest";
                                            }
                                            else
                                            {
                                                CurrentLine += "0,";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[MapW - 2, y - 1] && HasHills[MapW - 1, y])
                                            {
                                                EndCliffs += "1,";
                                            }
                                            else if (HasHills[MapW - 2, y - 1] && WaterArray[MapW - 1, y])
                                            {
                                                EndCliffs += "1,";
                                            }
                                            else
                                            {
                                                EndCliffs += "0,";
                                            }
                                        }
                                        else
                                        {
                                            EndCliffs += "0,";
                                        }

                                        if (WaterArray[x + 1, y] && HasHills[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else if (HasHills[x + 1, y] && WaterArray[x, y])
                                        {
                                            CurrentLine += "1,";
                                            Comment += ", Cliffs to the East";
                                        }
                                        else
                                        {
                                            CurrentLine += "0,";
                                        }

                                        if (WaterArray[0, y] && HasHills[MapW - 1, y])
                                        {
                                            EndCliffs += "1,";
                                        }
                                        else if (HasHills[0, y] && WaterArray[MapW - 1, y])
                                        {
                                            EndCliffs += "1,";
                                        }
                                        else
                                        {
                                            EndCliffs += "0,";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[x, y - 1] && HasHills[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else if (HasHills[x, y - 1] && WaterArray[x, y])
                                            {
                                                CurrentLine += "1}}";
                                                Comment += ", Cliffs to the Southeast";
                                            }
                                            else
                                            {
                                                CurrentLine += "0}}";
                                            }
                                        }
                                        else
                                        {
                                            CurrentLine += "0}}";
                                        }

                                        if (y > 0)
                                        {
                                            if (WaterArray[MapW - 1, y - 1] && HasHills[MapW - 1, y])
                                            {
                                                EndCliffs += "1}}";
                                            }
                                            else if (HasHills[MapW - 1, y - 1] && WaterArray[MapW - 1, y])
                                            {
                                                EndCliffs += "1}}";
                                            }
                                            else
                                            {
                                                EndCliffs += "0}}";
                                            }
                                        }
                                        else
                                        {
                                            EndCliffs += "0}}";
                                        }
                                    }
                                }
                            }
#endregion
                            if (x == 0 && !CliffsImport.Checked)
                            {
                                InvertedLines = CurrentLine + InvertedLines + EndCliffs + "\n";
                            }
                            else
                            {
                                //InvertedLines = CurrentLine + Comment + "\n" + InvertedLines;
                                InvertedLines = "\n" + CurrentLine + InvertedLines;
                            }
                            //LuaGenMap += CurrentLine + Comment + "\n";
                            //CurrentLine = "", Comment = "";
                        }
                        NatWond += NatWondTemp;
                        NatWondTemp = "";
                        LuaGenMap += InvertedLines;
                        //InvertedLines = "";
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
                    int GridSize = 0;
                    bool FirstLine = true, FirstRow = true, LastColumn = true;
                    string LuaTemp = "", LuaCliffsEnd = "";
                    while ((LuaLine = file.ReadLine()) != null)
                    {
                        string[] Parts = LuaLine.Split(' ');
                        string StartWord = "MapToConvert";
                        try
                        {
#region civ6
                            if ((Parts[0] == "YnAMP_InGame:" || Parts[0] == "WorldBuilderPlacement:") && !Civ5)
                            {
                                Civ6 = true;
                                if (StartWord == Parts[1].Substring(0, StartWord.Length))
                                {
                                    string MapArray = Parts[1];
                                    int Position = 0;
                                    int CoordX = 0, CoordY = 0, Feature = 0;
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
                                        /*
                                        if (Position == 2 && foo >= 0 && foo <= 9)
                                        {
                                            ReachedNum = true;
                                            Console.WriteLine("Terrain " + foo);
                                            //Skips the Terrain Type value
                                        }
                                        else if (Position == 2 && ReachedNum)
                                        {
                                            Position = 3;
                                            ReachedNum = false;
                                        }
                                        if (Position == 3 && c == '-')
                                        {
                                            Position = 4;
                                            ReachedNum = false;
                                            Console.WriteLine("Empty Feature " + c);
                                            Feature = -1;
                                            //Skips empty features
                                        }
                                        if (Position == 3 && foo >= 0 && foo <= 9)
                                        {
                                            ReachedNum = true;
                                            Feature = Feature * 10 + foo;
                                            Console.WriteLine("Feature " + foo);
                                        }
                                        else if (Position == 3 && ReachedNum)
                                        {
                                            Position = 4;
                                            ReachedNum = false;
                                        }*/
                                    }
                                    MapH = CoordY + 1;
                                    const string V = "\"FEATURE_.+?\"";
                                    const string T = "\"TERRAIN_.+?\"";
                                    const string H = "\"TERRAIN_.+?HILLS\"";
                                    const string W = "\"TERRAIN_(OCEAN|COAST)\"";
                                    const string C = "(0|1),(0|1),(0|1)}}";
                                    Match match = Regex.Match(MapArray, @V);
                                    Match terr = Regex.Match(MapArray, @T);
                                    Match hills = Regex.Match(MapArray, @H);
                                    Match water = Regex.Match(MapArray, @W);
                                    Match cliffs = Regex.Match(MapArray, @C);
                                    if (match.Success && terr.Success)
                                    {
                                        if (Civ6Wonder(match.Groups[0].Value) != null)
                                        {
                                            NatWondTemp = "\n\t\t<Replace MapName=\"" + ProjectName + "_Map\" X = \"" + CoordX + "\" Y = \"" + CoordY + "\" FeatureType = " + match.Groups[0].Value + " TerrainType = " + terr.Groups[0].Value + " />" + NatWondTemp;
                                            /*LuaEndWrap += "\n\tif GameInfo.Features[" + match.Groups[0].Value + "] then\n\t\tNaturalWonders[GameInfo.Features[" + match.Groups[0].Value + "].Index] = " +
                                                            "{ X = " + CoordX + ", Y = " + CoordY + "}\n\tend";*/
                                            MapArray = MapArray.Replace(match.Groups[0].Value, "-1");
                                        }
                                    }

                                    if (LastColumn) MapW = CoordX + 1;

                                    if (hills.Success) HasHills[CoordX, CoordY] = true;
                                    else if (water.Success) WaterArray[CoordX, CoordY] = true;
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
                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[CoordX, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else if (HasHills[CoordX, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0,";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (WaterArray[CoordX + 1, CoordY] && HasHills[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else if (HasHills[CoordX + 1, CoordY] && WaterArray[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[CoordX + 1, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else if (HasHills[CoordX + 1, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0}}";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0}}";
                                                    }
                                                }
                                                if (CoordX == 0)
                                                {
                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[MapW - 1, CoordY - 1] && HasHills[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1,";
                                                        }
                                                        else if (HasHills[MapW - 1, CoordY - 1] && WaterArray[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1,";
                                                        }
                                                        else
                                                        {
                                                            LuaCliffsEnd += "0,";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0,";
                                                    }

                                                    if (WaterArray[0, CoordY] && HasHills[MapW - 1, CoordY])
                                                    {
                                                        LuaCliffsEnd += "1,";
                                                    }
                                                    else if (HasHills[0, CoordY] && WaterArray[MapW - 1, CoordY])
                                                    {
                                                        LuaCliffsEnd += "1,";
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0,";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[0, CoordY - 1] && HasHills[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1}}";
                                                        }
                                                        else if (HasHills[0, CoordY - 1] && WaterArray[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1}}";
                                                        }
                                                        else
                                                        {
                                                            LuaCliffsEnd += "0}}";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0}}";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (CoordX < MapW - 1 && CoordX > 0)
                                                {
                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[CoordX - 1, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else if (HasHills[CoordX - 1, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0,";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (WaterArray[CoordX + 1, CoordY] && HasHills[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else if (HasHills[CoordX + 1, CoordY] && WaterArray[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[CoordX, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else if (HasHills[CoordX, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0}}";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0}}";
                                                    }
                                                }
                                                if (CoordX == 0)
                                                {
                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[MapW - 1, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else if (HasHills[MapW - 1, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1,";
                                                            //Comment += ", Cliffs to the Southwest";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0,";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[MapW - 2, CoordY - 1] && HasHills[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1,";
                                                        }
                                                        else if (HasHills[MapW - 2, CoordY - 1] && WaterArray[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1,";
                                                        }
                                                        else
                                                        {
                                                            LuaCliffsEnd += "0,";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0,";
                                                    }

                                                    if (WaterArray[CoordX + 1, CoordY] && HasHills[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else if (HasHills[CoordX + 1, CoordY] && WaterArray[CoordX, CoordY])
                                                    {
                                                        MapArray += "1,";
                                                        //Comment += ", Cliffs to the East";
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0,";
                                                    }

                                                    if (WaterArray[0, CoordY] && HasHills[MapW - 1, CoordY])
                                                    {
                                                        LuaCliffsEnd += "1,";
                                                    }
                                                    else if (HasHills[0, CoordY] && WaterArray[MapW - 1, CoordY])
                                                    {
                                                        LuaCliffsEnd += "1,";
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0,";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[CoordX, CoordY - 1] && HasHills[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else if (HasHills[CoordX, CoordY - 1] && WaterArray[CoordX, CoordY])
                                                        {
                                                            MapArray += "1}}";
                                                            //Comment += ", Cliffs to the Southeast";
                                                        }
                                                        else
                                                        {
                                                            MapArray += "0}}";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MapArray += "0}}";
                                                    }

                                                    if (CoordY > 0)
                                                    {
                                                        if (WaterArray[MapW - 1, CoordY - 1] && HasHills[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1}}";
                                                        }
                                                        else if (HasHills[MapW - 1, CoordY - 1] && WaterArray[MapW - 1, CoordY])
                                                        {
                                                            LuaCliffsEnd += "1}}";
                                                        }
                                                        else
                                                        {
                                                            LuaCliffsEnd += "0}}";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        LuaCliffsEnd += "0}}";
                                                    }
                                                }
                                            }
                                        }
#endregion
                                    }


                                    LuaTemp = "\n" + MapArray + LuaTemp;
                                    LastColumn = false;
                                    if (CoordX == 0)
                                    {
                                        NatWond += NatWondTemp;
                                        NatWondTemp = "";
                                        LuaGenMap += LuaTemp + LuaCliffsEnd;
                                        LuaTemp = "";
                                        LastColumn = true;
                                        if (CoordY == 0) FirstRow = false;
                                    }
                                    /*if (MapArray.Contains("FEATURE_"))
                                    if (Civ6Wonder(Feature) != null)*/
                                    /*if ((CoordX + 1) * MapH >= GridSize)
                                    {
                                        GridSize = (CoordX + 1) * MapH;
                                        MapW = GridSize / MapH;
                                    }*/

                                }
                            }
#endregion

#region civ 5
                            else if (Parts[1] == "InGame:" && !Civ6)
                            {
                                Civ5 = true;
                                if (StartWord == Parts[2].Substring(0, StartWord.Length))
                                {
                                    int NumberPosition = 13, Multiplier = 1;
                                    int[] IntArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                    string ReversedString = Reverse(Parts[2]);
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
                                    int[] MTCArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                    MTCArray[0] = IntArray[0];
                                    MTCArray[1] = IntArray[1];
                                    MTCArray[3] = Civ6Feature(IntArray[4]);
                                    MTCArray[4] = Civ6Contient(IntArray[5]);
                                    MTCArray[5] = IntArray[6];
                                    MTCArray[6] = IntArray[7];
                                    MTCArray[7] = IntArray[8];
                                    MTCArray[8] = IntArray[9];
                                    MTCArray[9] = IntArray[10];
                                    MTCArray[10] = IntArray[11];
                                    MTCArray[11] = Civ6Resource(IntArray[12]);
                                    MTCArray[12] = IntArray[13];
                                    if (IntArray[2] < 5)
                                    {
                                        MTCArray[2] = IntArray[2] * 3 + 2 - IntArray[3];
                                    }
                                    else if (IntArray[2] > 4)
                                    {
                                        MTCArray[2] = IntArray[2] + 10;
                                    }

                                    /*if (MTCArray[0] == 0 && !Civ5Lines.Any())
                                    {
                                        var array = Civ5Lines.ToArray();
                                        for (int i = Civ5Lines.Count; i > -1; i--)
                                        {
                                            Civ5LinesY.Add(array[i]);
                                        }
                                        Civ5Lines.Clear();
                                    }*/

                                    LuaGenMap += "MapToConvert[" + MTCArray[0] + "][" + MTCArray[1] + "]={" + MTCArray[2] + "," + MTCArray[3] + "," + MTCArray[4] + ",{{" + MTCArray[5] + "," + MTCArray[6] +
                                        "},{" + MTCArray[7] + "," + MTCArray[8] + "},{" + MTCArray[9] + "," + MTCArray[10] + "}},{" + MTCArray[11] + "," + MTCArray[12] + "},{0,0,0}}\n";
                                    /*Civ5Lines.Add("MapToConvert[" + MTCArray[0] + "][" + MTCArray[1] + "]={" + MTCArray[2] + "," + MTCArray[3] + "," + MTCArray[4] + ",{{" + MTCArray[5] + "," + MTCArray[6] +
                                        "},{" + MTCArray[7] + "," + MTCArray[8] + "},{" + MTCArray[9] + "," + MTCArray[10] + "}},{" + MTCArray[11] + "," + MTCArray[12] + "},{");*/
                                    MapH = MTCArray[1] + 1;
                                    MapW = MTCArray[0] + 1;
                                    if (Civ5Wonder(MTCArray[3]) != null)
                                    {
                                        LuaEndWrap += "\n\tif GameInfo.Features[\"" + Civ5Wonder(MTCArray[3]) + "\"] then\n\t\tNaturalWonders[GameInfo.Features[\"" + Civ5Wonder(MTCArray[3]) + "\"].Index] = " +
                                                            "{ X = " + MTCArray[0] + ", Y = " + MTCArray[1] + "}\n\tend";
                                    }
                                }
                            }
#endregion
                            else if (Civ6 || Civ5)
                            {
                                //Console.WriteLine(Civ5LinesY);
                                // write shit here
                                break;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                    if (Civ5)
                    {
                        Console.WriteLine("This is where the magic happens");
                        //Civ5LinesY.ForEach(i => Console.WriteLine("{0}", i));
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
#endregion

#region Config

#region Config Values
                if (STDRules.Checked)
                {
                    ConfigMap += "<Row File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                }
                if (RNFRules.Checked)
                {
                    ConfigMap += "<Row Domain=\"Maps:Expansion1Maps\" File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                }

#region Rivers
                if (OneSelected(RiversGenerate.Checked, RiversImport.Checked, RiversEmpty.Checked))
                {
                    ConfigParameters += ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT", DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 0, 231);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT", DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 1, 231);
                }
#endregion

#region Continents
                if (ContinentsGenerate.Checked && !ContinentsImport.Checked || !ContinentsGenerate.Checked && ContinentsImport.Checked)
                {
                    ConfigParameters += ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT", DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 0, 232);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT", DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 1, 232);
                }
#endregion

#region Natural Wonders
                if (OneSelected(WondersGenerate.Checked, WondersImport.Checked, WondersEmpty.Checked))
                {
                    ConfigParameters += ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT", DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 0, 244);
                }
                else
                {
                    ConfigParameters += ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT", DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 1, 244);
                }
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

                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapSize\" Name=\"LOC_MAP_SIZE\" Description=\"\" Domain=\"StandardMapSizes\" DefaultValue=\"" + SizeOfMap(MapW, MapH) + "\" ConfigurationGroup=\"Map\" ConfigurationId=\"MAP_SIZE\" GroupId=\"MapOptions\" Hash=\"1\" Visible=\"0\" SortIndex=\"225\"/>\n";
                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"HideSize\" Name=\"HideSize\" Description=\"\" Domain=\"bool\" DefaultValue=\"1\" ConfigurationGroup=\"Map\" ConfigurationId=\"HideSize\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";
                ConfigParameters += "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapName\" Name=\"MapName\" Description=\"\" Domain=\"text\" DefaultValue=\"" + ProjectName + "_Map\" ConfigurationGroup=\"Map\" ConfigurationId=\"MapName\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";

                //MapSupportedValues
#region Rivers
                if (!RiversGenerate.Checked || !RiversImport.Checked || !RiversEmpty.Checked)
                {
                    if (RiversGenerate.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (RiversImport.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (RiversEmpty.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Continents
                if (!ContinentsGenerate.Checked || !ContinentsImport.Checked)
                {
                    if (ContinentsGenerate.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ContinentsPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (ContinentsImport.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ContinentsPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                }
#endregion

#region Wonders
                if (!WondersGenerate.Checked || !WondersImport.Checked || !WondersEmpty.Checked)
                {
                    if (WondersGenerate.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (WondersImport.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (WondersEmpty.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Features
                if (!FeaturesGenerate.Checked || !FeaturesImport.Checked || !FeaturesEmpty.Checked)
                {
                    if (FeaturesGenerate.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (FeaturesImport.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (FeaturesEmpty.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Resources
                if (!ResourcesGenerate.Checked || !ResourcesImport.Checked || !ResourcesEmpty.Checked)
                {
                    if (ResourcesGenerate.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (ResourcesImport.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (ResourcesEmpty.Checked)
                    {
                        ConfigMapSupport += "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
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

#region TSL
                string TSLText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameData>\n\t<!-- You have to fill this in manually -->\n\t<StartPosition>\n\t\t<!--<Replace MapName=\"" + ProjectName + "_Map\" Civilization =\"CIVILIZATION_AMERICA\"	X=\"0\" Y=\"0\" />-->" +
                                    "\n\t</StartPosition>\n</GameData>";
                if (!File.Exists(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml"))
                {
                    File.Create(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml").Dispose();
                    System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml", TSLText);
                }
#endregion

#region Mod Info

                string ModInfo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Mod id=\"" + ModID + "\" version = \"1\">\n\t<Properties>\n\t\t<Name>" + AuthorText.Text + " - " + ProjectText.Text + "</Name>" +
                                    "\n\t\t<Description>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Civ 6 Map Maker</Description>" +
                                    "\n\t\t<Teaser>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Civ 6 Map Maker</Teaser>\n\t\t<Authors>" + AuthorText.Text + "</Authors>\n\t</Properties>" +
                                    "\n\t<Dependencies>\n\t\t<Mod id=\"36e88483-48fe-4545-b85f-bafc50dde315\" title=\"Yet (not) Another Maps Pack\"/>\n\t</Dependencies>" +
                                    "\n\t<FrontEndActions>\n\t\t<UpdateDatabase id=\"" + ProjectName + "_SETTING\">\n\t\t\t<File>Config/Config.xml</File>\n\t\t</UpdateDatabase>" +
                                    "\n\t\t<UpdateText id=\"NewAction\">\n\t\t\t<File>Config/Config_Text.xml</File>\n\t\t</UpdateText>\n\t</FrontEndActions>" +
                                    "\n\t<InGameActions>\n\t\t<ImportFiles id=\"" + ProjectName + "_IMPORT\">\n\t\t\t<File>Lua/" + ProjectName + "_Map.lua</File>\n\t\t</ImportFiles>" +
                                    "\n\t\t<UpdateDatabase id=\"NewAction\">\n\t\t\t<File>Map/Map.xml</File>\n\t\t\t<File>Map/NaturalWonders.xml</File>\n\t\t</UpdateDatabase>\n\t</InGameActions>" +
                                    "\n\t<Files>\n\t\t<File>Config/Config.xml</File>\n\t\t<File>Config/Config_Text.xml</File>\n\t\t<File>Map/Map.xml</File>\n\t\t<File>Map/NaturalWonders.xml</File>\n\t\t<File>Lua/" + ProjectName + "_Map.lua</File>\n\t</Files>\n</Mod>";

                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\" + ProjectName + ".modinfo", ModInfo);
                Application.Restart();
#endregion
            }
        }

#region Methods
        public string ParameterRow(string ProjectName, string ParameterID, string ParameterName, string DefaultValue, string ConfigurationID, int Hash, int Visible, int SortIndex)
        {
            if (Visible == 1 && Hash == 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" + ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" + ConfigurationID + "\"	GroupId=\"MapOptions\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 1 && Hash < 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" + ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" + ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 0 && Hash == 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" + ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" + ConfigurationID + "\"	GroupId=\"MapOptions\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            else if (Visible == 0 && Hash < 2) return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" + ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" + ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
            return "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ParameterID + "\" Name=\"LOC_MAP_" + ParameterName + "_NAME\" Description=\"LOC_MAP_" + ParameterName + "_DESCRIPTION\" Domain=\"" + ParameterID + "\" DefaultValue=\"" + DefaultValue + "\" ConfigurationGroup=\"Map\"	ConfigurationId=\"" + ConfigurationID + "\"	GroupId=\"MapOptions\" Hash=\"" + Hash + "\" Visible=\"" + Visible + "\" SortIndex=\"" + SortIndex + "\"/>\n";
        }

        public bool OneSelected(bool first, bool second, bool third)
        {
            if (first && !second && !third || !first && second && !third || !first && !second && third)
            {
                return true;
            }
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

        public int Civ6Feature(int f)
        {
            if (f == 0) return 1;
            if (f == 1) return 2;
            if (f == 2) return 5;
            if (f == 3) return 4;
            if (f == 4) return 0;
            if (f == 5) return 3;
            if (f == 17) return 6;
            return f;
        }

        public string Civ6Wonder(string w)
        {
            if (w == "\"FEATURE_BARRIER_REEF\"" || w == "\"FEATURE_CLIFFS_DOVER\"" || w == "\"FEATURE_CRATER_LAKE\"" ||
                w == "\"FEATURE_DEAD_SEA\"" || w == "\"FEATURE_EVEREST\"" || w == "\"FEATURE_GALAPAGOS\"" || w == "\"FEATURE_KILIMANJARO\"" ||
                w == "\"FEATURE_PANTANAL\"" || w == "\"FEATURE_PIOPIOTAHI\"" || w == "\"FEATURE_TORRES_DEL_PAINE\"" ||
                w == "\"FEATURE_TSINGY\"" || w == "\"FEATURE_YOSEMITE\"" || w == "\"FEATURE_DELICATE_ARCH\"" || w == "\"FEATURE_EYE_OF_THE_SAHARA\"" || 
                w == "\"FEATURE_LAKE_RETBA\"" || w == "\"FEATURE_MATTERHORN\"" || w == "\"FEATURE_RORAIMA\"" || 
                w == "\"FEATURE_UBSUNUR_HOLLOW\"" || w == "\"FEATURE_ZHANGYE_DANXIA\"" || w == "\"FEATURE_HA_LONG_BAY\"" || w == "\"FEATURE_EYJAFJALLAJOKULL\"" || 
                w == "\"FEATURE_LYSEFJORDEN\"" || w == "\"FEATURE_GIANTS_CAUSEWAY\"" || w == "\"FEATURE_ULURU\"") return w;
            return null;
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
        }

        public int Civ6Contient(int c)
        {
            if (c == 0) return -1;
            if (c == 1) return 2;
            if (c == 2) return 5;
            if (c == 3) return 0;
            if (c == 4) return 17;
            return 42;
        }

        public int Civ6Resource(int r)
        {
            if (r == 0) return 43;
            if (r == 1) return 42;
            if (r == 2) return 41;
            if (r == 3) return 45;
            if (r == 4) return 40;
            if (r == 5) return 46;
            if (r == 6) return 9;
            if (r == 7) return 1;
            if (r == 8) return 7;
            if (r == 9) return 4;
            if (r == 10) return 0;
            if (r == 11) return 5;
            if (r == 12) return 8;
            if (r == 13) return 32;
            if (r == 14) return 23;
            if (r == 15) return 44; //Gold -> Niter
            if (r == 16) return 26;
            if (r == 17) return 14; //Diamonds
            if (r == 18) return 21;
            if (r == 19) return 19;
            if (r == 20) return 16;
            if (r == 21) return 15;
            if (r == 22) return 27;
            if (r == 23) return 25;
            if (r == 24) return 28;
            if (r == 25) return 13;
            if (r == 26) return 33;
            if (r == 27) return 18;
            if (r == 28) return 14; //Jewelry
            if (r == 29) return 14; //Porcelain
            if (r == 30) return 2;
            if (r == 31) return 24;
            if (r == 32) return 3;
            if (r == 33) return 31;
            if (r == 34) return 10;
            if (r == 40) return 16; //Bison to Furs
            if (r == 41) return 11;
            return -1;
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            GenerateMap.Enabled = true;
            timer.Stop();
        }
#endregion
    }
}

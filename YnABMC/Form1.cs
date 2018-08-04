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

namespace YnABMC
{
    public partial class Form1 : Form
    {
        string FolderPath = "", BmpFilePath = "", ProjectName = "", AuthorName = "", ModID = "";
#region MapValues

#region Terrain
        string Grass = "(32,192,64)", Plains = "(192,224,0)", Desert = "(224,128,0)", Tundra = "(192,220,192)",
               Snow = "(255,255,255)", Coast = "(0,160,192)", Ocean = "(64,64,192)";
#endregion

#region feratures and plots
        string Mountain = "(128,128,128)", Hills = "(192,192,192)";

        string Ice = "(255,255,255)", Jungle = "(224,192,0)", Marsh = "(0,160,192)", Oasis = "(0,160,192)",
               FloodPlains = "(192,224,0)", Woods = "(192,128,64)", Reef = "(224,192,0)"; //, Fallout = "(224,160,192)";

        string BarrierReef = "(255,0,0)", CliffsDover = "(224,32,0)", CraterLake = "(192,32,0)", DeadSea = "(160,32,0)",
               Everest = "(128,0,0)", Galapagos = "(64,32,0)", Kilimanjaro = "(0,0,0)", Pantanal = "(0,128,128)",

               Piopiotahi = "(0,255,0)", TorresDelPaine = "(0,224,0)", Tsingy = "(0,192,0)", Yosemite = "(0,160,0)",
               Uluru = "(0,128,0)", HaLongBay = "(0,96,0)", Eyjafjallajokull = "(0,64,0)", Lysefjorden = "(0,96,128)",

               GiantsCauseway = "(0,0,255)", DelicateArch = "(0,0,192)", EyeOfTheSahara = "(0,0,128)", LakeRetba = "(0,0,64)",
               Matterhorn = "(32,0,64)", Roraima = "(64,64,64)", UbsunurHollow = "(192,128,64)", ZhangyeDanxia = "(128,128,128)";
#endregion

        string River0 = "(64,64,192)", River1 = "(160,64,192)", Cliffs = "(192,128,64)";
#region continents
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

#region resources
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
        string LuaFile = "",
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
        string Config = "",
                ConfigMap = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<GameInfo>\n\t<Maps>\n",
                ConfigParameters = "\t</Maps>\n\t<Parameters>\n",
                ConfigMapSupport = "\t</Parameters>\n\t<MapSupportedValues>\n",
                ConfigEnd = "\t</MapSupportedValues>\n</GameInfo>";
#endregion

#endregion

        bool  PathFound = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void SelectSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFDSource = new OpenFileDialog();
            OFDSource.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            OFDSource.FilterIndex = 2;
            if (OFDSource.ShowDialog() == DialogResult.OK)
            {
                FolderPath = Path.GetDirectoryName(OFDSource.FileName);
                BmpFilePath = OFDSource.FileName;
                PathFound = true;
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
#endregion

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private void GenerateMap_Click(object sender, EventArgs e)
        {
            if (PathFound && ProjectName.Length > 0)
            {
                timer.Interval = 5000;
                timer.Tick += Timer_Tick;
                timer.Start();
                GenerateMap.Enabled = false;
#region LUA
                Bitmap Map = new Bitmap(BmpFilePath);

                int MapH = (Map.Height - 3) / 5;
                int MapW = (Map.Width - 4) / 6;
                int Center = 3, OffsetX = 6, OffsetY = 5;
                string[] Output = new string[MapH * MapW];
                int Line = 0;

                for (int y = 0; y < MapH; y++)
                {
                    int MapY = Map.Height - ((y * OffsetY) + Center) - 1;
                    for (int x = 0; x < MapW; x++)
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
                                CurrentLine = CurrentLine + "2,";
                                Comment = Comment + "Grassland (Mountain)";
                            }
                            else if (MatchHills == Hills)
                            {
                                CurrentLine = CurrentLine + "1,";
                                Comment = Comment + "Grassland Hills";
                            }
                            else
                            {
                                CurrentLine = CurrentLine + "0,";
                                Comment = Comment + "Grassland";
                            }
                        }
                        else if (MatchTerrain == Plains)
                        {
                            if (MatchPlotFeature == Mountain)
                            {
                                CurrentLine = CurrentLine + "5,";
                                Comment = Comment + "Plains (Mountain)";
                            }
                            else if (MatchHills == Hills)
                            {
                                CurrentLine = CurrentLine + "4,";
                                Comment = Comment + "Plains Hills";
                            }
                            else
                            {
                                CurrentLine = CurrentLine + "3,";
                                Comment = Comment + "Plains";
                            }
                        }
                        else if (MatchTerrain == Desert)
                        {
                            if (MatchPlotFeature == Mountain)
                            {
                                CurrentLine = CurrentLine + "8,";
                                Comment = Comment + "Desert (Mountain)";
                            }
                            else if (MatchHills == Hills)
                            {
                                CurrentLine = CurrentLine + "7,";
                                Comment = Comment + "Desert Hills";
                            }
                            else
                            {
                                CurrentLine = CurrentLine + "6,";
                                Comment = Comment + "Desert";
                            }
                        }
                        else if (MatchTerrain == Tundra)
                        {
                            if (MatchPlotFeature == Mountain)
                            {
                                CurrentLine = CurrentLine + "11,";
                                Comment = Comment + "Tundra (Mountain)";
                            }
                            else if (MatchHills == Hills)
                            {
                                CurrentLine = CurrentLine + "10,";
                                Comment = Comment + "Tundra Hills";
                            }
                            else
                            {
                                CurrentLine = CurrentLine + "9,";
                                Comment = Comment + "Tundra";
                            }
                        }
                        else if (MatchTerrain == Snow)
                        {
                            if (MatchPlotFeature == Mountain)
                            {
                                CurrentLine = CurrentLine + "14,";
                                Comment = Comment + "Snow (Mountain)";
                            }
                            else if (MatchHills == Hills)
                            {
                                CurrentLine = CurrentLine + "13,";
                                Comment = Comment + "Snow Hills";
                            }
                            else
                            {
                                CurrentLine = CurrentLine + "12,";
                                Comment = Comment + "Snow";
                            }
                        }
                        else if (MatchTerrain == Coast)
                        {
                            CurrentLine = CurrentLine + "15,";
                            Comment = Comment + "Coast";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "16,";
                            Comment = Comment + "Ocean";
                        }
#endregion

#region Natural Wonders
                        if (MatchWonder == BarrierReef)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_BARRIER_REEF\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == CliffsDover)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_CLIFFS_DOVER\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == CraterLake)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_CRATER_LAKE\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == DeadSea)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_DEAD_SEA\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Everest)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_EVEREST\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Galapagos)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_GALAPAGOS\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Kilimanjaro)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_KILIMANJARO\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Pantanal)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_PANTANAL\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Piopiotahi)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_PIOPIOTAHI\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == TorresDelPaine)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_TORRES_DEL_PAINE\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Tsingy)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_TSINGY\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Yosemite)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tNaturalWonders[GameInfo.Features[\"FEATURE_YOSEMITE\"].Index] = { X = " + x + ", Y = " + y + "}";
                        }
                        else if (MatchWonder == Uluru)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_ULURU\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_ULURU\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == HaLongBay)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_HA_LONG_BAY\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_HA_LONG_BAY\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == Eyjafjallajokull)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_EYJAFJALLAJOKULL\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_EYJAFJALLAJOKULL\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == Lysefjorden)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_LYSEFJORDEN\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_LYSEFJORDEN\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == GiantsCauseway)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_GIANTS_CAUSEWAY\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_GIANTS_CAUSEWAY\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == DelicateArch)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_DELICATE_ARCH\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_DELICATE_ARCH\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == EyeOfTheSahara)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_EYE_OF_THE_SAHARA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_EYE_OF_THE_SAHARA\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == LakeRetba)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_LAKE_RETBA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_LAKE_RETBA\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == Matterhorn)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_MATTERHORN\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_MATTERHORN\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == Roraima)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_RORAIMA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_RORAIMA\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == UbsunurHollow)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_UBSUNUR_HOLLOW\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_UBSUNUR_HOLLOW\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
                        else if (MatchWonder == ZhangyeDanxia)
                        {
                            LuaEndWrap = LuaEndWrap + "\n\tif GameInfo.Features[\"FEATURE_ZHANGYE_DANXIA\"] then\n\t\tNaturalWonders[GameInfo.Features[\"FEATURE_ZHANGYE_DANXIA\"].Index] = " +
                                                "{ X = " + x + ", Y = " + y + "}\n\tend";
                        }
#endregion

#region Features
                        if (MatchPlotFeature == FloodPlains && MatchTerrain == Desert)
                        {
                            CurrentLine = CurrentLine + "0,";
                            Comment = Comment + " with Flood Plains";
                        }
                        else if (MatchPlotFeature == Ice && (MatchTerrain == Ocean || MatchTerrain == Coast))
                        {
                            CurrentLine = CurrentLine + "1,";
                            Comment = Comment + " with Ice";
                        }
                        else if (MatchPlotFeature == Jungle && MatchTerrain != Coast)
                        {
                            CurrentLine = CurrentLine + "2,";
                            Comment = Comment + " with Jungle";
                        }
                        else if (MatchPlotFeature == Woods)
                        {
                            CurrentLine = CurrentLine + "3,";
                            Comment = Comment + " with Woods";
                        }
                        else if (MatchPlotFeature == Oasis && MatchTerrain == Desert)
                        {
                            CurrentLine = CurrentLine + "4,";
                            Comment = Comment + " with Oasis";
                        }
                        else if (MatchPlotFeature == Marsh && MatchTerrain == Grass)
                        {
                            CurrentLine = CurrentLine + "5,";
                            Comment = Comment + " with Marsh";
                        }
                        else if (MatchPlotFeature == Reef && MatchTerrain == Coast)
                        {
                            CurrentLine = CurrentLine + "18,";
                            Comment = Comment + " with Reef";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "-1,";
                        }
#endregion

#region Continent
                        if ((MatchTerrain != Ocean || MatchTerrain != Coast) && MatchContinent == Africa)
                        {
                            CurrentLine = CurrentLine + "0,{{";
                            Comment = Comment + " in Africa";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Amasia)
                        {
                            CurrentLine = CurrentLine + "1,{{";
                            Comment = Comment + " in Amasia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == America)
                        {
                            CurrentLine = CurrentLine + "2,{{";
                            Comment = Comment + " in America";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Antarctica)
                        {
                            CurrentLine = CurrentLine + "3,{{";
                            Comment = Comment + " in Antarctica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Arctica)
                        {
                            CurrentLine = CurrentLine + "4,{{";
                            Comment = Comment + " in Arctica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Asia)
                        {
                            CurrentLine = CurrentLine + "5,{{";
                            Comment = Comment + " in Asia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Asiamerica)
                        {
                            CurrentLine = CurrentLine + "6,{{";
                            Comment = Comment + " in Asiamerica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Atlantica)
                        {
                            CurrentLine = CurrentLine + "7,{{";
                            Comment = Comment + " in Atlantica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Atlantis)
                        {
                            CurrentLine = CurrentLine + "8,{{";
                            Comment = Comment + " in Atlantis";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Australia)
                        {
                            CurrentLine = CurrentLine + "9,{{";
                            Comment = Comment + " in Australia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Avalonia)
                        {
                            CurrentLine = CurrentLine + "10,{{";
                            Comment = Comment + " in Avalonia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Azania)
                        {
                            CurrentLine = CurrentLine + "11,{{";
                            Comment = Comment + " in Azania";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Baltica)
                        {
                            CurrentLine = CurrentLine + "12,{{";
                            Comment = Comment + " in Baltica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Cimmeria)
                        {
                            CurrentLine = CurrentLine + "13,{{";
                            Comment = Comment + " in Cimmeria";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Columbia)
                        {
                            CurrentLine = CurrentLine + "14,{{";
                            Comment = Comment + " in Columbia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == CongoCraton)
                        {
                            CurrentLine = CurrentLine + "15,{{";
                            Comment = Comment + " in Congo Craton";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Euramerica)
                        {
                            CurrentLine = CurrentLine + "16,{{";
                            Comment = Comment + " in Euramerica";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Europe)
                        {
                            CurrentLine = CurrentLine + "17,{{";
                            Comment = Comment + " in Europe";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Gondwana)
                        {
                            CurrentLine = CurrentLine + "18,{{";
                            Comment = Comment + " in Gondwana";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Kalaharia)
                        {
                            CurrentLine = CurrentLine + "19,{{";
                            Comment = Comment + " in Kalaharia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Kazakstania)
                        {
                            CurrentLine = CurrentLine + "20,{{";
                            Comment = Comment + " in Kazakstania";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Kenorland)
                        {
                            CurrentLine = CurrentLine + "21,{{";
                            Comment = Comment + " in Kenorland";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == KumariKandam)
                        {
                            CurrentLine = CurrentLine + "22,{{";
                            Comment = Comment + " in Kumari Kandam";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Laurasia)
                        {
                            CurrentLine = CurrentLine + "23,{{";
                            Comment = Comment + " in Laurasia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Laurentia)
                        {
                            CurrentLine = CurrentLine + "24,{{";
                            Comment = Comment + " in Laurentia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Lemuria)
                        {
                            CurrentLine = CurrentLine + "25,{{";
                            Comment = Comment + " in Lemuria";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Mu)
                        {
                            CurrentLine = CurrentLine + "26,{{";
                            Comment = Comment + " in Mu";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Nena)
                        {
                            CurrentLine = CurrentLine + "27,{{";
                            Comment = Comment + " in Nena";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == NorthAmerica)
                        {
                            CurrentLine = CurrentLine + "28,{{";
                            Comment = Comment + " in North America";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Novapangaea)
                        {
                            CurrentLine = CurrentLine + "29,{{";
                            Comment = Comment + " in Novapangaea";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Nuna)
                        {
                            CurrentLine = CurrentLine + "30,{{";
                            Comment = Comment + " in Nuna";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Oceania)
                        {
                            CurrentLine = CurrentLine + "31,{{";
                            Comment = Comment + " in Oceania";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Pangaea)
                        {
                            CurrentLine = CurrentLine + "32,{{";
                            Comment = Comment + " in Pangaea";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == PangaeaUltima)
                        {
                            CurrentLine = CurrentLine + "33,{{";
                            Comment = Comment + " in Pangaea Ultima";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Pannotia)
                        {
                            CurrentLine = CurrentLine + "34,{{";
                            Comment = Comment + " in Panotia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Rodinia)
                        {
                            CurrentLine = CurrentLine + "35,{{";
                            Comment = Comment + " in Rodinia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Siberia)
                        {
                            CurrentLine = CurrentLine + "36,{{";
                            Comment = Comment + " in Siberia";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == SouthAmerica)
                        {
                            CurrentLine = CurrentLine + "37,{{";
                            Comment = Comment + " in South America";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == TerraAustralis)
                        {
                            CurrentLine = CurrentLine + "38,{{";
                            Comment = Comment + " in Terra Australis";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Ur)
                        {
                            CurrentLine = CurrentLine + "39,{{";
                            Comment = Comment + " in Ur";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Vaalbara)
                        {
                            CurrentLine = CurrentLine + "40,{{";
                            Comment = Comment + " in Vaalbara";
                        }
                        else if ((MatchTerrain != Ocean && MatchTerrain != Coast) && MatchContinent == Vendian)
                        {
                            CurrentLine = CurrentLine + "41,{{";
                            Comment = Comment + " in Vendian";
                        }
                        else if (MatchTerrain != Ocean && MatchTerrain != Coast)
                        {
                            CurrentLine = CurrentLine + "42,{{";
                            Comment = Comment + " in Zealandia";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "-1,{{";
                        }
#endregion

#region Rivers
                        if (MatchRiverSW == River0)
                        {
                            CurrentLine = CurrentLine + "1,2},{";
                            Comment = Comment + ", River to the Southwest heading Southeast";
                        }
                        else if (MatchRiverSW == River1)
                        {
                            CurrentLine = CurrentLine + "1,5},{";
                            Comment = Comment + ", River to the Southwest heading Northwest";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "0,-1},{";
                            Comment = Comment + "";
                        }

                        if (MatchRiverE == River0)
                        {
                            CurrentLine = CurrentLine + "1,0},{";
                            Comment = Comment + ", River to the East heading North";
                        }
                        else if (MatchRiverE == River1)
                        {
                            CurrentLine = CurrentLine + "1,3},{";
                            Comment = Comment + ", River to the East heading South";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "0,-1},{";
                            Comment = Comment + "";
                        }

                        if (MatchRiverSE == River0)
                        {
                            CurrentLine = CurrentLine + "1,1}},{";
                            Comment = Comment + ", River to the Southeast heading Northeast";
                        }
                        else if (MatchRiverSE == River1)
                        {
                            CurrentLine = CurrentLine + "1,4}},{";
                            Comment = Comment + ", River to the Southeast heading Southwest";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "0,-1}},{";
                        }
#endregion

#region Resources
                        if (MatchResource == Bananas && MatchTerrain != Ocean && MatchTerrain != Coast)
                        {
                            CurrentLine = CurrentLine + "0,1},{";
                            Comment = Comment + ": Bananas";
                        }
                        else if (MatchResource == Cattle)
                        {
                            CurrentLine = CurrentLine + "1,1},{";
                            Comment = Comment + ": Cattle";
                        }
                        else if (MatchResource == Copper)
                        {
                            CurrentLine = CurrentLine + "2,1},{";
                            Comment = Comment + ": Copper";
                        }
                        else if (MatchResource == Crabs)
                        {
                            CurrentLine = CurrentLine + "3,1},{";
                            Comment = Comment + ": Crabs";
                        }
                        else if (MatchResource == Deer)
                        {
                            CurrentLine = CurrentLine + "4,1},{";
                            Comment = Comment + ": Deer";
                        }
                        else if (MatchResource == Fish)
                        {
                            CurrentLine = CurrentLine + "5,1},{";
                            Comment = Comment + ": Fish";
                        }
                        else if (MatchResource == Rice)
                        {
                            CurrentLine = CurrentLine + "6,1},{";
                            Comment = Comment + ": Rice";
                        }
                        else if (MatchResource == Sheep)
                        {
                            CurrentLine = CurrentLine + "7,1},{";
                            Comment = Comment + ": Sheep";
                        }
                        else if (MatchResource == Stone)
                        {
                            CurrentLine = CurrentLine + "8,1},{";
                            Comment = Comment + ": Stone";
                        }
                        else if (MatchResource == Wheat)
                        {
                            CurrentLine = CurrentLine + "9,1},{";
                            Comment = Comment + ": Wheat";
                        }
                        else if (MatchResource == Citrus)
                        {
                            CurrentLine = CurrentLine + "10,1},{";
                            Comment = Comment + ": Citrus";
                        }
                        else if (MatchResource == Cocoa)
                        {
                            CurrentLine = CurrentLine + "11,1},{";
                            Comment = Comment + ": Cocoa";
                        }
                        else if (MatchResource == Coffee)
                        {
                            CurrentLine = CurrentLine + "12,1},{";
                            Comment = Comment + ": Coffee";
                        }
                        else if (MatchResource == Cotton)
                        {
                            CurrentLine = CurrentLine + "13,1},{";
                            Comment = Comment + ": Cotton";
                        }
                        else if (MatchResource == Diamonds)
                        {
                            CurrentLine = CurrentLine + "14,1},{";
                            Comment = Comment + ": Diamonds";
                        }
                        else if (MatchResource == Dyes)
                        {
                            CurrentLine = CurrentLine + "15,1},{";
                            Comment = Comment + ": Dyes";
                        }
                        else if (MatchResource == Furs)
                        {
                            CurrentLine = CurrentLine + "16,1},{";
                            Comment = Comment + ": Furs";
                        }
                        else if (MatchResource == Gypsum)
                        {
                            CurrentLine = CurrentLine + "17,1},{";
                            Comment = Comment + ": Gypsum";
                        }
                        else if (MatchResource == Incense)
                        {
                            CurrentLine = CurrentLine + "18,1},{";
                            Comment = Comment + ": Incense";
                        }
                        else if (MatchResource == Ivory)
                        {
                            CurrentLine = CurrentLine + "19,1},{";
                            Comment = Comment + ": Ivory";
                        }
                        else if (MatchResource == Jade)
                        {
                            CurrentLine = CurrentLine + "20,1},{";
                            Comment = Comment + ": Jade";
                        }
                        else if (MatchResource == Marble)
                        {
                            CurrentLine = CurrentLine + "21,1},{";
                            Comment = Comment + ": Marble";
                        }
                        else if (MatchResource == Mercury)
                        {
                            CurrentLine = CurrentLine + "22,1},{";
                            Comment = Comment + ": Mercury";
                        }
                        else if (MatchResource == Pearls)
                        {
                            CurrentLine = CurrentLine + "23,1},{";
                            Comment = Comment + ": Pearls";
                        }
                        else if (MatchResource == Salt)
                        {
                            CurrentLine = CurrentLine + "24,1},{";
                            Comment = Comment + ": Salt";
                        }
                        else if (MatchResource == Silk)
                        {
                            CurrentLine = CurrentLine + "25,1},{";
                            Comment = Comment + ": Silk";
                        }
                        else if (MatchResource == Silver)
                        {
                            CurrentLine = CurrentLine + "26,1},{";
                            Comment = Comment + ": Silver";
                        }
                        else if (MatchResource == Spices)
                        {
                            CurrentLine = CurrentLine + "27,1},{";
                            Comment = Comment + ": Spices";
                        }
                        else if (MatchResource == Sugar)
                        {
                            CurrentLine = CurrentLine + "28,1},{";
                            Comment = Comment + ": Sugar";
                        }
                        else if (MatchResource == Tea)
                        {
                            CurrentLine = CurrentLine + "29,1},{";
                            Comment = Comment + ": Tea";
                        }
                        else if (MatchResource == Tobacco)
                        {
                            CurrentLine = CurrentLine + "30,1},{";
                            Comment = Comment + ": Tobacco";
                        }
                        else if (MatchResource == Truffles)
                        {
                            CurrentLine = CurrentLine + "31,1},{";
                            Comment = Comment + ": Truffles";
                        }
                        else if (MatchResource == Whales && (MatchTerrain == Ocean || MatchTerrain == Coast))
                        {
                            CurrentLine = CurrentLine + "32,1},{";
                            Comment = Comment + ": Whales";
                        }
                        else if (MatchResource == Wine)
                        {
                            CurrentLine = CurrentLine + "33,1},{";
                            Comment = Comment + ": Wine";
                        }
                        else if (MatchResource == Aluminium)
                        {
                            CurrentLine = CurrentLine + "40,1},{";
                            Comment = Comment + ": Aluminium";
                        }
                        else if (MatchResource == Coal)
                        {
                            CurrentLine = CurrentLine + "41,1},{";
                            Comment = Comment + ": Coal";
                        }
                        else if (MatchResource == Horses)
                        {
                            CurrentLine = CurrentLine + "42,1},{";
                            Comment = Comment + ": Horses";
                        }
                        else if (MatchResource == Iron)
                        {
                            CurrentLine = CurrentLine + "43,1},{";
                            Comment = Comment + ": Iron";
                        }
                        else if (MatchResource == Niter)
                        {
                            CurrentLine = CurrentLine + "44,1},{";
                            Comment = Comment + ": Niter";
                        }
                        else if (MatchResource == Oil)
                        {
                            CurrentLine = CurrentLine + "45,1},{";
                            Comment = Comment + ": Oil";
                        }
                        else if (MatchResource == Uranium)
                        {
                            CurrentLine = CurrentLine + "46,1},{";
                            Comment = Comment + ": Uranium";
                        }
                        else if (MatchResource == AntiquitySite)
                        {
                            CurrentLine = CurrentLine + "47,1},{";
                            Comment = Comment + ": Antiquity Site";
                        }
                        else if (MatchResource == Shipwreck)
                        {
                            CurrentLine = CurrentLine + "48,1},{";
                            Comment = Comment + ": Shipwreck";
                        }
                        else if (MatchResource == Amber)
                        {
                            CurrentLine = CurrentLine + "49,1},{";
                            Comment = Comment + ": Amber";
                        }
                        else if (MatchResource == Olives)
                        {
                            CurrentLine = CurrentLine + "50,1},{";
                            Comment = Comment + ": Olives";
                        }
                        else if (MatchResource == Turtles)
                        {
                            CurrentLine = CurrentLine + "51,1},{";
                            Comment = Comment + ": Turtles";
                        }
                        else
                        {
                            CurrentLine = CurrentLine + "-1,0},{";
                        }
#endregion

#region Cliffs
                        if (MatchRiverSW == Cliffs)
                        {
                            CurrentLine = CurrentLine + "1,";
                            Comment = Comment + ", Cliffs to the Southwest";
                        }
                        else
                        {

                            CurrentLine = CurrentLine + "0,";
                        }
                        if (MatchRiverE == Cliffs)
                        {
                            CurrentLine = CurrentLine + "1,";
                            Comment = Comment + ", Cliffs to the East";
                        }
                        else
                        {

                            CurrentLine = CurrentLine + "0,";
                        }
                        if (MatchRiverSE == Cliffs)
                        {
                            CurrentLine = CurrentLine + "1}}";
                            Comment = Comment + ", Cliffs to the Southeast";
                        }
                        else
                        {

                            CurrentLine = CurrentLine + "0}}";
                        }
#endregion
                        Output[Line] = CurrentLine + Comment;
                        LuaGenMap = LuaGenMap + Output[Line] + "\n";
                        Line++;
                    }
                }
                string Wrapped = "" + WrapX.Checked;
                LuaFile = LuaStart + MapW + "\nlocal g_iH = " + MapH + LuaEndStart + Wrapped.ToLower() + LuaEndWrap + LuaGenMap + LuaEndMap;
                DirectoryInfo LuaDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Lua");
                File.Create(FolderPath + "\\" + ProjectName + "\\Lua\\" + ProjectName + "_Map.lua").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Lua\\" + ProjectName + "_Map.lua", LuaFile);
#endregion

#region Config

#region Config Values
                if (STDRules.Checked)
                {
                    ConfigMap = ConfigMap + "<Row File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                }
                if (RNFRules.Checked)
                {
                    ConfigMap = ConfigMap + "<Row Domain=\"Maps:Expansion1Maps\" File=\"" + ProjectName + "_Map.lua\" Name=\"LOC_" + ProjectName + "_Map_NAME\" Description=\"LOC_" + ProjectName + "_Map_DESC\" SortIndex=\"50\"/>\n";
                }

#region Rivers
                if (OneSelected(RiversGenerate.Checked, RiversImport.Checked, RiversEmpty.Checked))
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT", DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 0, 231);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "RiversPlacement", "RIVERS_PLACEMENT", DefaultPlacement(RiversImport.Checked, RiversGenerate.Checked, RiversEmpty.Checked), "RiversPlacement", 2, 1, 231);
                }
#endregion

#region Continents
                if (ContinentsGenerate.Checked && !ContinentsImport.Checked || !ContinentsGenerate.Checked && ContinentsImport.Checked)
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT", DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 0, 232);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ContinentsPlacement", "CONTINENT_PLACEMENT", DefaultPlacement(ContinentsImport.Checked, ContinentsGenerate.Checked, ContinentsGenerate.Checked), "ContinentsPlacement", 2, 1, 232);
                }
#endregion

#region Natural Wonders
                if (OneSelected(WondersGenerate.Checked, WondersImport.Checked, WondersEmpty.Checked))
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT", DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 0, 244);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "NaturalWondersPlacement", "NATURAL_WONDERS_PLACEMENT", DefaultPlacement(WondersImport.Checked, WondersGenerate.Checked, WondersEmpty.Checked), "NaturalWondersPlacement", 0, 1, 244);
                }
#endregion

#region Features
                if (OneSelected(FeaturesGenerate.Checked, FeaturesImport.Checked, FeaturesEmpty.Checked))
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "FeaturesPlacement", "FEATURES_PLACEMENT", DefaultPlacement(FeaturesImport.Checked, FeaturesGenerate.Checked, FeaturesEmpty.Checked), "FeaturesPlacement", 0, 0, 245);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "Rainfall", "RAINFALL", "2", "rainfall", 0, 1, 250);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "FeaturesPlacement", "FEATURES_PLACEMENT", DefaultPlacement(FeaturesImport.Checked, FeaturesGenerate.Checked, FeaturesEmpty.Checked), "FeaturesPlacement", 0, 1, 245);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "Rainfall", "RAINFALL", "2", "rainfall", 0, 1, 250);
                }
#endregion

#region Resources
                if (OneSelected(ResourcesGenerate.Checked, ResourcesImport.Checked, ResourcesEmpty.Checked))
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ResourcesPlacement", "RESOURCES_PLACEMENT", DefaultPlacement(ResourcesImport.Checked, ResourcesGenerate.Checked, ResourcesEmpty.Checked), "ResourcesPlacement", 0, 0, 269);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "Resources", "RESOURCES", "2", "resources", 0, 1, 270);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ResourcesPlacement", "RESOURCES_PLACEMENT", DefaultPlacement(ResourcesImport.Checked, ResourcesGenerate.Checked, ResourcesEmpty.Checked), "ResourcesPlacement", 0, 1, 269);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "Resources", "RESOURCES", "2", "resources", 0, 1, 270);
                }
#endregion

#region TSL
                if (TSLEnable.Checked)
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 280);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "CivilizationPlacement", "CIVILIZATION_PLACEMENT", "PLACEMENT_TSL", "CivilizationPlacement", 0, 1, 280);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ForceTSL", "FORCE_TSL", "FORCE_TSL_OFF", "ForceTSL", 0, 1, 281);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 285);
                }
                else
                {
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 280);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "CivilizationPlacement", "CIVILIZATION_PLACEMENT", "PLACEMENT_DEFAULT", "CivilizationPlacement", 0, 0, 280);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "ForceTSL", "FORCE_TSL", "FORCE_TSL_OFF", "ForceTSL", 0, 1, 281);
                    ConfigParameters = ConfigParameters + ParameterRow(ProjectName, "StartPosition", "START_POSITION", "2", "start", 0, 1, 285);
                }
#endregion

                ConfigParameters = ConfigParameters + "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapSize\" Name=\"LOC_MAP_SIZE\" Description=\"\" Domain=\"StandardMapSizes\" DefaultValue=\"" + SizeOfMap(MapW, MapH) + "\" ConfigurationGroup=\"Map\" ConfigurationId=\"MAP_SIZE\" GroupId=\"MapOptions\" Hash=\"1\" Visible=\"0\" SortIndex=\"225\"/>\n";
                ConfigParameters = ConfigParameters + "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"HideSize\" Name=\"HideSize\" Description=\"\" Domain=\"bool\" DefaultValue=\"1\" ConfigurationGroup=\"Map\" ConfigurationId=\"HideSize\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";
                ConfigParameters = ConfigParameters + "\t\t<Row Key1=\"Map\" Key2=\"" + ProjectName + "_Map.lua\" ParameterId=\"" + ProjectName + "_MapName\" Name=\"MapName\" Description=\"\" Domain=\"text\" DefaultValue=\"" + ProjectName + "_Map\" ConfigurationGroup=\"Map\" ConfigurationId=\"MapName\" GroupId=\"MapOptions\" Visible=\"0\" SortIndex=\"2010\"/>\n";

                //MapSupportedValues
#region Rivers
                if (!RiversGenerate.Checked || !RiversImport.Checked || !RiversEmpty.Checked)
                {
                    if (RiversGenerate.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (RiversImport.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (RiversEmpty.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"RiversPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Continents
                if (!ContinentsGenerate.Checked || !ContinentsImport.Checked)
                {
                    if (ContinentsGenerate.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ContinentsPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (ContinentsImport.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ContinentsPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                }
#endregion

#region Wonders
                if (!WondersGenerate.Checked || !WondersImport.Checked || !WondersEmpty.Checked)
                {
                    if (WondersGenerate.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (WondersImport.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (WondersEmpty.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"NaturalWondersPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Features
                if (!FeaturesGenerate.Checked || !FeaturesImport.Checked || !FeaturesEmpty.Checked)
                {
                    if (FeaturesGenerate.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (FeaturesImport.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (FeaturesEmpty.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"FeaturesPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

#region Resources
                if (!ResourcesGenerate.Checked || !ResourcesImport.Checked || !ResourcesEmpty.Checked)
                {
                    if (ResourcesGenerate.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_DEFAULT\" />\n";
                    }
                    if (ResourcesImport.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_IMPORT\" />\n";
                    }
                    if (ResourcesEmpty.Checked)
                    {
                        ConfigMapSupport = ConfigMapSupport + "\t\t<Row Map = \"" + ProjectName + "_Map.lua\" Domain = \"ResourcesPlacement\" Value = \"PLACEMENT_EMPTY\" />\n";
                    }
                }
#endregion

                Config = ConfigMap + ConfigParameters + ConfigMapSupport + ConfigEnd;
                DirectoryInfo ConfigDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Config");
                File.Create(FolderPath + "\\" + ProjectName + "\\Config\\Config.xml").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Config\\Config.xml", Config);
#endregion

#region Config Text
                string ConfigText = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<GameData>\n\t<LocalizedText>\n\t\t<Replace Tag=\"LOC_" + ProjectName + "_Map_NAME\" Language=\"en_US\">\n" +
                                    "\t\t\t<Text>" + ProjectText.Text + "</Text>\n\t\t</Replace>\n\t\t<Replace Tag=\"LOC_" + ProjectName + "_Map_DESC\" Language=\"en_US\"\n>" +
                                    "\t\t\t<Text>" + ProjectText.Text + " (" + MapW + "x" + MapH + ") by " + AuthorText.Text + "</Text>\n\t\t</Replace>\n\t</LocalizedText>\n</GameData>";
                File.Create(FolderPath + "\\" + ProjectName + "\\Config\\Config_Text.xml").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Config\\Config_Text.xml", ConfigText);
#endregion

#endregion

#region TSL
                string TSLText = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<GameData>\n\t<!-- You have to fill this in manually -->\n\t<StartPosition>\n\t\t<!--<Replace MapName=\"" + ProjectName + "_Map\" Civilization =\"CIVILIZATION_AMERICA\"	X=\"0\" Y=\"0\" />-->" +
                                    "\n\t</StartPosition>\n</GameData>";
                DirectoryInfo MapDir = Directory.CreateDirectory(FolderPath + "\\" + ProjectName + "\\Map");
                File.Create(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml").Dispose();
                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\Map\\Map.xml", TSLText);
#endregion

#region Mod Info

                string ModInfo = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<Mod id=\"" + ModID + "\" version = \"1\">\n\t<Properties>\n\t\t<Name>" + ProjectText.Text + " by " + AuthorText.Text + "</Name>" +
                                    "\n\t\t<Description>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Community Map Editor</Description>" +
                                    "\n\t\t<Teaser>This map has been created by " + AuthorText.Text + " using the \"Yet (not) Another Bit Map Converter\" Community Map Editor</Teaser>\n\t\t<Authors>" + AuthorText.Text + "</Authors>\n\t</Properties>" +
                                    "\n\t<Dependencies>\n\t\t<Mod id=\"36e88483-48fe-4545-b85f-bafc50dde315\" title=\"Yet (not) Another Maps Pack\"/>\n\t</Dependencies>" +
                                    "\n\t<FrontEndActions>\n\t\t<UpdateDatabase id=\"" + ProjectName + "_SETTING\">\n\t\t\t<File>Config/Config.xml</File>\n\t\t</UpdateDatabase>" +
                                    "\n\t\t<UpdateText id=\"NewAction\">\n\t\t\t<File>Config/Config_Text.xml</File>\n\t\t</UpdateText>\n\t</FrontEndActions>" +
                                    "\n\t<InGameActions>\n\t\t<ImportFiles id=\"" + ProjectName + "_IMPORT\">\n\t\t\t<File>Lua/" + ProjectName + "_Map.lua</File>\n\t\t</ImportFiles>" +
                                    "\n\t\t<UpdateDatabase id=\"NewAction\">\n\t\t\t<File>Map/Map.xml</File>\n\t\t</UpdateDatabase>\n\t</InGameActions>" +
                                    "\n\t<Files>\n\t\t<File>Config/Config.xml</File>\n\t\t<File>Config/Config_Text.xml</File>\n\t\t<File>Map/Map.xml</File>\n\t\t<File>Lua/" + ProjectName + "_Map.lua</File>\n\t</Files>\n</Mod>";

                System.IO.File.WriteAllText(FolderPath + "\\" + ProjectName + "\\" + ProjectName + ".modinfo", ModInfo);
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
            if (x * y < 1500) return "MAPSIZE_DUAL";
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

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            GenerateMap.Enabled = true;
            timer.Stop();
        }
#endregion
    }
}

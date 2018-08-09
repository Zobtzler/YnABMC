namespace YnABMC
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AuthorText = new System.Windows.Forms.TextBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ProjectText = new System.Windows.Forms.TextBox();
            this.ProjectLabel = new System.Windows.Forms.Label();
            this.GenerateMap = new System.Windows.Forms.Button();
            this.TSLEnable = new System.Windows.Forms.CheckBox();
            this.ResourcesEmpty = new System.Windows.Forms.CheckBox();
            this.ResourcesImport = new System.Windows.Forms.CheckBox();
            this.ResourcesGenerate = new System.Windows.Forms.CheckBox();
            this.ResourcesLabel = new System.Windows.Forms.Label();
            this.FeaturesEmpty = new System.Windows.Forms.CheckBox();
            this.FeaturesImport = new System.Windows.Forms.CheckBox();
            this.FeaturesGenerate = new System.Windows.Forms.CheckBox();
            this.FeaturesLabel = new System.Windows.Forms.Label();
            this.WondersEmpty = new System.Windows.Forms.CheckBox();
            this.WondersImport = new System.Windows.Forms.CheckBox();
            this.WondersGenerate = new System.Windows.Forms.CheckBox();
            this.WondersLabel = new System.Windows.Forms.Label();
            this.ContinentsImport = new System.Windows.Forms.CheckBox();
            this.ContinentsGenerate = new System.Windows.Forms.CheckBox();
            this.ContinentsLabel = new System.Windows.Forms.Label();
            this.RiversEmpty = new System.Windows.Forms.CheckBox();
            this.RiversImport = new System.Windows.Forms.CheckBox();
            this.RiversGenerate = new System.Windows.Forms.CheckBox();
            this.RiversLabel = new System.Windows.Forms.Label();
            this.RNFRules = new System.Windows.Forms.CheckBox();
            this.STDRules = new System.Windows.Forms.CheckBox();
            this.WrapX = new System.Windows.Forms.CheckBox();
            this.ModIDRandom = new System.Windows.Forms.Button();
            this.ModIDLabel = new System.Windows.Forms.Label();
            this.ModIDValue = new System.Windows.Forms.TextBox();
            this.SelectSource = new System.Windows.Forms.Button();
            this.Attention = new System.Windows.Forms.Label();
            this.SelectLua = new System.Windows.Forms.Button();
            this.AdvancedOptions = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // AuthorText
            // 
            this.AuthorText.Location = new System.Drawing.Point(375, 36);
            this.AuthorText.Name = "AuthorText";
            this.AuthorText.Size = new System.Drawing.Size(195, 20);
            this.AuthorText.TabIndex = 69;
            this.AuthorText.TextChanged += new System.EventHandler(this.AuthorText_TextChanged);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(372, 12);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(38, 13);
            this.AuthorLabel.TabIndex = 68;
            this.AuthorLabel.Text = "Author";
            // 
            // ProjectText
            // 
            this.ProjectText.Location = new System.Drawing.Point(178, 36);
            this.ProjectText.Name = "ProjectText";
            this.ProjectText.Size = new System.Drawing.Size(191, 20);
            this.ProjectText.TabIndex = 67;
            this.ProjectText.TextChanged += new System.EventHandler(this.ProjectText_TextChanged);
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.AutoSize = true;
            this.ProjectLabel.Location = new System.Drawing.Point(175, 12);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(71, 13);
            this.ProjectLabel.TabIndex = 66;
            this.ProjectLabel.Text = "Project Name";
            // 
            // GenerateMap
            // 
            this.GenerateMap.Location = new System.Drawing.Point(12, 85);
            this.GenerateMap.Name = "GenerateMap";
            this.GenerateMap.Size = new System.Drawing.Size(139, 67);
            this.GenerateMap.TabIndex = 65;
            this.GenerateMap.Text = "Generate Map";
            this.GenerateMap.UseVisualStyleBackColor = true;
            this.GenerateMap.Click += new System.EventHandler(this.GenerateMap_Click);
            // 
            // TSLEnable
            // 
            this.TSLEnable.AutoSize = true;
            this.TSLEnable.Checked = true;
            this.TSLEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSLEnable.Location = new System.Drawing.Point(178, 336);
            this.TSLEnable.Name = "TSLEnable";
            this.TSLEnable.Size = new System.Drawing.Size(191, 17);
            this.TSLEnable.TabIndex = 64;
            this.TSLEnable.Text = "Map Supports True Start Locations";
            this.TSLEnable.UseVisualStyleBackColor = true;
            // 
            // ResourcesEmpty
            // 
            this.ResourcesEmpty.AutoSize = true;
            this.ResourcesEmpty.Checked = true;
            this.ResourcesEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResourcesEmpty.Location = new System.Drawing.Point(370, 313);
            this.ResourcesEmpty.Name = "ResourcesEmpty";
            this.ResourcesEmpty.Size = new System.Drawing.Size(55, 17);
            this.ResourcesEmpty.TabIndex = 63;
            this.ResourcesEmpty.Text = "Empty";
            this.ResourcesEmpty.UseVisualStyleBackColor = true;
            this.ResourcesEmpty.CheckedChanged += new System.EventHandler(this.ResourcesEmpty_CheckedChanged);
            // 
            // ResourcesImport
            // 
            this.ResourcesImport.AutoSize = true;
            this.ResourcesImport.Checked = true;
            this.ResourcesImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResourcesImport.Location = new System.Drawing.Point(296, 313);
            this.ResourcesImport.Name = "ResourcesImport";
            this.ResourcesImport.Size = new System.Drawing.Size(55, 17);
            this.ResourcesImport.TabIndex = 62;
            this.ResourcesImport.Text = "Import";
            this.ResourcesImport.UseVisualStyleBackColor = true;
            this.ResourcesImport.CheckedChanged += new System.EventHandler(this.ResourcesImport_CheckedChanged);
            // 
            // ResourcesGenerate
            // 
            this.ResourcesGenerate.AutoSize = true;
            this.ResourcesGenerate.Checked = true;
            this.ResourcesGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResourcesGenerate.Location = new System.Drawing.Point(178, 313);
            this.ResourcesGenerate.Name = "ResourcesGenerate";
            this.ResourcesGenerate.Size = new System.Drawing.Size(97, 17);
            this.ResourcesGenerate.TabIndex = 61;
            this.ResourcesGenerate.Text = "Map Generator";
            this.ResourcesGenerate.UseVisualStyleBackColor = true;
            // 
            // ResourcesLabel
            // 
            this.ResourcesLabel.AutoSize = true;
            this.ResourcesLabel.Location = new System.Drawing.Point(85, 314);
            this.ResourcesLabel.Name = "ResourcesLabel";
            this.ResourcesLabel.Size = new System.Drawing.Size(58, 13);
            this.ResourcesLabel.TabIndex = 60;
            this.ResourcesLabel.Text = "Resources";
            // 
            // FeaturesEmpty
            // 
            this.FeaturesEmpty.AutoSize = true;
            this.FeaturesEmpty.Checked = true;
            this.FeaturesEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FeaturesEmpty.Location = new System.Drawing.Point(370, 290);
            this.FeaturesEmpty.Name = "FeaturesEmpty";
            this.FeaturesEmpty.Size = new System.Drawing.Size(55, 17);
            this.FeaturesEmpty.TabIndex = 59;
            this.FeaturesEmpty.Text = "Empty";
            this.FeaturesEmpty.UseVisualStyleBackColor = true;
            this.FeaturesEmpty.CheckedChanged += new System.EventHandler(this.FeaturesEmpty_CheckedChanged);
            // 
            // FeaturesImport
            // 
            this.FeaturesImport.AutoSize = true;
            this.FeaturesImport.Checked = true;
            this.FeaturesImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FeaturesImport.Location = new System.Drawing.Point(296, 290);
            this.FeaturesImport.Name = "FeaturesImport";
            this.FeaturesImport.Size = new System.Drawing.Size(55, 17);
            this.FeaturesImport.TabIndex = 58;
            this.FeaturesImport.Text = "Import";
            this.FeaturesImport.UseVisualStyleBackColor = true;
            this.FeaturesImport.CheckedChanged += new System.EventHandler(this.FeaturesImport_CheckedChanged);
            // 
            // FeaturesGenerate
            // 
            this.FeaturesGenerate.AutoSize = true;
            this.FeaturesGenerate.Checked = true;
            this.FeaturesGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FeaturesGenerate.Location = new System.Drawing.Point(178, 290);
            this.FeaturesGenerate.Name = "FeaturesGenerate";
            this.FeaturesGenerate.Size = new System.Drawing.Size(97, 17);
            this.FeaturesGenerate.TabIndex = 57;
            this.FeaturesGenerate.Text = "Map Generator";
            this.FeaturesGenerate.UseVisualStyleBackColor = true;
            // 
            // FeaturesLabel
            // 
            this.FeaturesLabel.AutoSize = true;
            this.FeaturesLabel.Location = new System.Drawing.Point(85, 291);
            this.FeaturesLabel.Name = "FeaturesLabel";
            this.FeaturesLabel.Size = new System.Drawing.Size(48, 13);
            this.FeaturesLabel.TabIndex = 56;
            this.FeaturesLabel.Text = "Features";
            // 
            // WondersEmpty
            // 
            this.WondersEmpty.AutoSize = true;
            this.WondersEmpty.Checked = true;
            this.WondersEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WondersEmpty.Location = new System.Drawing.Point(370, 267);
            this.WondersEmpty.Name = "WondersEmpty";
            this.WondersEmpty.Size = new System.Drawing.Size(55, 17);
            this.WondersEmpty.TabIndex = 55;
            this.WondersEmpty.Text = "Empty";
            this.WondersEmpty.UseVisualStyleBackColor = true;
            this.WondersEmpty.CheckedChanged += new System.EventHandler(this.WondersEmpty_CheckedChanged);
            // 
            // WondersImport
            // 
            this.WondersImport.AutoSize = true;
            this.WondersImport.Checked = true;
            this.WondersImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WondersImport.Location = new System.Drawing.Point(296, 267);
            this.WondersImport.Name = "WondersImport";
            this.WondersImport.Size = new System.Drawing.Size(55, 17);
            this.WondersImport.TabIndex = 54;
            this.WondersImport.Text = "Import";
            this.WondersImport.UseVisualStyleBackColor = true;
            this.WondersImport.CheckedChanged += new System.EventHandler(this.WondersImport_CheckedChanged);
            // 
            // WondersGenerate
            // 
            this.WondersGenerate.AutoSize = true;
            this.WondersGenerate.Checked = true;
            this.WondersGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WondersGenerate.Location = new System.Drawing.Point(178, 267);
            this.WondersGenerate.Name = "WondersGenerate";
            this.WondersGenerate.Size = new System.Drawing.Size(97, 17);
            this.WondersGenerate.TabIndex = 53;
            this.WondersGenerate.Text = "Map Generator";
            this.WondersGenerate.UseVisualStyleBackColor = true;
            // 
            // WondersLabel
            // 
            this.WondersLabel.AutoSize = true;
            this.WondersLabel.Location = new System.Drawing.Point(85, 268);
            this.WondersLabel.Name = "WondersLabel";
            this.WondersLabel.Size = new System.Drawing.Size(87, 13);
            this.WondersLabel.TabIndex = 52;
            this.WondersLabel.Text = "Natural Wonders";
            // 
            // ContinentsImport
            // 
            this.ContinentsImport.AutoSize = true;
            this.ContinentsImport.Checked = true;
            this.ContinentsImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ContinentsImport.Location = new System.Drawing.Point(296, 244);
            this.ContinentsImport.Name = "ContinentsImport";
            this.ContinentsImport.Size = new System.Drawing.Size(55, 17);
            this.ContinentsImport.TabIndex = 51;
            this.ContinentsImport.Text = "Import";
            this.ContinentsImport.UseVisualStyleBackColor = true;
            this.ContinentsImport.CheckedChanged += new System.EventHandler(this.ContinentsImport_CheckedChanged);
            // 
            // ContinentsGenerate
            // 
            this.ContinentsGenerate.AutoSize = true;
            this.ContinentsGenerate.Checked = true;
            this.ContinentsGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ContinentsGenerate.Location = new System.Drawing.Point(178, 244);
            this.ContinentsGenerate.Name = "ContinentsGenerate";
            this.ContinentsGenerate.Size = new System.Drawing.Size(97, 17);
            this.ContinentsGenerate.TabIndex = 50;
            this.ContinentsGenerate.Text = "Map Generator";
            this.ContinentsGenerate.UseVisualStyleBackColor = true;
            // 
            // ContinentsLabel
            // 
            this.ContinentsLabel.AutoSize = true;
            this.ContinentsLabel.Location = new System.Drawing.Point(85, 245);
            this.ContinentsLabel.Name = "ContinentsLabel";
            this.ContinentsLabel.Size = new System.Drawing.Size(57, 13);
            this.ContinentsLabel.TabIndex = 49;
            this.ContinentsLabel.Text = "Continents";
            // 
            // RiversEmpty
            // 
            this.RiversEmpty.AutoSize = true;
            this.RiversEmpty.Checked = true;
            this.RiversEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RiversEmpty.Location = new System.Drawing.Point(370, 218);
            this.RiversEmpty.Name = "RiversEmpty";
            this.RiversEmpty.Size = new System.Drawing.Size(55, 17);
            this.RiversEmpty.TabIndex = 48;
            this.RiversEmpty.Text = "Empty";
            this.RiversEmpty.UseVisualStyleBackColor = true;
            this.RiversEmpty.CheckedChanged += new System.EventHandler(this.RiversEmpty_CheckedChanged);
            // 
            // RiversImport
            // 
            this.RiversImport.AutoSize = true;
            this.RiversImport.Checked = true;
            this.RiversImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RiversImport.Location = new System.Drawing.Point(296, 218);
            this.RiversImport.Name = "RiversImport";
            this.RiversImport.Size = new System.Drawing.Size(55, 17);
            this.RiversImport.TabIndex = 47;
            this.RiversImport.Text = "Import";
            this.RiversImport.UseVisualStyleBackColor = true;
            this.RiversImport.CheckedChanged += new System.EventHandler(this.RiversImport_CheckedChanged);
            // 
            // RiversGenerate
            // 
            this.RiversGenerate.AutoSize = true;
            this.RiversGenerate.Checked = true;
            this.RiversGenerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RiversGenerate.Location = new System.Drawing.Point(178, 221);
            this.RiversGenerate.Name = "RiversGenerate";
            this.RiversGenerate.Size = new System.Drawing.Size(97, 17);
            this.RiversGenerate.TabIndex = 46;
            this.RiversGenerate.Text = "Map Generator";
            this.RiversGenerate.UseVisualStyleBackColor = true;
            // 
            // RiversLabel
            // 
            this.RiversLabel.AutoSize = true;
            this.RiversLabel.Location = new System.Drawing.Point(85, 222);
            this.RiversLabel.Name = "RiversLabel";
            this.RiversLabel.Size = new System.Drawing.Size(37, 13);
            this.RiversLabel.TabIndex = 45;
            this.RiversLabel.Text = "Rivers";
            // 
            // RNFRules
            // 
            this.RNFRules.AutoSize = true;
            this.RNFRules.Checked = true;
            this.RNFRules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RNFRules.Location = new System.Drawing.Point(296, 175);
            this.RNFRules.Name = "RNFRules";
            this.RNFRules.Size = new System.Drawing.Size(88, 17);
            this.RNFRules.TabIndex = 44;
            this.RNFRules.Text = "Rise And Fall";
            this.RNFRules.UseVisualStyleBackColor = true;
            this.RNFRules.CheckedChanged += new System.EventHandler(this.RNFRules_CheckedChanged);
            // 
            // STDRules
            // 
            this.STDRules.AutoSize = true;
            this.STDRules.Checked = true;
            this.STDRules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.STDRules.Location = new System.Drawing.Point(178, 175);
            this.STDRules.Name = "STDRules";
            this.STDRules.Size = new System.Drawing.Size(99, 17);
            this.STDRules.TabIndex = 43;
            this.STDRules.Text = "Standard Rules";
            this.STDRules.UseVisualStyleBackColor = true;
            // 
            // WrapX
            // 
            this.WrapX.AutoSize = true;
            this.WrapX.Checked = true;
            this.WrapX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WrapX.Location = new System.Drawing.Point(178, 198);
            this.WrapX.Name = "WrapX";
            this.WrapX.Size = new System.Drawing.Size(143, 17);
            this.WrapX.TabIndex = 42;
            this.WrapX.Text = "Enable Circumnavigation";
            this.WrapX.UseVisualStyleBackColor = true;
            // 
            // ModIDRandom
            // 
            this.ModIDRandom.Location = new System.Drawing.Point(451, 107);
            this.ModIDRandom.Name = "ModIDRandom";
            this.ModIDRandom.Size = new System.Drawing.Size(119, 23);
            this.ModIDRandom.TabIndex = 41;
            this.ModIDRandom.Text = "Generate Mod ID";
            this.ModIDRandom.UseVisualStyleBackColor = true;
            this.ModIDRandom.Click += new System.EventHandler(this.ModIDRandom_Click);
            // 
            // ModIDLabel
            // 
            this.ModIDLabel.AutoSize = true;
            this.ModIDLabel.Location = new System.Drawing.Point(175, 85);
            this.ModIDLabel.Name = "ModIDLabel";
            this.ModIDLabel.Size = new System.Drawing.Size(42, 13);
            this.ModIDLabel.TabIndex = 40;
            this.ModIDLabel.Text = "Mod ID";
            // 
            // ModIDValue
            // 
            this.ModIDValue.Location = new System.Drawing.Point(178, 109);
            this.ModIDValue.Name = "ModIDValue";
            this.ModIDValue.Size = new System.Drawing.Size(267, 20);
            this.ModIDValue.TabIndex = 39;
            this.ModIDValue.TextChanged += new System.EventHandler(this.ModIDValue_TextChanged);
            // 
            // SelectSource
            // 
            this.SelectSource.Location = new System.Drawing.Point(12, 12);
            this.SelectSource.Name = "SelectSource";
            this.SelectSource.Size = new System.Drawing.Size(139, 67);
            this.SelectSource.TabIndex = 38;
            this.SelectSource.Text = "Select Source File";
            this.SelectSource.UseVisualStyleBackColor = true;
            this.SelectSource.Click += new System.EventHandler(this.SelectSource_Click);
            // 
            // Attention
            // 
            this.Attention.AutoSize = true;
            this.Attention.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Attention.Location = new System.Drawing.Point(-3, 429);
            this.Attention.Name = "Attention";
            this.Attention.Size = new System.Drawing.Size(595, 16);
            this.Attention.TabIndex = 70;
            this.Attention.Text = "This Program is in Alpha, meaning that all maps produced may not work properly wi" +
    "th some settings\r\n";
            // 
            // SelectLua
            // 
            this.SelectLua.Enabled = false;
            this.SelectLua.Location = new System.Drawing.Point(597, 12);
            this.SelectLua.Name = "SelectLua";
            this.SelectLua.Size = new System.Drawing.Size(139, 67);
            this.SelectLua.TabIndex = 71;
            this.SelectLua.Text = "Select .log File (both civ 5 and 6 maps)";
            this.SelectLua.UseVisualStyleBackColor = true;
            this.SelectLua.Visible = false;
            this.SelectLua.Click += new System.EventHandler(this.SelectLua_Click);
            // 
            // AdvancedOptions
            // 
            this.AdvancedOptions.AutoSize = true;
            this.AdvancedOptions.Location = new System.Drawing.Point(178, 383);
            this.AdvancedOptions.Name = "AdvancedOptions";
            this.AdvancedOptions.Size = new System.Drawing.Size(114, 17);
            this.AdvancedOptions.TabIndex = 72;
            this.AdvancedOptions.Text = "Advanced Options";
            this.AdvancedOptions.UseVisualStyleBackColor = true;
            this.AdvancedOptions.CheckedChanged += new System.EventHandler(this.AdvancedOptions_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 454);
            this.Controls.Add(this.AdvancedOptions);
            this.Controls.Add(this.SelectLua);
            this.Controls.Add(this.Attention);
            this.Controls.Add(this.AuthorText);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.ProjectText);
            this.Controls.Add(this.ProjectLabel);
            this.Controls.Add(this.GenerateMap);
            this.Controls.Add(this.TSLEnable);
            this.Controls.Add(this.ResourcesEmpty);
            this.Controls.Add(this.ResourcesImport);
            this.Controls.Add(this.ResourcesGenerate);
            this.Controls.Add(this.ResourcesLabel);
            this.Controls.Add(this.FeaturesEmpty);
            this.Controls.Add(this.FeaturesImport);
            this.Controls.Add(this.FeaturesGenerate);
            this.Controls.Add(this.FeaturesLabel);
            this.Controls.Add(this.WondersEmpty);
            this.Controls.Add(this.WondersImport);
            this.Controls.Add(this.WondersGenerate);
            this.Controls.Add(this.WondersLabel);
            this.Controls.Add(this.ContinentsImport);
            this.Controls.Add(this.ContinentsGenerate);
            this.Controls.Add(this.ContinentsLabel);
            this.Controls.Add(this.RiversEmpty);
            this.Controls.Add(this.RiversImport);
            this.Controls.Add(this.RiversGenerate);
            this.Controls.Add(this.RiversLabel);
            this.Controls.Add(this.RNFRules);
            this.Controls.Add(this.STDRules);
            this.Controls.Add(this.WrapX);
            this.Controls.Add(this.ModIDRandom);
            this.Controls.Add(this.ModIDLabel);
            this.Controls.Add(this.ModIDValue);
            this.Controls.Add(this.SelectSource);
            this.Name = "Form1";
            this.Text = "Yet (not) Another Bit Map Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AuthorText;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.TextBox ProjectText;
        private System.Windows.Forms.Label ProjectLabel;
        private System.Windows.Forms.Button GenerateMap;
        private System.Windows.Forms.CheckBox TSLEnable;
        private System.Windows.Forms.CheckBox ResourcesEmpty;
        private System.Windows.Forms.CheckBox ResourcesImport;
        private System.Windows.Forms.CheckBox ResourcesGenerate;
        private System.Windows.Forms.Label ResourcesLabel;
        private System.Windows.Forms.CheckBox FeaturesEmpty;
        private System.Windows.Forms.CheckBox FeaturesImport;
        private System.Windows.Forms.CheckBox FeaturesGenerate;
        private System.Windows.Forms.Label FeaturesLabel;
        private System.Windows.Forms.CheckBox WondersEmpty;
        private System.Windows.Forms.CheckBox WondersImport;
        private System.Windows.Forms.CheckBox WondersGenerate;
        private System.Windows.Forms.Label WondersLabel;
        private System.Windows.Forms.CheckBox ContinentsImport;
        private System.Windows.Forms.CheckBox ContinentsGenerate;
        private System.Windows.Forms.Label ContinentsLabel;
        private System.Windows.Forms.CheckBox RiversEmpty;
        private System.Windows.Forms.CheckBox RiversImport;
        private System.Windows.Forms.CheckBox RiversGenerate;
        private System.Windows.Forms.Label RiversLabel;
        private System.Windows.Forms.CheckBox RNFRules;
        private System.Windows.Forms.CheckBox STDRules;
        private System.Windows.Forms.CheckBox WrapX;
        private System.Windows.Forms.Button ModIDRandom;
        private System.Windows.Forms.Label ModIDLabel;
        private System.Windows.Forms.TextBox ModIDValue;
        private System.Windows.Forms.Button SelectSource;
        private System.Windows.Forms.Label Attention;
        private System.Windows.Forms.Button SelectLua;
        private System.Windows.Forms.CheckBox AdvancedOptions;
    }
}


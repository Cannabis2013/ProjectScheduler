namespace Projecthandler.Forms.Project_and_activity_management.Controls
{
    partial class ActivityManagement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityManagement));
            this.ActivityListView = new System.Windows.Forms.ListView();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TabView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.ItemIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainLayout.SuspendLayout();
            this.TabView.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.ButtonLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ActivityListView
            // 
            this.ActivityListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivityListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ActivityListView.Location = new System.Drawing.Point(0, 0);
            this.ActivityListView.Margin = new System.Windows.Forms.Padding(0);
            this.ActivityListView.Name = "ActivityListView";
            this.ActivityListView.Size = new System.Drawing.Size(369, 360);
            this.ActivityListView.TabIndex = 4;
            this.ActivityListView.UseCompatibleStateImageBehavior = false;
            this.ActivityListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ActivityListView_MouseDoubleClick);
            // 
            // MainLayout
            // 
            this.MainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLayout.BackColor = System.Drawing.Color.Sienna;
            this.MainLayout.ColumnCount = 1;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainLayout.Controls.Add(this.TabView, 0, 0);
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Margin = new System.Windows.Forms.Padding(5);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 2;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainLayout.Size = new System.Drawing.Size(473, 385);
            this.MainLayout.TabIndex = 1;
            // 
            // TabView
            // 
            this.TabView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabView.Controls.Add(this.tabPage1);
            this.TabView.Location = new System.Drawing.Point(0, 0);
            this.TabView.Margin = new System.Windows.Forms.Padding(0);
            this.TabView.Multiline = true;
            this.TabView.Name = "TabView";
            this.TabView.Padding = new System.Drawing.Point(0, 0);
            this.TabView.SelectedIndex = 0;
            this.TabView.Size = new System.Drawing.Size(473, 385);
            this.TabView.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Sienna;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(465, 359);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Activity overview";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.ButtonLayout, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ActivityListView, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(465, 360);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ButtonLayout
            // 
            this.ButtonLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLayout.ColumnCount = 1;
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.Controls.Add(this.linkLabel1, 0, 0);
            this.ButtonLayout.Controls.Add(this.linkLabel2, 0, 1);
            this.ButtonLayout.Controls.Add(this.linkLabel3, 0, 2);
            this.ButtonLayout.Location = new System.Drawing.Point(369, 0);
            this.ButtonLayout.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonLayout.Name = "ButtonLayout";
            this.ButtonLayout.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ButtonLayout.RowCount = 5;
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ButtonLayout.Size = new System.Drawing.Size(96, 360);
            this.ButtonLayout.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(3, 5);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(74, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Create activity";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.Location = new System.Drawing.Point(3, 18);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(61, 13);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Edit activity";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel3.Location = new System.Drawing.Point(3, 31);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(74, 13);
            this.linkLabel3.TabIndex = 2;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Delete activity";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // ItemIcons
            // 
            this.ItemIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ItemIcons.ImageStream")));
            this.ItemIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ItemIcons.Images.SetKeyName(0, "Project_Icon.png");
            // 
            // ActivityManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainLayout);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ActivityManagement";
            this.Size = new System.Drawing.Size(473, 385);
            this.MainLayout.ResumeLayout(false);
            this.TabView.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ButtonLayout.ResumeLayout(false);
            this.ButtonLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ActivityListView;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.TableLayoutPanel ButtonLayout;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.ImageList ItemIcons;
        private System.Windows.Forms.TabControl TabView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

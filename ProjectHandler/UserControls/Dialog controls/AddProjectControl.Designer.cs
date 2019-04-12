namespace Projecthandler.Forms.Dialogs
{
    partial class AddProjectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProjectControl));
            this.UserIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.projectIDSelector = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.leaderSelector = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.StartDateSelector = new System.Windows.Forms.DateTimePicker();
            this.EndDateSelector = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DescriptionBoxSelector = new System.Windows.Forms.TextBox();
            this.MainLayout.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserIcons
            // 
            this.UserIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UserIcons.ImageStream")));
            this.UserIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.UserIcons.Images.SetKeyName(0, "User.png");
            // 
            // MainLayout
            // 
            this.MainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainLayout.BackColor = System.Drawing.Color.Silver;
            this.MainLayout.ColumnCount = 3;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.Controls.Add(this.label2, 0, 2);
            this.MainLayout.Controls.Add(this.label3, 0, 3);
            this.MainLayout.Controls.Add(this.label4, 0, 1);
            this.MainLayout.Controls.Add(this.projectIDSelector, 1, 1);
            this.MainLayout.Controls.Add(this.label5, 0, 4);
            this.MainLayout.Controls.Add(this.leaderSelector, 1, 4);
            this.MainLayout.Controls.Add(this.label6, 0, 0);
            this.MainLayout.Controls.Add(this.tableLayoutPanel2, 0, 6);
            this.MainLayout.Controls.Add(this.StartDateSelector, 1, 2);
            this.MainLayout.Controls.Add(this.EndDateSelector, 1, 3);
            this.MainLayout.Controls.Add(this.label1, 2, 0);
            this.MainLayout.Controls.Add(this.DescriptionBoxSelector, 2, 1);
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.Padding = new System.Windows.Forms.Padding(5);
            this.MainLayout.RowCount = 8;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.Size = new System.Drawing.Size(578, 371);
            this.MainLayout.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "End date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 40);
            this.label4.TabIndex = 3;
            this.label4.Text = "ProjectID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectIDSelector
            // 
            this.projectIDSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.projectIDSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectIDSelector.Location = new System.Drawing.Point(136, 42);
            this.projectIDSelector.Name = "projectIDSelector";
            this.projectIDSelector.Size = new System.Drawing.Size(186, 26);
            this.projectIDSelector.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 40);
            this.label5.TabIndex = 3;
            this.label5.Text = "Leader";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leaderSelector
            // 
            this.leaderSelector.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.leaderSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leaderSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leaderSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leaderSelector.FormattingEnabled = true;
            this.leaderSelector.Location = new System.Drawing.Point(136, 158);
            this.leaderSelector.Name = "leaderSelector";
            this.leaderSelector.Size = new System.Drawing.Size(186, 28);
            this.leaderSelector.Sorted = true;
            this.leaderSelector.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.MainLayout.SetColumnSpan(this.label6, 2);
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(320, 30);
            this.label6.TabIndex = 3;
            this.label6.Text = "Fill in information";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.MainLayout.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.linkLabel1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkLabel2, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 215);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(568, 13);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(536, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(32, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Save";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(493, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(40, 13);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Cancel";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // StartDateSelector
            // 
            this.StartDateSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartDateSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateSelector.Location = new System.Drawing.Point(136, 78);
            this.StartDateSelector.Name = "StartDateSelector";
            this.StartDateSelector.Size = new System.Drawing.Size(186, 26);
            this.StartDateSelector.TabIndex = 7;
            // 
            // EndDateSelector
            // 
            this.EndDateSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EndDateSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateSelector.Location = new System.Drawing.Point(136, 118);
            this.EndDateSelector.Name = "EndDateSelector";
            this.EndDateSelector.Size = new System.Drawing.Size(186, 26);
            this.EndDateSelector.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter short description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescriptionBoxSelector
            // 
            this.DescriptionBoxSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionBoxSelector.Location = new System.Drawing.Point(328, 38);
            this.DescriptionBoxSelector.Multiline = true;
            this.DescriptionBoxSelector.Name = "DescriptionBoxSelector";
            this.MainLayout.SetRowSpan(this.DescriptionBoxSelector, 4);
            this.DescriptionBoxSelector.Size = new System.Drawing.Size(242, 154);
            this.DescriptionBoxSelector.TabIndex = 8;
            // 
            // AddProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.MainLayout);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AddProjectControl";
            this.Size = new System.Drawing.Size(578, 371);
            this.MainLayout.ResumeLayout(false);
            this.MainLayout.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList UserIcons;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox projectIDSelector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox leaderSelector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.DateTimePicker StartDateSelector;
        private System.Windows.Forms.DateTimePicker EndDateSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DescriptionBoxSelector;
    }
}

namespace Projecthandler.Forms.Dialogs
{
    partial class AddActivityControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddActivityControl));
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.projectSelector = new System.Windows.Forms.ComboBox();
            this.UserIcons = new System.Windows.Forms.ImageList(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.UserListView = new System.Windows.Forms.ListView();
            this.AssignedUserListView = new System.Windows.Forms.ListView();
            this.Link_Remove = new System.Windows.Forms.LinkLabel();
            this.Link_Add = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.IDSelector = new System.Windows.Forms.TextBox();
            this.ButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.endWeekSelector = new System.Windows.Forms.ComboBox();
            this.startWeekSelector = new System.Windows.Forms.ComboBox();
            this.ButtonLayout.SuspendLayout();
            this.MainLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainLayout.SetColumnSpan(this.label9, 2);
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 190);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(533, 30);
            this.label9.TabIndex = 3;
            this.label9.Text = "Assign users to activity";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "End week:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start week:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectSelector
            // 
            this.projectSelector.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.projectSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectSelector.FormattingEnabled = true;
            this.projectSelector.Location = new System.Drawing.Point(131, 73);
            this.projectSelector.Name = "projectSelector";
            this.projectSelector.Size = new System.Drawing.Size(399, 28);
            this.projectSelector.Sorted = true;
            this.projectSelector.TabIndex = 2;
            // 
            // UserIcons
            // 
            this.UserIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UserIcons.ImageStream")));
            this.UserIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.UserIcons.Images.SetKeyName(0, "User.png");
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 40);
            this.label5.TabIndex = 3;
            this.label5.Text = "Project:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(240, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Available users";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(289, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(241, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Assigned users";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserListView
            // 
            this.UserListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.UserListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserListView.Location = new System.Drawing.Point(0, 20);
            this.UserListView.Margin = new System.Windows.Forms.Padding(0);
            this.UserListView.Name = "UserListView";
            this.tableLayoutPanel1.SetRowSpan(this.UserListView, 4);
            this.UserListView.Size = new System.Drawing.Size(246, 167);
            this.UserListView.SmallImageList = this.UserIcons;
            this.UserListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.UserListView.TabIndex = 1;
            this.UserListView.UseCompatibleStateImageBehavior = false;
            this.UserListView.View = System.Windows.Forms.View.SmallIcon;
            this.UserListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UserListView_MouseDoubleClick);
            // 
            // AssignedUserListView
            // 
            this.AssignedUserListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.AssignedUserListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AssignedUserListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AssignedUserListView.Location = new System.Drawing.Point(286, 20);
            this.AssignedUserListView.Margin = new System.Windows.Forms.Padding(0);
            this.AssignedUserListView.Name = "AssignedUserListView";
            this.tableLayoutPanel1.SetRowSpan(this.AssignedUserListView, 4);
            this.AssignedUserListView.Size = new System.Drawing.Size(247, 167);
            this.AssignedUserListView.SmallImageList = this.UserIcons;
            this.AssignedUserListView.TabIndex = 1;
            this.AssignedUserListView.UseCompatibleStateImageBehavior = false;
            this.AssignedUserListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Link_Remove
            // 
            this.Link_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Link_Remove.AutoSize = true;
            this.Link_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Link_Remove.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Link_Remove.Location = new System.Drawing.Point(249, 40);
            this.Link_Remove.Name = "Link_Remove";
            this.Link_Remove.Size = new System.Drawing.Size(34, 20);
            this.Link_Remove.TabIndex = 4;
            this.Link_Remove.TabStop = true;
            this.Link_Remove.Text = "<<";
            this.Link_Remove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Link_Remove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Remove_LinkClicked);
            // 
            // Link_Add
            // 
            this.Link_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Link_Add.AutoSize = true;
            this.Link_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Link_Add.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Link_Add.Location = new System.Drawing.Point(249, 60);
            this.Link_Add.Name = "Link_Add";
            this.Link_Add.Size = new System.Drawing.Size(34, 20);
            this.Link_Add.TabIndex = 5;
            this.Link_Add.TabStop = true;
            this.Link_Add.Text = ">>";
            this.Link_Add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Link_Add.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Add_LinkClicked);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 40);
            this.label4.TabIndex = 3;
            this.label4.Text = "Activity ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IDSelector
            // 
            this.IDSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.IDSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDSelector.Location = new System.Drawing.Point(131, 37);
            this.IDSelector.Name = "IDSelector";
            this.IDSelector.Size = new System.Drawing.Size(399, 26);
            this.IDSelector.TabIndex = 4;
            // 
            // ButtonLayout
            // 
            this.ButtonLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLayout.ColumnCount = 3;
            this.MainLayout.SetColumnSpan(this.ButtonLayout, 2);
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.Controls.Add(this.linkLabel1, 2, 0);
            this.ButtonLayout.Controls.Add(this.linkLabel2, 1, 0);
            this.ButtonLayout.Location = new System.Drawing.Point(3, 438);
            this.ButtonLayout.Name = "ButtonLayout";
            this.ButtonLayout.RowCount = 1;
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.Size = new System.Drawing.Size(527, 26);
            this.ButtonLayout.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(456, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(68, 26);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Save activity";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(410, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(40, 26);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Cancel";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // MainLayout
            // 
            this.MainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLayout.BackColor = System.Drawing.Color.Sienna;
            this.MainLayout.ColumnCount = 2;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 405F));
            this.MainLayout.Controls.Add(this.label4, 0, 1);
            this.MainLayout.Controls.Add(this.IDSelector, 1, 1);
            this.MainLayout.Controls.Add(this.tableLayoutPanel1, 0, 6);
            this.MainLayout.Controls.Add(this.label6, 0, 0);
            this.MainLayout.Controls.Add(this.label9, 0, 5);
            this.MainLayout.Controls.Add(this.endWeekSelector, 1, 4);
            this.MainLayout.Controls.Add(this.startWeekSelector, 1, 3);
            this.MainLayout.Controls.Add(this.label3, 0, 4);
            this.MainLayout.Controls.Add(this.label2, 0, 3);
            this.MainLayout.Controls.Add(this.label5, 0, 2);
            this.MainLayout.Controls.Add(this.projectSelector, 1, 2);
            this.MainLayout.Controls.Add(this.ButtonLayout, 0, 8);
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 9;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainLayout.Size = new System.Drawing.Size(533, 467);
            this.MainLayout.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.MainLayout.SetColumnSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.UserListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.AssignedUserListView, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Link_Remove, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Link_Add, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 220);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 187);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainLayout.SetColumnSpan(this.label6, 2);
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(533, 30);
            this.label6.TabIndex = 3;
            this.label6.Text = "Fill in information";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // endWeekSelector
            // 
            this.endWeekSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endWeekSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endWeekSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endWeekSelector.FormattingEnabled = true;
            this.endWeekSelector.Location = new System.Drawing.Point(131, 153);
            this.endWeekSelector.Name = "endWeekSelector";
            this.endWeekSelector.Size = new System.Drawing.Size(399, 28);
            this.endWeekSelector.TabIndex = 2;
            // 
            // startWeekSelector
            // 
            this.startWeekSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startWeekSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startWeekSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startWeekSelector.FormattingEnabled = true;
            this.startWeekSelector.Location = new System.Drawing.Point(131, 113);
            this.startWeekSelector.Name = "startWeekSelector";
            this.startWeekSelector.Size = new System.Drawing.Size(399, 28);
            this.startWeekSelector.TabIndex = 1;
            // 
            // AddActivityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.MainLayout);
            this.Name = "AddActivityControl";
            this.Size = new System.Drawing.Size(533, 467);
            this.ButtonLayout.ResumeLayout(false);
            this.ButtonLayout.PerformLayout();
            this.MainLayout.ResumeLayout(false);
            this.MainLayout.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox IDSelector;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView UserListView;
        private System.Windows.Forms.ImageList UserIcons;
        private System.Windows.Forms.ListView AssignedUserListView;
        private System.Windows.Forms.LinkLabel Link_Remove;
        private System.Windows.Forms.LinkLabel Link_Add;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox projectSelector;
        private System.Windows.Forms.TableLayoutPanel ButtonLayout;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ComboBox endWeekSelector;
        private System.Windows.Forms.ComboBox startWeekSelector;
    }
}

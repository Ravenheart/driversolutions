namespace DriverSolutions.ModuleDriver
{
    partial class XF_DriverLicenseNewEdit
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlReminders = new DevExpress.XtraGrid.GridControl();
            this.gridViewReminders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_ReminderID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_ReminderID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.col_ReminderType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_ReminderType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.col_ShouldRemind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Delete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Delete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Permits = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.MVRReviewDate = new DevExpress.XtraEditors.DateEdit();
            this.ExpirationDate = new DevExpress.XtraEditors.DateEdit();
            this.IssueDate = new DevExpress.XtraEditors.DateEdit();
            this.LicenseID = new DevExpress.XtraEditors.LookUpEdit();
            this.DriverID = new DriverSolutions.ModuleSystem.XU_Driver();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Permits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVRReviewDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVRReviewDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpirationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpirationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gridControlReminders);
            this.layoutControl1.Controls.Add(this.Permits);
            this.layoutControl1.Controls.Add(this.MVRReviewDate);
            this.layoutControl1.Controls.Add(this.ExpirationDate);
            this.layoutControl1.Controls.Add(this.IssueDate);
            this.layoutControl1.Controls.Add(this.LicenseID);
            this.layoutControl1.Controls.Add(this.DriverID);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(680, 254, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 496);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DriverSolutions.Properties.Resources.delete_32x32;
            this.btnDelete.Location = new System.Drawing.Point(2, 456);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(111, 38);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::DriverSolutions.Properties.Resources.close_32x32;
            this.btnCancel.Location = new System.Drawing.Point(566, 456);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 38);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DriverSolutions.Properties.Resources.save_32x32;
            this.btnSave.Location = new System.Drawing.Point(423, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 38);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gridControlReminders
            // 
            this.gridControlReminders.Location = new System.Drawing.Point(2, 138);
            this.gridControlReminders.MainView = this.gridViewReminders;
            this.gridControlReminders.Name = "gridControlReminders";
            this.gridControlReminders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_ReminderID,
            this.rep_ReminderType,
            this.rep_Delete});
            this.gridControlReminders.Size = new System.Drawing.Size(425, 314);
            this.gridControlReminders.TabIndex = 11;
            this.gridControlReminders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewReminders});
            // 
            // gridViewReminders
            // 
            this.gridViewReminders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_ReminderID,
            this.col_ReminderType,
            this.col_ShouldRemind,
            this.col_Delete});
            this.gridViewReminders.GridControl = this.gridControlReminders;
            this.gridViewReminders.Name = "gridViewReminders";
            this.gridViewReminders.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewReminders.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewReminders.OptionsView.ShowGroupPanel = false;
            // 
            // col_ReminderID
            // 
            this.col_ReminderID.Caption = "Reminder";
            this.col_ReminderID.ColumnEdit = this.rep_ReminderID;
            this.col_ReminderID.FieldName = "ReminderID";
            this.col_ReminderID.Name = "col_ReminderID";
            this.col_ReminderID.Visible = true;
            this.col_ReminderID.VisibleIndex = 0;
            this.col_ReminderID.Width = 130;
            // 
            // rep_ReminderID
            // 
            this.rep_ReminderID.AutoHeight = false;
            this.rep_ReminderID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rep_ReminderID.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display", "Reminder")});
            this.rep_ReminderID.DisplayMember = "Display";
            this.rep_ReminderID.Name = "rep_ReminderID";
            this.rep_ReminderID.NullText = "";
            this.rep_ReminderID.ValueMember = "Value";
            // 
            // col_ReminderType
            // 
            this.col_ReminderType.Caption = "Reminder Type";
            this.col_ReminderType.ColumnEdit = this.rep_ReminderType;
            this.col_ReminderType.FieldName = "ReminderType";
            this.col_ReminderType.Name = "col_ReminderType";
            this.col_ReminderType.Visible = true;
            this.col_ReminderType.VisibleIndex = 1;
            this.col_ReminderType.Width = 124;
            // 
            // rep_ReminderType
            // 
            this.rep_ReminderType.AutoHeight = false;
            this.rep_ReminderType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rep_ReminderType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display", "Reminder Type")});
            this.rep_ReminderType.DisplayMember = "Display";
            this.rep_ReminderType.Name = "rep_ReminderType";
            this.rep_ReminderType.NullText = "";
            this.rep_ReminderType.ValueMember = "Value";
            // 
            // col_ShouldRemind
            // 
            this.col_ShouldRemind.Caption = "Should remind";
            this.col_ShouldRemind.FieldName = "ShouldRemind";
            this.col_ShouldRemind.Name = "col_ShouldRemind";
            this.col_ShouldRemind.Visible = true;
            this.col_ShouldRemind.VisibleIndex = 2;
            this.col_ShouldRemind.Width = 97;
            // 
            // col_Delete
            // 
            this.col_Delete.Caption = "Delete";
            this.col_Delete.ColumnEdit = this.rep_Delete;
            this.col_Delete.Name = "col_Delete";
            this.col_Delete.OptionsColumn.AllowEdit = false;
            this.col_Delete.Visible = true;
            this.col_Delete.VisibleIndex = 3;
            this.col_Delete.Width = 56;
            // 
            // rep_Delete
            // 
            this.rep_Delete.AutoHeight = false;
            this.rep_Delete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.rep_Delete.Name = "rep_Delete";
            this.rep_Delete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // Permits
            // 
            this.Permits.Appearance.Options.UseTextOptions = true;
            this.Permits.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Permits.CheckOnClick = true;
            this.Permits.DisplayMember = "Display";
            this.Permits.ItemHeight = 30;
            this.Permits.Location = new System.Drawing.Point(431, 138);
            this.Permits.Name = "Permits";
            this.Permits.Size = new System.Drawing.Size(251, 314);
            this.Permits.StyleController = this.layoutControl1;
            this.Permits.TabIndex = 9;
            this.Permits.ValueMember = "Value";
            this.Permits.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.Permits_ItemCheck);
            // 
            // MVRReviewDate
            // 
            this.MVRReviewDate.EditValue = null;
            this.MVRReviewDate.Location = new System.Drawing.Point(86, 98);
            this.MVRReviewDate.Name = "MVRReviewDate";
            this.MVRReviewDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MVRReviewDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MVRReviewDate.Size = new System.Drawing.Size(596, 20);
            this.MVRReviewDate.StyleController = this.layoutControl1;
            this.MVRReviewDate.TabIndex = 8;
            // 
            // ExpirationDate
            // 
            this.ExpirationDate.EditValue = null;
            this.ExpirationDate.Location = new System.Drawing.Point(86, 74);
            this.ExpirationDate.Name = "ExpirationDate";
            this.ExpirationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExpirationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExpirationDate.Size = new System.Drawing.Size(596, 20);
            this.ExpirationDate.StyleController = this.layoutControl1;
            this.ExpirationDate.TabIndex = 7;
            // 
            // IssueDate
            // 
            this.IssueDate.EditValue = null;
            this.IssueDate.Location = new System.Drawing.Point(86, 50);
            this.IssueDate.Name = "IssueDate";
            this.IssueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.IssueDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.IssueDate.Size = new System.Drawing.Size(596, 20);
            this.IssueDate.StyleController = this.layoutControl1;
            this.IssueDate.TabIndex = 6;
            // 
            // LicenseID
            // 
            this.LicenseID.Location = new System.Drawing.Point(86, 26);
            this.LicenseID.Name = "LicenseID";
            this.LicenseID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LicenseID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display", "License")});
            this.LicenseID.Properties.DisplayMember = "Display";
            this.LicenseID.Properties.NullText = "";
            this.LicenseID.Properties.ValueMember = "Value";
            this.LicenseID.Size = new System.Drawing.Size(596, 20);
            this.LicenseID.StyleController = this.layoutControl1;
            this.LicenseID.TabIndex = 5;
            // 
            // DriverID
            // 
            this.DriverID.DriverID = ((uint)(0u));
            this.DriverID.Location = new System.Drawing.Point(86, 2);
            this.DriverID.Name = "DriverID";
            this.DriverID.ReadOnly = false;
            this.DriverID.Size = new System.Drawing.Size(596, 20);
            this.DriverID.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.emptySpaceItem1,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 496);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.DriverID;
            this.layoutControlItem1.CustomizationFormText = "Driver:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(201, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(684, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Driver:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.LicenseID;
            this.layoutControlItem2.CustomizationFormText = "License class:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(684, 24);
            this.layoutControlItem2.Text = "License class:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.IssueDate;
            this.layoutControlItem3.CustomizationFormText = "Issue date:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(684, 24);
            this.layoutControlItem3.Text = "Issue date:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ExpirationDate;
            this.layoutControlItem4.CustomizationFormText = "Expiration date:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(684, 24);
            this.layoutControlItem4.Text = "Expiration date:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.MVRReviewDate;
            this.layoutControlItem5.CustomizationFormText = "MVR review date";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(684, 24);
            this.layoutControlItem5.Text = "MVR review date";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.Permits;
            this.layoutControlItem6.CustomizationFormText = "Endorsements:";
            this.layoutControlItem6.Location = new System.Drawing.Point(429, 120);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(255, 334);
            this.layoutControlItem6.Text = "Endorsements:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gridControlReminders;
            this.layoutControlItem8.CustomizationFormText = "Reminders:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(429, 334);
            this.layoutControlItem8.Text = "Reminders:";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(421, 454);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(0, 42);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(39, 42);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(143, 42);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnCancel;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(564, 454);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(0, 42);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(41, 42);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(120, 42);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(115, 454);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(306, 42);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnDelete;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 454);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(0, 42);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(46, 42);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(115, 42);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // XF_DriverLicenseNewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 496);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XF_DriverLicenseNewEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "License";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Permits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVRReviewDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVRReviewDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpirationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpirationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ModuleSystem.XU_Driver DriverID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit LicenseID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit MVRReviewDate;
        private DevExpress.XtraEditors.DateEdit ExpirationDate;
        private DevExpress.XtraEditors.DateEdit IssueDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckedListBoxControl Permits;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.GridControl gridControlReminders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewReminders;
        private DevExpress.XtraGrid.Columns.GridColumn col_ReminderID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep_ReminderID;
        private DevExpress.XtraGrid.Columns.GridColumn col_ReminderType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep_ReminderType;
        private DevExpress.XtraGrid.Columns.GridColumn col_ShouldRemind;
        private DevExpress.XtraGrid.Columns.GridColumn col_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Delete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
    }
}
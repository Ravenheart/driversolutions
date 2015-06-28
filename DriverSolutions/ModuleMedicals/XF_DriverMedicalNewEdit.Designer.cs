namespace DriverSolutions.ModuleMedicals
{
    partial class XF_DriverMedicalNewEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XF_DriverMedicalNewEdit));
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
            this.ValidityDate = new DevExpress.XtraEditors.DateEdit();
            this.ExaminationDate = new DevExpress.XtraEditors.DateEdit();
            this.MedTypeID = new DevExpress.XtraEditors.LookUpEdit();
            this.DriverID = new DriverSolutions.ModuleSystem.XU_Driver();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidityDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidityDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExaminationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExaminationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MedTypeID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gridControlReminders);
            this.layoutControl1.Controls.Add(this.ValidityDate);
            this.layoutControl1.Controls.Add(this.ExaminationDate);
            this.layoutControl1.Controls.Add(this.MedTypeID);
            this.layoutControl1.Controls.Add(this.DriverID);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(940, 167, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 462);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(5, 416);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(165, 41);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(513, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(166, 41);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(344, 416);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 41);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gridControlReminders
            // 
            this.gridControlReminders.Location = new System.Drawing.Point(5, 117);
            this.gridControlReminders.MainView = this.gridViewReminders;
            this.gridControlReminders.Name = "gridControlReminders";
            this.gridControlReminders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_ReminderID,
            this.rep_ReminderType,
            this.rep_Delete});
            this.gridControlReminders.Size = new System.Drawing.Size(674, 295);
            this.gridControlReminders.TabIndex = 8;
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
            this.col_ReminderID.Width = 166;
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
            this.col_ReminderType.Width = 166;
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
            this.col_ShouldRemind.Width = 216;
            // 
            // col_Delete
            // 
            this.col_Delete.Caption = "Delete";
            this.col_Delete.ColumnEdit = this.rep_Delete;
            this.col_Delete.Name = "col_Delete";
            this.col_Delete.Visible = true;
            this.col_Delete.VisibleIndex = 3;
            this.col_Delete.Width = 116;
            // 
            // rep_Delete
            // 
            this.rep_Delete.AutoHeight = false;
            this.rep_Delete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.rep_Delete.Name = "rep_Delete";
            this.rep_Delete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // ValidityDate
            // 
            this.ValidityDate.EditValue = null;
            this.ValidityDate.Location = new System.Drawing.Point(95, 77);
            this.ValidityDate.Name = "ValidityDate";
            this.ValidityDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ValidityDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ValidityDate.Size = new System.Drawing.Size(584, 20);
            this.ValidityDate.StyleController = this.layoutControl1;
            this.ValidityDate.TabIndex = 7;
            // 
            // ExaminationDate
            // 
            this.ExaminationDate.EditValue = null;
            this.ExaminationDate.Location = new System.Drawing.Point(95, 53);
            this.ExaminationDate.Name = "ExaminationDate";
            this.ExaminationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExaminationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExaminationDate.Size = new System.Drawing.Size(584, 20);
            this.ExaminationDate.StyleController = this.layoutControl1;
            this.ExaminationDate.TabIndex = 6;
            // 
            // MedTypeID
            // 
            this.MedTypeID.Location = new System.Drawing.Point(95, 29);
            this.MedTypeID.Name = "MedTypeID";
            this.MedTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MedTypeID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display", "Medical type")});
            this.MedTypeID.Properties.DisplayMember = "Display";
            this.MedTypeID.Properties.NullText = "";
            this.MedTypeID.Properties.ValueMember = "Value";
            this.MedTypeID.Size = new System.Drawing.Size(584, 20);
            this.MedTypeID.StyleController = this.layoutControl1;
            this.MedTypeID.TabIndex = 5;
            // 
            // DriverID
            // 
            this.DriverID.DriverID = ((uint)(0u));
            this.DriverID.Location = new System.Drawing.Point(95, 5);
            this.DriverID.Name = "DriverID";
            this.DriverID.ReadOnly = false;
            this.DriverID.Size = new System.Drawing.Size(584, 20);
            this.DriverID.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 462);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.DriverID;
            this.layoutControlItem1.CustomizationFormText = "Driver:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(678, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Driver:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(87, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(169, 411);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(170, 45);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.MedTypeID;
            this.layoutControlItem2.CustomizationFormText = "Medical type:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(678, 24);
            this.layoutControlItem2.Text = "Medical type:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ExaminationDate;
            this.layoutControlItem3.CustomizationFormText = "Examination date:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(678, 24);
            this.layoutControlItem3.Text = "Examination date:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ValidityDate;
            this.layoutControlItem4.CustomizationFormText = "Validity date:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(678, 24);
            this.layoutControlItem4.Text = "Validity date:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControlReminders;
            this.layoutControlItem5.CustomizationFormText = "Reminders";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(678, 315);
            this.layoutControlItem5.Text = "Reminders";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(339, 411);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 45);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(82, 45);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(169, 45);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(508, 411);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(0, 45);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(82, 45);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(170, 45);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnDelete;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 411);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 45);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(82, 45);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(169, 45);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // XF_DriverMedicalNewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XF_DriverMedicalNewEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medical";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ReminderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidityDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidityDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExaminationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExaminationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MedTypeID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ModuleSystem.XU_Driver DriverID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gridControlReminders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewReminders;
        private DevExpress.XtraEditors.DateEdit ValidityDate;
        private DevExpress.XtraEditors.DateEdit ExaminationDate;
        private DevExpress.XtraEditors.LookUpEdit MedTypeID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn col_ReminderID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep_ReminderID;
        private DevExpress.XtraGrid.Columns.GridColumn col_ReminderType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep_ReminderType;
        private DevExpress.XtraGrid.Columns.GridColumn col_ShouldRemind;
        private DevExpress.XtraGrid.Columns.GridColumn col_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Delete;
    }
}
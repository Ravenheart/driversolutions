namespace DriverSolutions.ModuleDriver
{
    partial class XU_DriverLicenses
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnNewLicense = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlLicenses = new DevExpress.XtraGrid.GridControl();
            this.gridViewLicenses = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_LicenseID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_LicenseID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.col_IssueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ExpirationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MVRReviewDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Edit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Edit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.col_Renew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Renew = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.col_Delete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Delete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLicenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLicenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_LicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Renew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnNewLicense);
            this.layoutControl1.Controls.Add(this.gridControlLicenses);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(578, 223, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(782, 452);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnNewLicense
            // 
            this.btnNewLicense.Image = global::DriverSolutions.Properties.Resources.add_32x32;
            this.btnNewLicense.Location = new System.Drawing.Point(547, 5);
            this.btnNewLicense.Name = "btnNewLicense";
            this.btnNewLicense.Size = new System.Drawing.Size(230, 38);
            this.btnNewLicense.StyleController = this.layoutControl1;
            this.btnNewLicense.TabIndex = 5;
            this.btnNewLicense.Text = "New license";
            this.btnNewLicense.Click += new System.EventHandler(this.btnNewLicense_Click);
            // 
            // gridControlLicenses
            // 
            this.gridControlLicenses.Location = new System.Drawing.Point(5, 47);
            this.gridControlLicenses.MainView = this.gridViewLicenses;
            this.gridControlLicenses.Name = "gridControlLicenses";
            this.gridControlLicenses.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_LicenseID,
            this.rep_Delete,
            this.rep_Renew,
            this.rep_Edit});
            this.gridControlLicenses.Size = new System.Drawing.Size(772, 400);
            this.gridControlLicenses.TabIndex = 4;
            this.gridControlLicenses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLicenses});
            // 
            // gridViewLicenses
            // 
            this.gridViewLicenses.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_LicenseID,
            this.col_IssueDate,
            this.col_ExpirationDate,
            this.col_MVRReviewDate,
            this.col_Edit,
            this.col_Renew,
            this.col_Delete});
            this.gridViewLicenses.GridControl = this.gridControlLicenses;
            this.gridViewLicenses.Name = "gridViewLicenses";
            this.gridViewLicenses.OptionsBehavior.Editable = false;
            this.gridViewLicenses.OptionsDetail.AllowZoomDetail = false;
            this.gridViewLicenses.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewLicenses.OptionsDetail.ShowDetailTabs = false;
            this.gridViewLicenses.OptionsDetail.SmartDetailExpand = false;
            this.gridViewLicenses.OptionsView.ShowGroupPanel = false;
            // 
            // col_LicenseID
            // 
            this.col_LicenseID.Caption = "License";
            this.col_LicenseID.ColumnEdit = this.rep_LicenseID;
            this.col_LicenseID.FieldName = "LicenseID";
            this.col_LicenseID.Name = "col_LicenseID";
            this.col_LicenseID.Visible = true;
            this.col_LicenseID.VisibleIndex = 0;
            this.col_LicenseID.Width = 113;
            // 
            // rep_LicenseID
            // 
            this.rep_LicenseID.AutoHeight = false;
            this.rep_LicenseID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rep_LicenseID.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display", "Type")});
            this.rep_LicenseID.DisplayMember = "Display";
            this.rep_LicenseID.Name = "rep_LicenseID";
            this.rep_LicenseID.NullText = "";
            this.rep_LicenseID.ValueMember = "Value";
            // 
            // col_IssueDate
            // 
            this.col_IssueDate.Caption = "Issue date";
            this.col_IssueDate.FieldName = "IssueDate";
            this.col_IssueDate.Name = "col_IssueDate";
            this.col_IssueDate.Visible = true;
            this.col_IssueDate.VisibleIndex = 1;
            this.col_IssueDate.Width = 125;
            // 
            // col_ExpirationDate
            // 
            this.col_ExpirationDate.Caption = "Expiration date";
            this.col_ExpirationDate.FieldName = "ExpirationDate";
            this.col_ExpirationDate.Name = "col_ExpirationDate";
            this.col_ExpirationDate.Visible = true;
            this.col_ExpirationDate.VisibleIndex = 2;
            this.col_ExpirationDate.Width = 125;
            // 
            // col_MVRReviewDate
            // 
            this.col_MVRReviewDate.Caption = "MVR review date";
            this.col_MVRReviewDate.FieldName = "MVRReviewDate";
            this.col_MVRReviewDate.Name = "col_MVRReviewDate";
            this.col_MVRReviewDate.Visible = true;
            this.col_MVRReviewDate.VisibleIndex = 3;
            this.col_MVRReviewDate.Width = 167;
            // 
            // col_Edit
            // 
            this.col_Edit.Caption = "Edit";
            this.col_Edit.ColumnEdit = this.rep_Edit;
            this.col_Edit.Name = "col_Edit";
            this.col_Edit.Visible = true;
            this.col_Edit.VisibleIndex = 4;
            this.col_Edit.Width = 72;
            // 
            // rep_Edit
            // 
            this.rep_Edit.AutoHeight = false;
            this.rep_Edit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Edit", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, global::DriverSolutions.Properties.Resources.edit_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.rep_Edit.Name = "rep_Edit";
            this.rep_Edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // col_Renew
            // 
            this.col_Renew.Caption = "Renew";
            this.col_Renew.ColumnEdit = this.rep_Renew;
            this.col_Renew.Name = "col_Renew";
            this.col_Renew.Visible = true;
            this.col_Renew.VisibleIndex = 5;
            this.col_Renew.Width = 68;
            // 
            // rep_Renew
            // 
            this.rep_Renew.AutoHeight = false;
            this.rep_Renew.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Renew", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, global::DriverSolutions.Properties.Resources.refresh2_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.rep_Renew.Name = "rep_Renew";
            this.rep_Renew.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // col_Delete
            // 
            this.col_Delete.Caption = "Delete";
            this.col_Delete.ColumnEdit = this.rep_Delete;
            this.col_Delete.Name = "col_Delete";
            this.col_Delete.Visible = true;
            this.col_Delete.VisibleIndex = 6;
            this.col_Delete.Width = 80;
            // 
            // rep_Delete
            // 
            this.rep_Delete.AutoHeight = false;
            this.rep_Delete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, global::DriverSolutions.Properties.Resources.delete_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.rep_Delete.Name = "rep_Delete";
            this.rep_Delete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(782, 452);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlLicenses;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(776, 404);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnNewLicense;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(542, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 42);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(82, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(234, 42);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(542, 42);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XU_DriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XU_DriverLicenses";
            this.Size = new System.Drawing.Size(782, 452);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLicenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLicenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_LicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Renew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnNewLicense;
        private DevExpress.XtraGrid.GridControl gridControlLicenses;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLicenses;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn col_LicenseID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep_LicenseID;
        private DevExpress.XtraGrid.Columns.GridColumn col_IssueDate;
        private DevExpress.XtraGrid.Columns.GridColumn col_ExpirationDate;
        private DevExpress.XtraGrid.Columns.GridColumn col_MVRReviewDate;
        private DevExpress.XtraGrid.Columns.GridColumn col_Renew;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Renew;
        private DevExpress.XtraGrid.Columns.GridColumn col_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Delete;
        private DevExpress.XtraGrid.Columns.GridColumn col_Edit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Edit;
    }
}

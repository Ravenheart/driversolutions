namespace DriverSolutions.ModuleSystem
{
    partial class XU_FileBlobs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XU_FileBlobs));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
            this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAttachFile = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAttachMultipleFiles = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.col_BlobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_BlobDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_BlobExtension = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_BlobSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Delete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Preview = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Delete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rep_Preview = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.col_Edit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_Edit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Edit)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnAttachMultipleFiles);
            this.layoutControl1.Controls.Add(this.btnAttachFile);
            this.layoutControl1.Controls.Add(this.gridControlFiles);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(531, 264, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(833, 493);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(833, 493);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gridControlFiles
            // 
            this.gridControlFiles.Location = new System.Drawing.Point(5, 46);
            this.gridControlFiles.MainView = this.gridViewFiles;
            this.gridControlFiles.Name = "gridControlFiles";
            this.gridControlFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_Delete,
            this.rep_Preview,
            this.rep_Edit});
            this.gridControlFiles.Size = new System.Drawing.Size(823, 442);
            this.gridControlFiles.TabIndex = 4;
            this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
            // 
            // gridViewFiles
            // 
            this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_BlobName,
            this.col_BlobDescription,
            this.col_BlobExtension,
            this.col_BlobSize,
            this.col_Preview,
            this.col_Edit,
            this.col_Delete});
            this.gridViewFiles.GridControl = this.gridControlFiles;
            this.gridViewFiles.Name = "gridViewFiles";
            this.gridViewFiles.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlFiles;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(827, 446);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachFile.Image")));
            this.btnAttachFile.Location = new System.Drawing.Point(479, 5);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(180, 37);
            this.btnAttachFile.StyleController = this.layoutControl1;
            this.btnAttachFile.TabIndex = 5;
            this.btnAttachFile.Text = "Attach File";
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnAttachFile;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(474, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 41);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(82, 41);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(184, 41);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // btnAttachMultipleFiles
            // 
            this.btnAttachMultipleFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachMultipleFiles.Image")));
            this.btnAttachMultipleFiles.Location = new System.Drawing.Point(663, 5);
            this.btnAttachMultipleFiles.Name = "btnAttachMultipleFiles";
            this.btnAttachMultipleFiles.Size = new System.Drawing.Size(165, 37);
            this.btnAttachMultipleFiles.StyleController = this.layoutControl1;
            this.btnAttachMultipleFiles.TabIndex = 6;
            this.btnAttachMultipleFiles.Text = "Attach Multiple Files";
            this.btnAttachMultipleFiles.Click += new System.EventHandler(this.btnAttachMultipleFiles_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnAttachMultipleFiles;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(658, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 41);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(82, 41);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(169, 41);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(474, 41);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // col_BlobName
            // 
            this.col_BlobName.Caption = "Name";
            this.col_BlobName.FieldName = "BlobName";
            this.col_BlobName.Name = "col_BlobName";
            this.col_BlobName.Visible = true;
            this.col_BlobName.VisibleIndex = 0;
            this.col_BlobName.Width = 145;
            // 
            // col_BlobDescription
            // 
            this.col_BlobDescription.Caption = "Description";
            this.col_BlobDescription.FieldName = "BlobDescription";
            this.col_BlobDescription.Name = "col_BlobDescription";
            this.col_BlobDescription.Visible = true;
            this.col_BlobDescription.VisibleIndex = 1;
            this.col_BlobDescription.Width = 313;
            // 
            // col_BlobExtension
            // 
            this.col_BlobExtension.Caption = "Extension";
            this.col_BlobExtension.FieldName = "BlobExtension";
            this.col_BlobExtension.Name = "col_BlobExtension";
            this.col_BlobExtension.OptionsColumn.AllowEdit = false;
            this.col_BlobExtension.Visible = true;
            this.col_BlobExtension.VisibleIndex = 2;
            this.col_BlobExtension.Width = 85;
            // 
            // col_BlobSize
            // 
            this.col_BlobSize.Caption = "Size";
            this.col_BlobSize.FieldName = "BlobSize";
            this.col_BlobSize.Name = "col_BlobSize";
            this.col_BlobSize.OptionsColumn.AllowEdit = false;
            this.col_BlobSize.Visible = true;
            this.col_BlobSize.VisibleIndex = 3;
            this.col_BlobSize.Width = 64;
            // 
            // col_Delete
            // 
            this.col_Delete.Caption = "Delete";
            this.col_Delete.ColumnEdit = this.rep_Delete;
            this.col_Delete.Name = "col_Delete";
            this.col_Delete.OptionsColumn.AllowEdit = false;
            this.col_Delete.Visible = true;
            this.col_Delete.VisibleIndex = 6;
            this.col_Delete.Width = 78;
            // 
            // col_Preview
            // 
            this.col_Preview.Caption = "Preview";
            this.col_Preview.ColumnEdit = this.rep_Preview;
            this.col_Preview.Name = "col_Preview";
            this.col_Preview.OptionsColumn.AllowEdit = false;
            this.col_Preview.Visible = true;
            this.col_Preview.VisibleIndex = 4;
            this.col_Preview.Width = 60;
            // 
            // rep_Delete
            // 
            this.rep_Delete.AutoHeight = false;
            this.rep_Delete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.rep_Delete.Name = "rep_Delete";
            this.rep_Delete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // rep_Preview
            // 
            this.rep_Preview.AutoHeight = false;
            this.rep_Preview.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Preview", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.rep_Preview.Name = "rep_Preview";
            this.rep_Preview.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // col_Edit
            // 
            this.col_Edit.Caption = "Edit";
            this.col_Edit.ColumnEdit = this.rep_Edit;
            this.col_Edit.Name = "col_Edit";
            this.col_Edit.OptionsColumn.AllowEdit = false;
            this.col_Edit.Visible = true;
            this.col_Edit.VisibleIndex = 5;
            this.col_Edit.Width = 60;
            // 
            // rep_Edit
            // 
            this.rep_Edit.AutoHeight = false;
            this.rep_Edit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Edit", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.rep_Edit.Name = "rep_Edit";
            this.rep_Edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // XU_FileBlobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XU_FileBlobs";
            this.Size = new System.Drawing.Size(833, 493);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Edit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnAttachMultipleFiles;
        private DevExpress.XtraEditors.SimpleButton btnAttachFile;
        private DevExpress.XtraGrid.GridControl gridControlFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn col_BlobName;
        private DevExpress.XtraGrid.Columns.GridColumn col_BlobDescription;
        private DevExpress.XtraGrid.Columns.GridColumn col_BlobExtension;
        private DevExpress.XtraGrid.Columns.GridColumn col_BlobSize;
        private DevExpress.XtraGrid.Columns.GridColumn col_Preview;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Preview;
        private DevExpress.XtraGrid.Columns.GridColumn col_Edit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Edit;
        private DevExpress.XtraGrid.Columns.GridColumn col_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Delete;
    }
}

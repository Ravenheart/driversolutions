namespace DriverSolutions.ModuleSystem
{
    partial class XU_Company
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
            this.CompanyCode = new DevExpress.XtraEditors.TextEdit();
            this.btnCompany = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // CompanyCode
            // 
            this.CompanyCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CompanyCode.Location = new System.Drawing.Point(0, 0);
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.Size = new System.Drawing.Size(50, 20);
            this.CompanyCode.TabIndex = 2;
            // 
            // btnCompany
            // 
            this.btnCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompany.Location = new System.Drawing.Point(56, 0);
            this.btnCompany.Name = "btnCompany";
            this.btnCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Search...", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnCompany.Properties.DisplayMember = "CompanyName";
            this.btnCompany.Properties.NullText = "";
            this.btnCompany.Properties.ValueMember = "CompanyID";
            this.btnCompany.Properties.View = this.searchLookUpEdit1View;
            this.btnCompany.Size = new System.Drawing.Size(294, 20);
            this.btnCompany.TabIndex = 3;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Code,
            this.col_Name});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // col_Code
            // 
            this.col_Code.Caption = "Company Code";
            this.col_Code.FieldName = "CompanyCode";
            this.col_Code.Name = "col_Code";
            this.col_Code.Visible = true;
            this.col_Code.VisibleIndex = 0;
            // 
            // col_Name
            // 
            this.col_Name.Caption = "Company Name";
            this.col_Name.FieldName = "CompanyName";
            this.col_Name.Name = "col_Name";
            this.col_Name.Visible = true;
            this.col_Name.VisibleIndex = 1;
            // 
            // XU_Company
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CompanyCode);
            this.Controls.Add(this.btnCompany);
            this.Name = "XU_Company";
            this.Size = new System.Drawing.Size(350, 20);
            ((System.ComponentModel.ISupportInitialize)(this.CompanyCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit CompanyCode;
        private DevExpress.XtraEditors.SearchLookUpEdit btnCompany;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn col_Code;
        private DevExpress.XtraGrid.Columns.GridColumn col_Name;
    }
}

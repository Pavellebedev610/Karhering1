namespace Karhering
{
    partial class Trips
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            PanelInfo = new Panel();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Button7
            // 
            guna2Button7.BackColor = Color.Transparent;
            guna2Button7.CustomizableEdges = customizableEdges1;
            guna2Button7.DisabledState.BorderColor = Color.DarkGray;
            guna2Button7.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button7.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button7.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button7.FillColor = Color.Transparent;
            guna2Button7.Font = new Font("Segoe UI", 9F);
            guna2Button7.ForeColor = Color.Transparent;
            guna2Button7.Image = Properties.Resources.cancel_cross_icon_icons_com_71726;
            guna2Button7.Location = new Point(3, 4);
            guna2Button7.Name = "guna2Button7";
            guna2Button7.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button7.Size = new Size(28, 25);
            guna2Button7.TabIndex = 1;
            guna2Button7.Click += guna2Button7_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(25, 37);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 2;
            label1.Text = "История";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 30;
            // 
            // PanelInfo
            // 
            PanelInfo.Location = new Point(12, 75);
            PanelInfo.Name = "PanelInfo";
            PanelInfo.Size = new Size(500, 498);
            PanelInfo.TabIndex = 3;
            // 
            // Trips
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(524, 585);
            Controls.Add(PanelInfo);
            Controls.Add(label1);
            Controls.Add(guna2Button7);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Trips";
            Text = "Trips";
            Load += Trips_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Button guna2Button7;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Panel PanelInfo;
    }
}
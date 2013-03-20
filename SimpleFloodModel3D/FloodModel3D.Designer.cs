namespace SimpleFloodModel3D
{
    partial class FloodModel3D
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.modelRenderPanel = new SimpleFloodModel3D.ModelRenderPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // modelRenderPanel
            // 
            this.modelRenderPanel.BackColor = System.Drawing.Color.Black;
            this.modelRenderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelRenderPanel.Location = new System.Drawing.Point(0, 0);
            this.modelRenderPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.modelRenderPanel.Name = "modelRenderPanel";
            this.modelRenderPanel.Size = new System.Drawing.Size(564, 416);
            this.modelRenderPanel.TabIndex = 0;
            this.modelRenderPanel.VSync = false;
            this.modelRenderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.modelRenderPanel_MouseDown);
            this.modelRenderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.modelRenderPanel_MouseMove);
            this.modelRenderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.modelRenderPanel_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.modelRenderPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonStep);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRandomize);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRun);
            this.splitContainer1.Size = new System.Drawing.Size(564, 582);
            this.splitContainer1.SplitterDistance = 416;
            this.splitContainer1.TabIndex = 1;
            // 
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(503, 3);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(49, 26);
            this.buttonRandomize.TabIndex = 1;
            this.buttonRandomize.Text = "rand";
            this.buttonRandomize.UseVisualStyleBackColor = true;
            this.buttonRandomize.Click += new System.EventHandler(this.buttonRandomize_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(12, 3);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(50, 26);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(79, 3);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(50, 26);
            this.buttonStep.TabIndex = 2;
            this.buttonStep.Text = "Step";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // FloodModel3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 582);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FloodModel3D";
            this.Text = "Flood3D";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ModelRenderPanel modelRenderPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonStep;
    }
}


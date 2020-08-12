namespace FlipFlop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainView1 = new FlipFlop.MainView();
            this.btnPlayGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainView1
            // 
            this.mainView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mainView1.BackgroundImage")));
            this.mainView1.Location = new System.Drawing.Point(0, 0);
            this.mainView1.Name = "mainView1";
            this.mainView1.Size = new System.Drawing.Size(935, 620);
            this.mainView1.TabIndex = 0;
            // 
            // btnPlayGame
            // 
            this.btnPlayGame.Location = new System.Drawing.Point(0, 620);
            this.btnPlayGame.Name = "btnPlayGame";
            this.btnPlayGame.Size = new System.Drawing.Size(935, 45);
            this.btnPlayGame.TabIndex = 1;
            this.btnPlayGame.Text = "Play Game";
            this.btnPlayGame.UseVisualStyleBackColor = true;
            this.btnPlayGame.Click += new System.EventHandler(this.btnPlayGame_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 665);
            this.Controls.Add(this.btnPlayGame);
            this.Controls.Add(this.mainView1);
            this.Name = "MainForm";
            this.Text = "FlipFlop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MainView mainView1;
        private System.Windows.Forms.Button btnPlayGame;
    }
}


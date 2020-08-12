using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlipFlop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainView.SoundManager.SetupSound();
            MainView.SoundManager.PlaySound(MainView.SoundManager.GSound.Main);

            this.mainView1.Enabled = false;


            this.mainView1.GameEnded += delegate 
            {
                if(MessageBox.Show("Game finished... quit or restart?", "Game End", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.mainView1.StartNewGame();
                }
                else
                {
                    Environment.Exit(0);
                }
            };
        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            this.mainView1.Enabled = true;
            this.mainView1.StartNewGame();
        }
    }
}

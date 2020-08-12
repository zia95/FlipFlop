using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace FlipFlop
{
    public partial class MainView : UserControl
    {
        public static class SoundManager
        {
            private static SoundPlayer sp_main = null;
            private static SoundPlayer sp_tile_clicked = null;
            private static SoundPlayer sp_tile_matched = null;
            private static SoundPlayer sp_tile_mismatched = null;
            private static SoundPlayer sp_game_start = null;
            private static SoundPlayer sp_game_end = null;

            private static string snd_main = Path.GetFullPath("snd\\main.wav");
            private static string snd_tile_clicked = Path.GetFullPath("snd\\tile_clicked.wav");
            private static string snd_tile_matched = Path.GetFullPath("snd\\tile_matched.wav");
            private static string snd_tile_mismatched = Path.GetFullPath("snd\\tile_mismatch.wav");
            private static string snd_game_start = Path.GetFullPath("snd\\game_start.wav");
            private static string snd_game_end = Path.GetFullPath("snd\\game_end.wav");

            

            public static bool OnOrOff { get; set; } = true;
            public static void SetupSound()
            {
                if (File.Exists(snd_main))
                    sp_main = new SoundPlayer(snd_main);
                else MessageBox.Show("Cannot find sound file (" + snd_main + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (File.Exists(snd_tile_clicked))
                    sp_tile_clicked = new SoundPlayer(snd_tile_clicked);
                else MessageBox.Show("Cannot find sound file (" + snd_tile_clicked + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (File.Exists(snd_tile_matched))
                    sp_tile_matched = new SoundPlayer(snd_tile_matched);
                else MessageBox.Show("Cannot find sound file (" + snd_tile_matched + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (File.Exists(snd_tile_mismatched))
                    sp_tile_mismatched = new SoundPlayer(snd_tile_mismatched);
                else MessageBox.Show("Cannot find sound file (" + snd_tile_mismatched + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (File.Exists(snd_game_start))
                    sp_game_start = new SoundPlayer(snd_game_start);
                else MessageBox.Show("Cannot find sound file (" + snd_game_start + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (File.Exists(snd_game_end))
                    sp_game_end = new SoundPlayer(snd_game_end);
                else MessageBox.Show("Cannot find sound file (" + snd_game_end + ")", "File not found (sound)", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                sp_main?.Load();
                sp_tile_clicked?.Load();
                sp_tile_matched?.Load();
                sp_tile_mismatched?.Load();
                sp_game_start?.Load();
                sp_game_end?.Load();
            }
            public static void Shutdown()
            {
                StopAllSounds();
                sp_main?.Dispose();
                sp_tile_clicked?.Dispose();
                sp_tile_matched?.Dispose();
                sp_tile_mismatched?.Dispose();
                sp_game_start?.Dispose();
                sp_game_end?.Dispose();
            }
            public enum GSound { Main, TileClicked, TileMatched, TileMismatch, GameStart, GameEnd };
            public static void PlaySound(GSound s)
            {
                if (OnOrOff == false)
                    return;
                
                if (s == GSound.Main)
                    sp_main?.PlayLooping();
                else if (s == GSound.TileClicked)
                    sp_tile_clicked?.Play();
                else if (s == GSound.TileMatched)
                    sp_tile_matched?.Play();
                else if (s == GSound.TileMismatch)
                    sp_tile_mismatched?.Play();
                else if (s == GSound.GameStart)
                    sp_game_start?.Play();
                else if (s == GSound.GameEnd)
                    sp_game_end?.Play();
            }
            public static void StopSound(GSound s)
            {
                if (s == GSound.Main)
                    sp_main?.Stop();
                else if (s == GSound.TileClicked)
                    sp_tile_clicked?.Stop();
                else if (s == GSound.TileClicked)
                    sp_tile_matched?.Stop();
                else if (s == GSound.TileClicked)
                    sp_tile_mismatched?.Stop();
                else if (s == GSound.GameStart)
                    sp_game_start?.Stop();
                else if (s == GSound.GameEnd)
                    sp_game_end?.Stop();
            }
            public static void StopAllSounds()
            {
                for(int i = 0; i < Enum.GetNames(typeof(GSound)).Length; i++)
                    StopSound((GSound)i);
            }
        }

        


        private List<Button> Tiles { get; }
        private ImageList TileImageList { get; }

        private Button LastClicked = null;

        public uint Score { get { return this._score; } }
        private uint _score;

        public bool IsGameRunning { get { return this._is_running; } }
        private bool _is_running;

        public event EventHandler GameEnded;
        public event EventHandler GameStarted;

        public MainView()
        {
            InitializeComponent();

            this.Tiles = new List<Button>();

            this.Tiles.Add(this.btnTile1x1);
            this.Tiles.Add(this.btnTile2x1);
            this.Tiles.Add(this.btnTile3x1);
            this.Tiles.Add(this.btnTile4x1);
            this.Tiles.Add(this.btnTile5x1);
            this.Tiles.Add(this.btnTile6x1);

            this.Tiles.Add(this.btnTile1x2);
            this.Tiles.Add(this.btnTile2x2);
            this.Tiles.Add(this.btnTile3x2);
            this.Tiles.Add(this.btnTile4x2);
            this.Tiles.Add(this.btnTile5x2); 
            this.Tiles.Add(this.btnTile6x2);

            this.Tiles.Add(this.btnTile1x3);
            this.Tiles.Add(this.btnTile2x3);
            this.Tiles.Add(this.btnTile3x3);
            this.Tiles.Add(this.btnTile4x3);
            this.Tiles.Add(this.btnTile5x3);
            this.Tiles.Add(this.btnTile6x3);

            //--------------------------------------------------------------------//

            string img_0 = System.IO.Path.GetFullPath("img\\0.jpg");
            string img_0A = System.IO.Path.GetFullPath("img\\0A.jpg");
            string img_1 = System.IO.Path.GetFullPath("img\\1.jpg");
            string img_2 = System.IO.Path.GetFullPath("img\\2.jpg");
            string img_3 = System.IO.Path.GetFullPath("img\\3.jpg");
            string img_4 = System.IO.Path.GetFullPath("img\\4.jpg");
            string img_5 = System.IO.Path.GetFullPath("img\\5.jpg");
            string img_6 = System.IO.Path.GetFullPath("img\\6.jpg");
            string img_7 = System.IO.Path.GetFullPath("img\\7.jpg");
            string img_8 = System.IO.Path.GetFullPath("img\\8.jpg");
            string img_9 = System.IO.Path.GetFullPath("img\\9.jpg");

            if(System.IO.File.Exists(img_0) &&
                System.IO.File.Exists(img_0A) &&
                System.IO.File.Exists(img_1) &&
                System.IO.File.Exists(img_2) &&
                System.IO.File.Exists(img_3) &&
                System.IO.File.Exists(img_4) &&
                System.IO.File.Exists(img_5) &&
                System.IO.File.Exists(img_6) &&
                System.IO.File.Exists(img_7) &&
                System.IO.File.Exists(img_8) &&
                System.IO.File.Exists(img_9))
            {
                Image oimg_0 = Image.FromFile(img_0);
                Image oimg_0A = Image.FromFile(img_0A);
                Image oimg_1 = Image.FromFile(img_1);
                Image oimg_2 = Image.FromFile(img_2);
                Image oimg_3 = Image.FromFile(img_3);
                Image oimg_4 = Image.FromFile(img_4);
                Image oimg_5 = Image.FromFile(img_5);
                Image oimg_6 = Image.FromFile(img_6);
                Image oimg_7 = Image.FromFile(img_7);
                Image oimg_8 = Image.FromFile(img_8);
                Image oimg_9 = Image.FromFile(img_9);

                this.BackgroundImage = oimg_0A;//set back_ground_img

                oimg_0.Tag = 0;
                oimg_1.Tag = 1;
                oimg_2.Tag = 2;
                oimg_3.Tag = 3;
                oimg_4.Tag = 4;
                oimg_5.Tag = 5;
                oimg_6.Tag = 6;
                oimg_7.Tag = 7;
                oimg_8.Tag = 8;
                oimg_9.Tag = 9;

                this.TileImageList = new ImageList();

                this.TileImageList.Images.Add(oimg_0);
                this.TileImageList.Images.Add(oimg_1);
                this.TileImageList.Images.Add(oimg_2);
                this.TileImageList.Images.Add(oimg_3);
                this.TileImageList.Images.Add(oimg_4);
                this.TileImageList.Images.Add(oimg_5);
                this.TileImageList.Images.Add(oimg_6);
                this.TileImageList.Images.Add(oimg_7);
                this.TileImageList.Images.Add(oimg_8);
                this.TileImageList.Images.Add(oimg_9);
            }
            else
            {
                MessageBox.Show("Cannot locate image file(s)... (Make sure all images are in " + System.IO.Path.GetDirectoryName(img_0) + ")", "Image file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);

#if !DEBUG
                Environment.Exit(101);
#endif
            }


        }
        
        public void StartNewGame()
        {
            SoundManager.PlaySound(SoundManager.GSound.GameStart);
            List<byte> seq = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rand = new Random();
            

            foreach (var t in this.Tiles)
            {
                t.ImageList = TileImageList;
                t.ImageIndex = 0;
                t.ImageList.ImageSize = t.Size;
                t.Text = string.Empty;
                t.Enabled = true;
                t.Visible = true;

                int rand_index = rand.Next(0, (seq.Count() - 1));
                t.Tag = seq[rand_index].ToString();// store as string
                seq.RemoveAt(rand_index);
            }

            this._is_running = true;
            this.GameStarted?.Invoke(this, new EventArgs());
        }
        private void TileClicked(Button tile)
        {
            SoundManager.PlaySound(SoundManager.GSound.TileClicked);

            tile.ImageIndex = int.Parse((string)tile.Tag);//switch img
            tile.Refresh();// make sure image is displayed

            System.Threading.Thread.Sleep(500);//sleep for a half sec
            if (this.LastClicked != null)
            {
                if (int.Parse((string)this.LastClicked.Tag) == int.Parse((string)tile.Tag))
                {
                    SoundManager.PlaySound(SoundManager.GSound.TileMatched);
                    this.LastClicked.Visible = false;
                    tile.Visible = false;
                    _score++;
                }
                else
                {
                    SoundManager.PlaySound(SoundManager.GSound.TileMismatch);
                    this.LastClicked.ImageIndex = 0;
                    tile.ImageIndex = 0;
                }
                this.LastClicked = null;
            }
            else
            {
                this.LastClicked = tile;
            }

            for(int i = 0; i < this.Tiles.Count; i++)
            {
                if (this.Tiles[i].Visible)
                    break;

                if(this.Tiles[i].Visible == false && i == (this.Tiles.Count - 1))
                {
                    this._is_running = false;
                    this.GameEnded?.Invoke(this, new EventArgs());
                }
            }

            
        }
        private void MainView_Load(object sender, EventArgs e)
        {
            this.Disposed += delegate { SoundManager.Shutdown(); };

            foreach (var t in this.Tiles)
            {
                t.ImageList = TileImageList;
                t.ImageIndex = 0;
                t.ImageList.ImageSize = t.Size;
                t.Text = string.Empty;
                t.Enabled = true;
                t.Visible = true;
                t.Click += delegate { TileClicked(t); };
            }

            //this.StartNewGame();
        }
    }
}

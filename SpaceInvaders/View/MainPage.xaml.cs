using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 480;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 960;

        private int fireRate;
        private int powerUpDuration;
        private readonly GameManager gameManager;
        private bool[] leftright;
        DispatcherTimer enemyTimer;
        DispatcherTimer playerTimer = new DispatcherTimer();
        private bool isPaused = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            
            this.InitializeComponent();
            enemyTimer = new DispatcherTimer();
            enemyTimer.Tick += this.timeTick;
            enemyTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            enemyTimer.Start();
            
            playerTimer.Tick += this.timeTickPlayer;
            playerTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            playerTimer.Start();
            leftright = new bool[3];
            this.powerUpDuration = 0;
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            Window.Current.CoreWindow.KeyUp += this.coreWindowOnKeyUp;
            

            this.gameManager = new GameManager(ApplicationHeight, ApplicationWidth);
            this.gameManager.InitializeGame(this.theCanvas);
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                
                case VirtualKey.Left:
                    this.leftright[0] = true;
                    break;
                case VirtualKey.Right:
                    this.leftright[1] = true;
                    break;
                case VirtualKey.Space:
                    this.leftright[2] = true;

                    break;
                case VirtualKey.P:
                    this.pauseGame();
                    break;
                case VirtualKey.U:
                    this.unpauseGame();
                    break;
            }
        }
        private void coreWindowOnKeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.leftright[0] = false;
                    break;
                case VirtualKey.Right:
                    this.leftright[1] = false;
                    break;
                case VirtualKey.Space:
                    this.leftright[2] = false;
                    break;
            }
        }

        private void timeTickPlayer(object sender, object e)
        {
            if (this.leftright[0])
            {
                this.gameManager.MovePlayerShipLeft();
            }

            if (this.leftright[1])
            {
                this.gameManager.MovePlayerShipRight();
            }

            if (this.leftright[2])
            {
                if (this.fireRate > 2)
                {
                    this.gameManager.FirePlayerBullet();
                    this.fireRate = 0;
                }
            }
        }

        private void timeTick(object sender, object e)
        {
            this.fireRate++;

            this.gameManager.EnemyManager.OnTick();

            if (this.gameManager.GameOver)
            {
                this.gameManager.HandleGameOver();
            }
            else
            {
                this.gameManager.MoveElements();

                this.gameManager.CheckForCollisions();
            }

            if (this.gameManager.PowerUp)
            {
                this.powerUpDuration++;
                
                if (this.powerUpDuration >= 50)
                {
                    this.gameManager.EndPowerUp();
                    this.powerUpDuration = 0;
                    
                }
            }
        }

        private void pauseGame()
        {
            this.isPaused = true;
            if (this.isPaused == true)
            {
                this.enemyTimer.Stop();
                this.playerTimer.Stop();
            }
        }

        private void unpauseGame()
        {
            if (this.isPaused)
            {
                this.enemyTimer.Start();
                this.playerTimer.Start();
            }
        }

        #endregion
    }
}
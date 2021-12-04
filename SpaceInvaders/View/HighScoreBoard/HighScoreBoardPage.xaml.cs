using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders.View.HighScoreBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScoreBoardPage : Page
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreBoardPage"/> class.
        /// </summary>
        public HighScoreBoardPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SortByScore();
        }

        private void SortByScore()
        {

            List<String[]> list = HighScoreSettings.SortByScore();
            for (int i = 0; i < 10; i++)
            {


                this.FirstListView.Items.Add(list[i][0]);
                this.SecondListView.Items.Add(list[i][1]);
                this.ThirdListView.Items.Add(list[i][2]);

            }
        }
        private void SortByLevel()
        {
            List<String[]> list = HighScoreSettings.SortByLevel();
            for (int i = 0; i < 10; i++)
            {
                
                    
                    this.FirstListView.Items.Add(list[i][2]);
                    this.SecondListView.Items.Add(list[i][0]);
                    this.ThirdListView.Items.Add(list[i][1]);
                
            }
        }
        private void SortByPlayer()
        {
            List<String[]> list = HighScoreSettings.SortByPlayer();
            for (int i = 0; i < 10; i++)
            {


                this.FirstListView.Items.Add(list[i][1]);
                this.SecondListView.Items.Add(list[i][0]);
                this.ThirdListView.Items.Add(list[i][2]);

            }
        }

        private void SortByScoreButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.SortByScore();
        }
        private void SortByLevelButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.SortByLevel();
        }
        private void SortByPlayerButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.SortByPlayer();
        }

        private void clearViews()
        {
            this.FirstListView.Items.Clear();
            this.SecondListView.Items.Clear();
            this.ThirdListView.Items.Clear();
        }
    }
}

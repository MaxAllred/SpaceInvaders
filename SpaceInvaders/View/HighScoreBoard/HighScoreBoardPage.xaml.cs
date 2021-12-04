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
            foreach (var current in HighScoreSettings.SortByScore())
            {
                this.FirstListView.Items.Add(current[0]);
                this.SecondListView.Items.Add(current[1]);
                this.ThirdListView.Items.Add(current[2]);
            }
        }
        private void SortByLevel()
        {
            foreach (var current in HighScoreSettings.SortByLevel())
            {
                this.FirstListView.Items.Add(current[2]);
                this.SecondListView.Items.Add(current[0]);
                this.ThirdListView.Items.Add(current[1]);
            }
        }
        private void SortByPlayer()
        {
            foreach (var current in HighScoreSettings.SortByPlayer())
            {
                this.FirstListView.Items.Add(current[1]);
                this.SecondListView.Items.Add(current[0]);
                this.ThirdListView.Items.Add(current[2]);
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

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders.View.HighScoreBoard
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScoreBoardPage
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreBoardPage" /> class.
        /// </summary>
        public HighScoreBoardPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.sortByScore();
        }

        private void sortByScore()
        {
            var list = HighScoreSettings.SortByScore();
            for (var i = 0; i < 10; i++)
            {
                this.firstListView.Items?.Add(list[i][0]);
                this.secondListView.Items?.Add(list[i][1]);
                this.thirdListView.Items?.Add(list[i][2]);
            }
        }

        private void sortByLevel()
        {
            var list = HighScoreSettings.SortByLevel();
            for (var i = 0; i < 10; i++)
            {
                this.firstListView.Items?.Add(list[i][2]);
                this.secondListView.Items?.Add(list[i][0]);
                this.thirdListView.Items?.Add(list[i][1]);
            }
        }

        private void sortByPlayer()
        {
            var list = HighScoreSettings.SortByPlayer();
            for (var i = 0; i < 10; i++)
            {
                this.firstListView.Items?.Add(list[i][1]);
                this.secondListView.Items?.Add(list[i][0]);
                this.thirdListView.Items?.Add(list[i][2]);
            }
        }

        private void sortByScoreButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.sortByScore();
        }

        private void sortByLevelButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.sortByLevel();
        }

        private void sortByPlayerButtonClick(object sender, RoutedEventArgs e)
        {
            this.clearViews();
            this.sortByPlayer();
        }

        private void clearViews()
        {
            this.firstListView.Items?.Clear();
            this.secondListView.Items?.Clear();
            this.thirdListView.Items?.Clear();
        }

        private void homeButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        #endregion
    }
}
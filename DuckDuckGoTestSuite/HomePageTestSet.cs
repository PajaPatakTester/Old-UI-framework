using Framework;
using NUnit.Framework;
using PageObjects.cs;

namespace DuckDuckGoTestSuite
{
    public class HomePageTestSet : TestBase

    {
        private HomePage _homePage;
        private ResultsPage _resultsPage;

        private string _text;
        private string _moneyMovie;
        private string _moneyMovieImdb;
        private string _moneyLyrics;
        private string _moneyLyricsMetro;
        private string _water;
        private string _bluesBrothers;
        private string _bluesBrothersMovie;
        private string _bluesBrothersImdb;
        private string _brassAgainst;
        private string _brassAgainstSmallCaps;
        private string _randomText;

        public override void TestInitialization()
        {
            _homePage = new HomePage();
            _resultsPage = new ResultsPage();

            _text = "text";
            _moneyMovie = "money for nothing movie";
            _moneyMovieImdb = "Money for Nothing (1993) - IMDb";
            _moneyLyrics = "money for nothing lyrics";
            _moneyLyricsMetro = "Dire Straits - Money For Nothing Lyrics | MetroLyrics";
            _water = "water";
            _bluesBrothers = "the blues brothers";
            _bluesBrothersMovie = "the blues brothers movie";
            _bluesBrothersImdb = "The Blues Brothers (1980) - IMDb";
            _brassAgainst = "brASs aGAINst";
            _brassAgainstSmallCaps = "brass against";
            _randomText = "adskla;vlbnfaworsbv['wrmva]wposrmgb";
        }

        [Test]

        public void TC01_HomePageElements()
        {
            _homePage.GoTo();

            Assert.IsTrue(_homePage.VerifyHomePageLogoEnabled(), "Logo on home page should be displayed and enabled.");
            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");
            Assert.IsFalse(_homePage.VerifyClearSearchTermDisplayed(), "[X] for clearing search term should not be displayed.");
            Assert.IsTrue(_homePage.VerifyMagnifyingGlassEnabled(), "Magnifying glass should be displayed and enabled.");
            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");
            Assert.IsTrue(_homePage.VerifyMainTextDisplayed(), $"Text should be displayed:'{HomePage.MainText}'");
            Assert.IsTrue(_homePage.VerifyLinkedPartOfMainTextDisplayed(), $"Linked part of the text should be: '{HomePage.HelpSpreadDuckDuckGo}'");

            _homePage.CloseRobotBadge();
            _homePage.CloseCookieBadge();

            Assert.IsTrue(_homePage.VerifyDropdownPrivacySimplifiedEnabled(), $"Dropdown menu with text: '{HomePage.PrivacySimplified}'");
            Assert.IsTrue(_homePage.VerifyDropDownTwiterEnabled(), "Dropdown menu with Twiter logo should be enabled.");
            Assert.IsTrue(_homePage.VerifySettingsEnabled(), "Settings menu in top right corner should be enabled.");
        }

        [Test]
        public void TC02_SearchFlowFromHomePage()
        {
            _homePage.GoTo();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");

            _homePage.InputSearchTerm(_text);

            Assert.IsTrue(_homePage.VerifySearchFieldContainingText(_text), $"Search field should contain text: {_text}");
            Assert.IsTrue(_homePage.VerifyClearSearchTermDisplayed(), "[X] for clearing search term should be displayed.");

            _homePage.SearchTermClear();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");
            Assert.IsFalse(_homePage.VerifyClearSearchTermDisplayed(), "[X] for clearing search term should not be displayed.");
            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");

            _homePage.InputSearchTerm(_moneyMovie);
            _homePage.EnterClick();

            Assert.IsTrue(_resultsPage.IsAt(), "Should be on Results Page.");
            Assert.IsTrue(_resultsPage.VerifySearchFieldContainingText(_moneyMovie), $"Search field should contain text: '{_moneyMovie}'");
            Assert.IsTrue(_resultsPage.VerifyFirstSearchResultTitle(_moneyMovieImdb), $"First search result should be: '{_moneyMovieImdb}");

            _homePage.GoTo();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");

            _homePage.InputSearchTerm(_moneyLyrics);
            _homePage.MagnifyingGlassClick();

            Assert.IsTrue(_resultsPage.IsAt(), "Should be on Results Page.");
            Assert.IsTrue(_resultsPage.VerifySearchFieldContainingText(_moneyLyrics), $"Search field should contain text: '{_moneyLyrics}'");
            Assert.IsTrue(_resultsPage.VerifyASearchResultTitle(_moneyLyricsMetro), $"A search result should be: '{_moneyLyricsMetro}");
        }

        [Test]
        public void TC03_AutocompleteOnHomePage()
        {
            _homePage.GoTo();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");
            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");

            _homePage.SearchFiledClick();

            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");

            _homePage.InputSearchTerm(_water);

            Assert.IsTrue(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete field should be displayed.");
            Assert.IsTrue(_homePage.VerifyNumberOfDefaultAutocompleteSuggestions(), $"Autocomplete should suggest: {HomePage.DefaultAutocompleteSuggestionNumber} suggestions.");
            Assert.IsTrue(_homePage.VerifyAutocompleteSuggestionsAllDifferent(), "All suggestions from autocomplete should be different.");
            Assert.IsTrue(_homePage.VerifyAllAutocompleteSuggestionsContainSearchTerm(_water), $"All autocomplete suggestions should contain: '{_water}'");

            _homePage.SearchTermClear();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");
            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");

            _homePage.InputSearchTerm(_bluesBrothers);

            Assert.IsTrue(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete field should be displayed.");

            _homePage.AutocompleteSuggestionClick(_bluesBrothersMovie);

            Assert.IsTrue(_resultsPage.IsAt(), "Should be at Results Page.");
            Assert.IsTrue(_resultsPage.VerifyFirstSearchResultTitle(_bluesBrothersImdb), $"First search result should be: '{_bluesBrothersImdb}");

            _homePage.GoTo();

            Assert.IsTrue(_homePage.VerifySearchFieldDisplayed(), "Search field should be displayed.");
            Assert.IsTrue(_homePage.VerifySearchFieldEmpty(), "Search field should be empty.");

            _homePage.InputSearchTerm(_brassAgainst);

            Assert.IsTrue(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete field should be displayed.");
            Assert.IsTrue(_homePage.VerifyAllAutocompleteSuggestionsContainSearchTerm(_brassAgainstSmallCaps), $"All autocomplete suggestions should contain: '{_brassAgainstSmallCaps}'");

            _homePage.InputSearchTerm(_randomText);

            Assert.IsFalse(_homePage.VerifyAutocompleteDisplayed(), "Autocomplete filed should not been displayed.");
        }
    }
}

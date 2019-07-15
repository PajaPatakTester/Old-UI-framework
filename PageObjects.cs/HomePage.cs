using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.PageObjectBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PageObjects.cs
{
    public class HomePage : PageObjectBase
    {
        #region Properties

        public const string Url = "https://duckduckgo.com";
        public const string HelpSpreadDuckDuckGo = "Help Spread DuckDuckGo!";
        public const string MainText = "The search engine that doesn't track you. " + HelpSpreadDuckDuckGo;
        public const string PrivacySimplified = "Privacy, simplified.";
        public const int DefaultAutocompleteSuggestionNumber = 10;

        [FindsBy(How = How.Id, Using = "logo_homepage_link")]
        private IWebElement HomePageLogo { get; set; }

        [FindsBy(How = How.ClassName, Using = "onboarding-ed__slide-1")]
        private IWebElement FirstOnboardingSlide { get; set; }

        [FindsBy(How = How.Id, Using = "search_form_input_homepage")]
        private IWebElement SearchField { get; set; }

        [FindsBy(How = How.ClassName, Using = "search__autocomplete")]
        private IWebElement AutocompleteField { get; set; }

        [FindsBy(How = How.Id, Using = "search_button_homepage")]
        private IWebElement MagnifyingGlass { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.tag-home__item")]
        private IWebElement MainTextElem { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.tag-home__link")]
        private IWebElement HelpLink { get; set; }

        [FindsBy(How = How.Id, Using = "wedonttrack")]
        private IWebElement PrivacySimplifiedElem { get; set; }

        [FindsBy(How = How.ClassName, Using = "js-showcase-popout")]
        private IWebElement PrivacySimplifiedDropdownElem { get; set; }

        [FindsBy(How = How.ClassName, Using = "header--aside__twitter")]
        private IWebElement TwitterIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = "span.header__label")]
        private IWebElement SocialNetworkDropdown { get; set; }

        [FindsBy(How = How.ClassName, Using = "js-side-menu-open")]
        private IWebElement SettingsMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".js-badge-main-msg .badge-link__close")]
        private IWebElement RobotBadgeCloseElem { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".js-badge-cookie-msg .badge-link__close")]
        private IWebElement CookieBadgeCloseElem { get; set; }

        [FindsBy(How = How.Id, Using = "search_form_input_clear")]
        private IWebElement ClearSearchTermElem { get; set; }

        [FindsBy(How = How.Id, Using = "search_button_homepage")]
        private IWebElement MagnifyingGlassElem { get; set; }

        [FindsBy(How = How.ClassName, Using = "acp")]
        private IList<IWebElement> AutocompleteSuggestionsElem { get; set; }

        #endregion

        #region Actions

        public void GoTo()
        {
            WebDriver.Instance.Navigate().GoToUrl(Url);
        }

        public void InputSearchTerm(string term)
        {
            SearchField.Clear();
            SearchField.SendKeys(term);
        }

        public void MagnifyingGlassClick()
        {
            MagnifyingGlass.Click();
        }

        public void SearchFiledClick()
        {
            SearchField.Click();
        }

        public void EnterClick()
        {
            SearchField.SendKeys(Keys.Enter);
        }

        public void CloseRobotBadge()
        {
            RobotBadgeCloseElem.Click();
        }

        public void CloseCookieBadge()
        {
            CookieBadgeCloseElem.Click();
        }

        public void SearchTermClear()
        {
            ClearSearchTermElem.Click();
        }


        private List<string> AutocompleteList()
        {
            var textList = new List<string>();
            foreach (var elem in AutocompleteSuggestionsElem)
            {
                textList.Add(elem.Text);
            }

            return textList;
        }

        private IWebElement FetchAutocompleteSuggestion(string suggestion)
        {
            return AutocompleteSuggestionsElem.FirstOrDefault(x => x.Text.Equals(suggestion));
        }

        public void AutocompleteSuggestionClick(string suggestion)
        {
         FetchAutocompleteSuggestion(suggestion).Click();
        }

        #endregion

        #region Verifications

        public bool VerifyHomePageLogoEnabled()
        {
            return HomePageLogo.Displayed && HomePageLogo.Enabled;
        }

        public bool VerifySearchFieldDisplayed()
        {
            return SearchField.Displayed;
        }

        public bool VerifySearchFieldEmpty()
        {
            return VerifySearchFieldContainingText(string.Empty);
        }

        public bool VerifySearchFieldContainingText(string term)
        {
            return SearchField.GetAttribute("value").Equals(term);
        }

        public bool VerifyMagnifyingGlassEnabled()
        {
            return MagnifyingGlass.Displayed && MagnifyingGlass.Enabled;
        }

        public bool VerifyMainTextDisplayed()
        {
            return MainTextElem.Text.Trim().Equals(MainText);
        }

        public bool VerifyLinkedPartOfMainTextDisplayed()
        {
            return HelpLink.Displayed;
        }

        public bool VerifyDropdownPrivacySimplifiedEnabled()
        {
            return PrivacySimplifiedElem.Enabled && PrivacySimplifiedDropdownElem.Enabled;
        }

        public bool VerifyTwiterIconDisplayed()
        {
            return TwitterIcon.Displayed;
        }

        public bool VerifyDropDownTwiterEnabled()
        {
            return TwitterIcon.Enabled && SocialNetworkDropdown.Enabled;
        }

        public bool VerifySettingsEnabled()
        {
            return SettingsMenu.Enabled;
        }

        public bool VerifyAutocompleteDisplayed()
        {
            WebDriver.Wait(TimeSpan.FromMilliseconds(300)); // added wait since slower response from autocomplete field
            return AutocompleteField.GetAttribute("style").Contains("block");
        }

        public bool VerifyFirstOnboardingSlideDisplayed()
        {
            return FirstOnboardingSlide.Displayed;
        }

        public bool VerifyClearSearchTermDisplayed()
        {
            return ClearSearchTermElem.Displayed && ClearSearchTermElem.Enabled;
        }

        public bool VerifyNumberOfDefaultAutocompleteSuggestions()
        {
            return AutocompleteSuggestionsElem.Count == DefaultAutocompleteSuggestionNumber;
        }

        public bool VerifyAutocompleteSuggestionsAllDifferent()
        {
            return AutocompleteList().Count == AutocompleteList().Distinct().Count();
        }

        public bool VerifyAllAutocompleteSuggestionsContainSearchTerm(string term)
        {
            foreach (var lst in AutocompleteList())
            {
                if (!lst.Contains(term)) return false;
            }

            return true;
        }

        #endregion
    }
}

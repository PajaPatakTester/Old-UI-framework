using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.PageObjectBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PageObjects.cs
{
    public class ResultsPage : PageObjectBase
    {
        #region Properties

        [FindsBy(How = How.ClassName, Using = "header__search-wrap")]
        private IWebElement HomePageLogo { get; set; }

        [FindsBy(How = How.Id, Using = "search_form_input")]
        private IWebElement SearchField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[id^='r1']")]
        private IList<IWebElement> Results { get; set; }

        #endregion

        #region Actions

        #endregion

        #region Verifications

        public bool IsAt() => WebDriver.Instance.FindElements(By.ClassName("header__search-wrap")).Count != 0;

        public bool VerifySearchFieldContainingText(string term)
        {
            return SearchField.GetAttribute("value").Trim().Equals(term);
        }

        public bool VerifyFirstSearchResultTitle(string title)
        {
            var  txt = Results.First().FindElement(By.TagName("a")).Text;
            return txt.Equals(title);
        }

        public bool VerifyASearchResultTitle(string title)
        {
            return FetchResult(title) != null;
        }

        private IWebElement FetchResult(string title)
        {
            return Results.FirstOrDefault(x => x.FindElement(By.TagName("a")).Text.Equals(title));
        }

        #endregion



    }
}

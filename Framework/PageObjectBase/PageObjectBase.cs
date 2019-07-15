using OpenQA.Selenium.Support.PageObjects;

namespace Framework.PageObjectBase
{
    public class PageObjectBase
    {
        protected PageObjectBase()
        {
            PageFactory.InitElements(WebDriver.Instance, this,  new ExtendedPageObjectMemberDecorator());
        }
    }
}

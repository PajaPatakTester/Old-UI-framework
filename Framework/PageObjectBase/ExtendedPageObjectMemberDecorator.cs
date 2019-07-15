using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Framework.PageObjectBase
{
   public class ExtendedPageObjectMemberDecorator : DefaultPageObjectMemberDecorator, IPageObjectMemberDecorator

    {

        private static object CreateProxyObject(Type memberType, IElementLocator locator, IEnumerable<By> bys,
            bool cache)
        {
            var dynMethod = typeof(DefaultPageObjectMemberDecorator).GetMethod("CreateProxyObject",
                BindingFlags.NonPublic | BindingFlags.Static);

            var isWebElement = memberType == typeof(IList<IWebElement>) || memberType == typeof(IWebElement);

            var proxyObject = dynMethod.Invoke(null,
                new object[] { isWebElement ? memberType : typeof(IWebElement), locator, bys, cache });

            if (!isWebElement)
            {
                // see if the type has a constructor that takes an instance of IWebElement. If so, 
                // create an instance of the type and feed it the generated proxy object.
                var ctor = memberType.GetConstructor(new[] { typeof(IWebElement) });
                if (ctor == null)
                {
                    throw new ArgumentException(
                        "No constructor for the specified class containing a single argument of type IWebDriver can be found");
                }

                return ctor.Invoke(new[] { proxyObject });
            }

            return proxyObject;
        }
    }
}

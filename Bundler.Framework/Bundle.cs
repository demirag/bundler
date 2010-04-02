using Bundler.Framework.Css;
using Bundler.Framework.JavaScript;
using Bundler.Framework.Resx;

namespace Bundler.Framework
{
    public class Bundle
    {
        public static IJavaScriptBundle JavaScript()
        {
            return new JavaScriptBundle();
        }
       
        public static ICssBundle Css()
        {
            return new CssBundle();
        }

        public static IResxBundle Resx()
        {
            return new ResxBundle();
        }
    }
}
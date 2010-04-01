using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bundler.Framework.Resx
{
    public interface IResxBundleBuilder
    {
        IResxBundleBuilder Add(string resxPath);
        string Render(string renderTo);
        void AsNamed(string name, string renderTo);
    }
}

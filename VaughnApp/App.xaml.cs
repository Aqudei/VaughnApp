using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using VaughnApp.Views;

namespace VaughnApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        // RegisterTypes function is here

        protected override Window CreateShell()
        {
            var w = Container.Resolve<Shell>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(Arduino));
        }
    }
}

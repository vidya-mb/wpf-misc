using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Fluent.UITests;


public class ApplicationFixture : IDisposable
{
    public ApplicationFixture() 
    {
        AppInstance = new Application();
    }

    public void CreateApplicationInstance()
    {
        if(AppInstance == null)
        {
            AppInstance = new Application();
        }
    }

    public void ResetApplicationInstance()
    {
        AppInstance.Dispatcher.BeginInvoke(() => { 
            AppInstance.ThemeMode = ThemeMode.None;
            AppInstance.MainWindow = null;
            if (AppInstance.Resources.Count > 0 
                || AppInstance.Resources.MergedDictionaries.Count > 0)
            {
                AppInstance.Resources.Clear();
            }
        }, System.Windows.Threading.DispatcherPriority.ContextIdle);
    }

    public void RunAction(Action action)
    {
        AppInstance.Dispatcher.BeginInvoke(action, System.Windows.Threading.DispatcherPriority.ContextIdle);
    }

    public DispatcherOperation GetWindowCollection()
    {
        DispatcherOperation task = AppInstance.Dispatcher.BeginInvoke(() => {
            return AppInstance.Windows;
        }, DispatcherPriority.Send);
        return task;
    }

    public void Dispose()
    {
        AppInstance.Shutdown();
    }

    public Application AppInstance { get; private set; }
}

[CollectionDefinition("Application Tests Collection", DisableParallelization = true)]
public class ApplicationTestsCollection : ICollectionFixture<ApplicationFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

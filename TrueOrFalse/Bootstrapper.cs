using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using TrueOrFalse.Models;
using TrueOrFalse.ViewModels;
using TrueOrFalse.Views;

namespace TrueOrFalse
{
    public class Bootstrapper : BootstrapperBase
    {
        private WindsorContainer _windsorContainer;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }

        protected override void Configure()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Register(
                Component.For<IEventAggregator>()
                    .ImplementedBy<EventAggregator>()
                    .LifestyleSingleton(),
                Component.For<IDialogService>()
                    .ImplementedBy<DialogService>(),
                Component.For<IWindowManager>()
                    .ImplementedBy<WindowManager>()
                    .LifestyleSingleton(),
                Component.For<IPersistence>()
                    .ImplementedBy<Persistence>()
                    .DependsOn(Dependency.OnValue<string>("database.xml"))
                    .LifestyleSingleton()
            );

            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            _windsorContainer.Register(
                Classes.FromAssembly(typeof(MainViewModel).Assembly)
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .Configure(r => r.LifeStyle.Is(LifestyleType.Transient))
            );
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? _windsorContainer.Resolve(service)
                : _windsorContainer.Resolve(key, service);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}

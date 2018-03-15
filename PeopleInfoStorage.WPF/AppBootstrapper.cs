namespace PeopleInfoStorage.WPF {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using PeopleInfoStorage.Core.Domain;
    using PeopleInfoStorage.Services.Repository;
    using PeopleInfoStorage.Services.Services;
    using PeopleInfoStorage.WPF.ViewModels;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, MainWindowViewModel>();
            container.Singleton<IPeopleService, PeopleService>();
            container.Singleton<IRepository<Person>, XmlRepository<Person>>();
            container.Singleton<ISerializer<Person>, XmlSerializer<Person>>();
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}
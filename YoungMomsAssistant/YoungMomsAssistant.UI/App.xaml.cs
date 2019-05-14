using System.Windows;
using Unity;
using YoungMomsAssistant.UI.Services;
using YoungMomsAssistant.UI.Views.CustomControls;
using YoungMomsAssistant.UI.Views.Windows;

namespace YoungMomsAssistant.UI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            var container = new UnityContainer();

            container.AddExtension(new Diagnostic());

            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterSingleton<IAuthorizationTokensService, AuthorizationTokensService>();
            container.RegisterSingleton<ILifeEventsService, LifeEventsService>();
            container.RegisterSingleton<IRequestJwtTokensDecorator, RequestJwtTokensDecorator>();
            container.RegisterSingleton<IBabiesService, BabiesService>();

            container.RegisterType<MainWindow>();
            container.RegisterType<SignInWindow>();
            container.RegisterType<SignUpWindow>();

            container.RegisterInstance(new WindowsService(
                () => container.Resolve<MainWindow>(),
                () => container.Resolve<SignInWindow>(),
                () => container.Resolve<SignUpWindow>()
            ));

            container.RegisterInstance(container.Resolve<TemplatesNavigationService>());

            var signInWindow = container.Resolve<SignInWindow>();
            signInWindow.Show();
        }
    }
}

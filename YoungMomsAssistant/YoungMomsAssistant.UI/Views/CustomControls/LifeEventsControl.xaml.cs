using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using YoungMomsAssistant.UI.Models;
using YoungMomsAssistant.UI.ViewModels;

namespace YoungMomsAssistant.UI.Views.CustomControls {
    /// <summary>
    /// Interaction logic for LifeEventsControl.xaml
    /// </summary>
    public partial class LifeEventsControl : UserControl {
        public LifeEventsControl() {
            InitializeComponent();
        }

        [Dependency]
        public LifeEventsViewModel ViewModel {
            get => DataContext as LifeEventsViewModel;
            set => DataContext = value;
        }

        private void EditLifeEvent_Click(object sender, RoutedEventArgs e) {
            if ((sender as Button)?.DataContext is LifeEvent lifeEvent) {
                ViewModel.LifeEventToEdit = new LifeEvent {
                    Id = lifeEvent.Id,
                    Image = lifeEvent.Image.Clone() as byte[],
                    Summary = lifeEvent.Summary,
                    Title = lifeEvent.Title,
                    Date = lifeEvent.Date
                };
                editExpander.IsExpanded = mainExpander.IsExpanded = true;
            }
        }
    }
}

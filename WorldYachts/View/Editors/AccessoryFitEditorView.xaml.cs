using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldYachts.ViewModel;

namespace WorldYachts.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для AccessoryFitEditorView.xaml
    /// </summary>
    public partial class AccessoryFitEditorView : UserControl
    {
        public static Func<BaseViewModel> EditorAfterLoad;

        public AccessoryFitEditorView()
        {
            InitializeComponent();
            if (EditorAfterLoad != null)
                DataContext = EditorAfterLoad?.Invoke();
        }
    }
}

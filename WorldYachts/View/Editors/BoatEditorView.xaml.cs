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
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для AddBoatView.xaml
    /// </summary>
    public partial class BoatEditorView : UserControl
    {
        public static Func<BaseViewModel> EditorAfterLoad;
        public BoatEditorView()
        {
            InitializeComponent();
            if (EditorAfterLoad != null)
                DataContext = EditorAfterLoad.Invoke();
        }
    }
}

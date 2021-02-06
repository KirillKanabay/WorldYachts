using System;
using System.Windows.Controls;
using WorldYachts.ViewModel;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для PartnerEditorView.xaml
    /// </summary>
    public partial class PartnerEditorView : UserControl
    {
        public static Func<BaseViewModel> EditorAfterLoad;
        public PartnerEditorView()
        {
            InitializeComponent();
            if(EditorAfterLoad != null) 
                DataContext = EditorAfterLoad.Invoke();
        }
    }
}

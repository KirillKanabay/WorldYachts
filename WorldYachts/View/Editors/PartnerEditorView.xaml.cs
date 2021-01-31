using System;
using System.Windows.Controls;
using WorldYachts.ViewModel;

namespace WorldYachts.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для PartnerEditorView.xaml
    /// </summary>
    public partial class PartnerEditorView : UserControl
    {
        public static Func<BaseViewModel> PartnerEditorAfterLoad;
        public PartnerEditorView()
        {
            InitializeComponent();
            if(PartnerEditorAfterLoad != null) 
                DataContext = PartnerEditorAfterLoad.Invoke();
        }
    }
}

//using WorldYachts.Data.Entities;
//using WorldYachts.View.Editors;
//using WorldYachts.View.MessageDialogs;
//using WorldYachts.ViewModel.BaseViewModels;

//namespace WorldYachts.ViewModel.Users.SalesPersons
//{
//    class SelectableSalesPersonViewModel:BaseSelectableViewModel<SalesPerson>
//    {
//        #region Поля

//        private int _id;
//        private string _name;
//        private string _secondName;

//        #endregion
//        public SelectableSalesPersonViewModel(SalesPerson item) : base(item,null)
//        {
//            Id = item.Id;
//            Name = item.FirstName;
//            SecondName = item.SecondName;
//        }

//        #region Свойства

//        public int Id
//        {
//            get => _id;
//            set
//            {
//                _id = value;
//                OnPropertyChanged(nameof(Id));
//            }
//        }


//        public string Name
//        {
//            get => _name;
//            set
//            {
//                _name = value;
//                OnPropertyChanged(nameof(Name));
//            }
//        }

//        public string SecondName
//        {
//            get => _secondName;
//            set
//            {
//                _secondName = value;
//                OnPropertyChanged(nameof(SecondName));
//            }
//        }
//        #endregion

//        public override BaseEditorViewModel<SalesPerson> Editor => new SalesPersonEditorViewModel();

//        #region Методы

//        public override string ToString()
//        {
//            return $"Id: {Id}\n" +
//                   $"Имя: {Name}\n" +
//                   $"Фамилия: {SecondName}";
//        }

//        protected override void ToggleViewEditorAfterLoaded()
//        {
//            if (SalesPersonEditorView.EditorAfterLoad != null)
//            {
//                SalesPersonEditorView.EditorAfterLoad = null;
//            }
//            else
//            {
//                SalesPersonEditorView.EditorAfterLoad = GetEditorViewModel;
//            }
//        }

//        protected override BaseViewModel GetEditorViewModel()=> new SalesPersonEditorViewModel();

//        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
//        {
//            return new MessageDialogProperty()
//            {
//                Title = "Подтверждение удаления",
//                Message = "Будет удален следующий менеджер:\n\n" + this
//            };
//        }

//        #endregion

//    }
//}

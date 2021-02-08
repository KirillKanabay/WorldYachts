using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Validators;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class DepositEditorViewModel:ContractEditorViewModel, IDataErrorInfo
    {
        #region Конструкторы
        public DepositEditorViewModel(Contract contract) : base(contract)
        {
        }

        public DepositEditorViewModel()
        {
        }

        #endregion

        #region Свойства

        public string Title => $"Внесение депозита в контракт #{_contract.Id}";

        #endregion

        #region Методы

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return $"В контракт #{_contract.Id} внесена сумма: {Deposit:F2}₽";
        }

        #endregion

        #region Валидация полей
        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;

                new Validation(new PositiveNumberValidationRule(Deposit)).Validate(ref error);

                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }


        #endregion

    }
}

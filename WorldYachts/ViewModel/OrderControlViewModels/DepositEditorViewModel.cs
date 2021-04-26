using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.Services;
using WorldYachts.Validators;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class DepositEditorViewModel:ContractEditorViewModel, IDataErrorInfo
    {
        private readonly AuthUser _authUser;
        #region Конструкторы
        public DepositEditorViewModel(Contract contract,AuthUser authUser) : base(contract,authUser)
        {
            _authUser = authUser;
        }

        public DepositEditorViewModel(AuthUser authUser):base(authUser)
        {
            _authUser = authUser;
        }

        #endregion

        #region Свойства

        public string Title => $"Внесение депозита в контракт #{_contract.Id}";

        #endregion

        #region Методы

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return (_authUser.TypeOfUser==TypeOfUser.SalesPerson)
                ? $"В контракт #{_contract.Id} внесена сумма: {Deposit:F2}₽":
                $"Сумма: {Deposit:F2}₽ отправлена на рассмотрение. Контракт #{_contract.Id}";
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

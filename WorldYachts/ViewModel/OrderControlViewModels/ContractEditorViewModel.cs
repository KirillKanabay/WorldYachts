﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class ContractEditorViewModel:BaseEditorViewModel<Contract>
    {
        #region Поля

        private readonly int _id;
        private decimal _deposit;
        private string _productionProcess;
        protected Contract _contract;

        protected Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        public IEnumerable<string> _productProcesses = new List<string>()
        {
            EnumWorker.GetDescription(ProductionProcess.NotStarted),
            EnumWorker.GetDescription(ProductionProcess.Started),
            EnumWorker.GetDescription(ProductionProcess.InWork25),
            EnumWorker.GetDescription(ProductionProcess.InWork50),
            EnumWorker.GetDescription(ProductionProcess.InWork75),
            EnumWorker.GetDescription(ProductionProcess.Finishing),
            EnumWorker.GetDescription(ProductionProcess.Finished),
        };

        private readonly AuthUser _authUser;
        #endregion

        #region Конструкторы
        public ContractEditorViewModel(Contract contract,AuthUser authUser)
        {
            _id = contract.Id;
            _productionProcess = contract.ProductionProcess;
            _authUser = authUser;
            _contract = contract;
        }

        public ContractEditorViewModel(AuthUser authUser)
        {
            _authUser = authUser;
        }

        #endregion

        #region Свойства

        public decimal Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }

        public string ProductProcess
        {
            get => _productionProcess;
            set
            {
                _productionProcess = value;
                OnPropertyChanged(ProductProcess);
            }
        }

        public IEnumerable<string> ProductProcesses
        {
            get => _productProcesses;
            set
            {
                _productProcesses = value;
                OnPropertyChanged(nameof(ProductProcesses));
            }
        }

        public override bool SaveButtonIsEnabled => true;
        public override IDataModel<Contract> ModelItem => null;
        
        #endregion

        #region Методы

        protected override async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var item = GetSaveItem(_isEdit);
            item.ProductionProcess = ProductProcess;
            
            Invoice invoice = null;
            if (Deposit > 0)
            {
                invoice = new Invoice()
                {
                    ContractId = item.Id,
                    Settled = _authUser.TypeOfUser == TypeOfUser.SalesPerson,
                    //Если внесено больше, чем требуется, пополняем оставшуюся сумму контракта
                    Sum = (item.DepositPayed + Deposit > item.ContractTotalPriceInclVat)
                        ? item.ContractTotalPriceInclVat - item.DepositPayed
                        : Deposit,
                    SumInclVat = Deposit * (Convert.ToDecimal(18 * 0.01)),
                    Date = DateTime.Now,
                };

                if (invoice.Settled)
                {
                    item.DepositPayed += invoice.Sum;
                }
            }
            
            try
            {
                if (_isEdit)
                {
                    //await Task.Run(() => ModelItem.UpdateAsync(item));
                    //if (invoice != null)
                    //    await Task.Run(() => new InvoiceModel().AddAsync(invoice));
                }
                else
                {
                    await Task.Run((() => ModelItem.AddAsync(item)));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            MainWindow.SendSnackbarAction?.Invoke(GetSaveSnackbarMessage(_isEdit));

            //Закрываем диалог редактирования
            MainWindow.GetMainWindow?.Invoke().DialogHost.CurrentSession.Close();
        }

        protected override Contract GetSaveItem(bool isEdit)
        {
            return _contract;
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return $"Изменение в контракт #{_id} внесено успешно";
        }

        #endregion

    }
}

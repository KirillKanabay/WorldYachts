﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Helpers;

namespace WorldYachts.View.MessageDialogs
{
    struct MessageDialogProperty
    {
        public string Title;
        public string Message;
    }
    class SampleMessageDialogViewModel:INotifyPropertyChanged
    {
        #region Поля
        private string _title = "Заголовок";
        private string _message = "Сообщение!";
        #endregion

        #region Свойства
        /// <summary>
        /// Заголовок диалога
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                this.MutateVerbose(ref _title, value, RaisePropertyChanged());
            }
        }
        /// <summary>
        /// Сообщение диалога
        /// </summary>
        public string Message
        {
            get => _message;
            set
            {
                this.MutateVerbose(ref _message, value, RaisePropertyChanged());
            }
        }


        #endregion

        #region Конструкторы
        public SampleMessageDialogViewModel()
        {
        }
        public SampleMessageDialogViewModel(string title, string message)
        {
            _title = title;
            _message = message;
        }

        public SampleMessageDialogViewModel(MessageDialogProperty mdp) : this(mdp.Title, mdp.Message)
        {

        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }


        #endregion

    }
}

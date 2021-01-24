using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Helpers.Commands
{
    public class AsyncRelayCommand: AsyncCommandBase
    {
        private readonly Func<Task> _callback;
        private readonly Action<Exception> _onException;

        public AsyncRelayCommand(Func<Task> callback, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
            _onException = onException;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _callback();
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }
        }
    }
}

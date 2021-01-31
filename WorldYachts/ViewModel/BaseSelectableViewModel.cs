using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.ViewModel
{
    class BaseSelectableViewModel<TData>
    {
        private TData _data;
        public BaseSelectableViewModel(TData data)
        {
            _data = data;
        }


    }
}

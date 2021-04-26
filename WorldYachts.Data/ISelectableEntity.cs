using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Data
{
    public interface ISelectableEntity
    {
        bool IsSelected { get; set; }
        bool IsDeleted { get; set; }
    }
}

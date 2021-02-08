using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WorldYachts.Infrastructure
{
    enum ProductionProcess
    {
        [Description("Работы не начаты")]
        NotStarted = 0,
        [Description("Начато производство")]
        Started = 1,
        [Description("25% готовности")]
        InWork25 = 2,
        [Description("50% готовности")]
        InWork50 = 3,
        [Description("75% готовности")]
        InWork75 = 4,
        [Description("Отделка лодки")]
        Finishing = 5,
        [Description("Лодка готова")]
        Finished = 6
    } 
}

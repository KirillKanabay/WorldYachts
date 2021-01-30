using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    class PartnerModel
    {
        public Partner Partner { get; set; }
        public PartnerModel()
        {
            
        }

        public PartnerModel(Partner partner)
        {
            Partner = partner;
        }


    }
}

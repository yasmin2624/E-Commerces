﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BasketModules
{
    public  class CustomerBasket
    {
        public string Id { get; set; } //Guid => Created From Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; }

    }
}

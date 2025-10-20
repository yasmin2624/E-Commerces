﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException(int id) : NotFoundException($"The Product With Id :{id} Is Not Found")
    {
    }
}

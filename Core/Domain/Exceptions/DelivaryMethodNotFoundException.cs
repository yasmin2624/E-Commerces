using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DelivaryMethodNotFoundException(int Id):NotFoundException($"Delivary Method with Id {Id} not found.")
    {
    }
}

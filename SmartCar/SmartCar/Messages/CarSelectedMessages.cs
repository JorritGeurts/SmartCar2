using CommunityToolkit.Mvvm.Messaging.Messages;
using SmartCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCar.Messages
{
    public class CarSelectedMessages(SmarterCarDTO car): ValueChangedMessage<SmarterCarDTO>(car)
    {
    }
}

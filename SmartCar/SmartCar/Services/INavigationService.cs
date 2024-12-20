﻿using SmartCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCar.Services
{
    public interface INavigationService
    {
        Task NavigateToInfoPageAsync(SmarterCar car);
        Task NavigateBackAsync();
        Task NavigateToDetailsPageAsync();
    }
}

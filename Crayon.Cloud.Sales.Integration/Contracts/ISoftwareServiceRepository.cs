﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ISoftwareServiceRepository
    {
        Task<bool> CancaelSoftwareService(int softwareId, int accountId);
    }
}

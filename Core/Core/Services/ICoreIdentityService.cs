﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ICoreIdentityService
    {
        public string GetUserId { get; }
    }
}

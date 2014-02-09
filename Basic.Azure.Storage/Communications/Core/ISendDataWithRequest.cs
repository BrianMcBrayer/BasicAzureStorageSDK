﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Azure.Storage.Communications.Core
{
    public interface ISendDataWithRequest
    {
        byte[] GetContentToSend();
        int GetContentLength();
    }
}

﻿/*
 * Shadowsocks-Net https://github.com/shadowsocks/Shadowsocks-Net
 */

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Buffers;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Argument.Check;

namespace Shadowsocks.Infrastructure.Pipe
{
    /// <summary>
    /// Represent the reading result of <see cref="IClientReader"/>.
    /// </summary>
    public struct ClientReadResult
    {
        public ClientReadWriteResult Result;
        public SmartBuffer Memory;
        public int Read;

        public ClientReadResult(ClientReadWriteResult result, SmartBuffer memory, int read)
        {
            Result = result;
            Memory = memory;
            Read = read;
        }
    }
}

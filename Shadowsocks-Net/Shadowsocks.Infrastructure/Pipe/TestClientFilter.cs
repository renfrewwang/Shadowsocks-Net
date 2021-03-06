﻿/*
 * Shadowsocks-Net https://github.com/shadowsocks/Shadowsocks-Net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Argument.Check;


namespace Shadowsocks.Infrastructure.Pipe
{
    using Sockets;
    public class TestClientFilter : ClientFilter
    {
        public TestClientFilter(IClient client, ILogger logger = null)
                 : base(client, ClientFilterCategory.Custom, 0)
        {
            _logger = logger;
        }
        public override ClientFilterResult BeforeWriting(ClientFilterContext ctx)
        {
            _logger?.LogInformation($"TestPipeFilter BeforeWriting data={ctx.Memory.ToArray().ToHexString()}");
            var newBuff = SmartBuffer.Rent(ctx.Memory.Length + 4);
            var p = newBuff.Memory.Span;
            p[0] = 0x12;
            p[1] = 0x34;
            p[4] = 0xAB;
            p[3] = 0xCD;
            ctx.Memory.CopyTo(newBuff.Memory.Slice(4));
            newBuff.SignificantLength = ctx.Memory.Length + 4;
            return new ClientFilterResult(ctx.Client, newBuff, true);
        }

        public override ClientFilterResult AfterReading(ClientFilterContext ctx)
        {
            _logger?.LogInformation($"TestPipeFilter AfterReading data={ctx.Memory.ToArray().ToHexString()}");
            var newBuff = SmartBuffer.Rent(ctx.Memory.Length - 4);
            ctx.Memory.Slice(4).CopyTo(newBuff.Memory);
            newBuff.SignificantLength = ctx.Memory.Length - 4;
            return new ClientFilterResult(ctx.Client, newBuff, true);
        }


    }
}

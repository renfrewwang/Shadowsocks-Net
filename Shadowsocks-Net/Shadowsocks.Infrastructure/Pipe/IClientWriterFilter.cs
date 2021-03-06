﻿/*
 * Shadowsocks-Net https://github.com/shadowsocks/Shadowsocks-Net
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.Infrastructure.Pipe
{
    public interface IClientWriterFilter : IComparer<IClientWriterFilter>, IClientObject
    {
        ClientFilterResult BeforeWriting(ClientFilterContext filterContext);
    }
}

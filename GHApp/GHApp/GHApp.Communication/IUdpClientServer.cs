﻿using System;

namespace GHApp.Communication
{
    public interface IUdpClientServer
    {
        IObservable<byte[]> Listen(int localPort);

        IObservable<int> Send(string remoteAddress, int port, byte[] data);
    }
}
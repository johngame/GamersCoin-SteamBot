using System;
using GamerscoinWrapper.Data;

namespace GamerscoinWrapper.Wrapper.Interfaces
{
    public interface IGamerscoinService
    {
        Transaction GetTransaction(String txId);
    }
}
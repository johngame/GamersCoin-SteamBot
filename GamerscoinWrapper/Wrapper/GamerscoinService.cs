using System;
using GamerscoinWrapper.Data;
using GamerscoinWrapper.Wrapper.Interfaces;

namespace GamerscoinWrapper.Wrapper
{
    /// <summary>
    /// This class is a helper class to get useful information
    /// </summary>
    public sealed class GamerscoinService : IGamerscoinService
    {
        private readonly IBaseBtcConnector _baseBtcConnector;

        public GamerscoinService(bool isPrimary)
        {
            _baseBtcConnector = new BaseBtcConnector(isPrimary);    
        }

        public Transaction GetTransaction(String txId)
        {
            String rawTransaction = _baseBtcConnector.GetRawTransaction(txId);
            return _baseBtcConnector.DecodeRawTransaction(rawTransaction);
        }
    }
}

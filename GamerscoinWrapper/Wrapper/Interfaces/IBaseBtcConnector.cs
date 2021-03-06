﻿using System;
using GamerscoinWrapper.Common;
using GamerscoinWrapper.Data;

namespace GamerscoinWrapper.Wrapper.Interfaces
{
    public interface IBaseBtcConnector
    {
        Decimal GetBalance(String account);
        Block GetBlock(String hash);
        String GetRawTransaction(String txId);
        Transaction DecodeRawTransaction(String rawTransaction);
        String GetAccount(String GamerscoinAddress);
        Int32 GetBlockCount();
        String GetBlockhash(Int32 index);
        Double GetDifficulty();
        String GetGenerate();
        Int32 GetHashesPerSec();
        String GetInfo();
        String GetMiningInfo();
        String GetPeerInfo();
        String GetRawMemPool();
        String BackupWallet(String destination);
        String DumpPrivKey(String GamerscoinAddress);
        String EncryptWallet(String passphrame);
        String GetAccountAddress(String account);
        String GetAddressesByAccount(String account);
        String GetBlockTemplate();
        Int32 GetconnectionCount();
        String GetNewAddress(String account);
        String GetReceivedByAccount(String account);
        String GetReceivedByAddress(String GamerscoinAddress);

        /// <summary>
        /// Only for in-wallet transactions. To find "all transactions", use GetRawTransaction and decude with DecodeRawTransaction
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        InwalletTransaction GetTransaction(String txid);

        String GetWork();
        String Help();
        String ListAccounts();
        String ListAddressGroupings();
        String ListReceivedByAccount();
        String ListReceivedByAddress();
        String ListSinceBlock();
        String ListTransactions();
        String ListLockUnspent();
        String ListUnspent(int min, int max);
        String SetTxFee(Decimal amount);
        String WalletLock();
        String ValidateAddress(String GamerscoinAddress);
        String Stop();
        String SetGenerate(Boolean variable);

        /// <summary>
        /// not tested
        /// </summary>
        /// <param name="GamerscoinAddress"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        String SendToAddress(String GamerscoinAddress, Decimal amount);
        String SendRawTransaction(String rawTrans);
        String AddNode(String node, NodeAction action);
        String GetAddedNodeInfo(String dns);
        String GetTxOut(String txId, Int32 n);
        String GetTxOutSetInfo();
        String KeyPoolRefill();
        String SendFrom(String fromAccount, String toGamerscoinAddress, Decimal amount);
        String SignMessage(String GamerscoinAddress, String message);
        String SubmitBlock(String hexData);
        String VerifyMessage(String GamerscoinAddress, String signature, String message);

        /// <summary>
        /// This opens the Gamerscoin wallet to access methods which requires an open wallet
        /// </summary>
        /// <param name="passphrase">Password you've encrypted the wallet with</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        String WalletPassphrase(String passphrase, Int32 timeout);
        String WalletPassphraseChange(String oldPassphrase, String newPassphrase);
        String Move(String fromAccount, String toAccount, Decimal amount);
    }
}
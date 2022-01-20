using System;
using System.Linq;
using Xunit;

namespace Nano.Net.Tests
{
    public class RpcTest
    {
        private readonly RpcClient _rpcClient = new RpcClient(Constants.RpcAddress);

        [Fact]
        public async void UpdateAccountInfoTest()
        {
            AccountInfoResponse accountInfo = await _rpcClient.AccountInfoAsync(Constants.ReferenceAccount);

            Assert.Equal("3865BFCD423CE3579C4A7C6010CE763BE4C63964AC06BDA451A63BBCAC9E3712", accountInfo.Frontier, true);
            Assert.Equal("15000000000000000000000000000", accountInfo.Balance, true);
            Assert.Equal("nano_18shbirtzhmkf7166h39nowj9c9zrpufeg75bkbyoobqwf1iu3srfm9eo3pz", accountInfo.Representative, true);
        }

        [Fact]
        public async void UnopenedAccountTest()
        {
            await Assert.ThrowsAsync<UnopenedAccountException>(async () =>
                await _rpcClient.AccountInfoAsync("nano_3cxdogotqcxdogotqcxdogotqcxdogotqcxdogotqcxdogotqcxdqq8mbd6p"));
        }

        [Fact]
        public async void RpcErrorTest()
        {
            await Assert.ThrowsAsync<RpcException>(async () => await _rpcClient.AccountInfoAsync("0"));
        }

        [Fact]
        public async void PendingBlockTest()
        {
            ReceivableBlocksResponse receivableBlocks = await _rpcClient.PendingBlocksAsync(Constants.ReferenceAccount);
            Assert.NotEmpty(receivableBlocks.PendingBlocks);
        }

        [Fact]
        public async void AccountHistoryTest()
        {
            AccountHistoryResponse accountHistoryResponse = await _rpcClient.AccountHistoryAsync(Constants.ReferenceAccount);
            HistoryBlock firstBlock = accountHistoryResponse.History[0];

            Assert.Equal(Constants.ReferenceAccount, accountHistoryResponse.Account);
            Assert.Equal("receive", firstBlock.Type);
            Assert.Equal("75F0B821DE3B25908755520117660E1297DDEA774DEC817FAA2C27221442403A", firstBlock.Hash);
        }

        [Fact]
        public async void AccountsBalanceTest()
        {
            AccountsBalancesResponse accountsBalancesResponse = await _rpcClient.AccountsBalancesAsync(new string[] { Constants.ReferenceAccount });

            Assert.Equal("15000000000000000000000000000", accountsBalancesResponse.Balances[Constants.ReferenceAccount].Balance);
        }

        [Fact]
        public async void AccountsPendingTest()
        {
            AccountsPendingResponse accountsPendingResponse = await _rpcClient.AccountsPendingAsync(new string[] { Constants.ReferenceAccount });
            
            Assert.NotNull(accountsPendingResponse.Blocks[Constants.ReferenceAccount].First().Value.Amount);
            Assert.NotNull(accountsPendingResponse.Blocks[Constants.ReferenceAccount].First().Value.Source);
        }

        [Fact]
        public async void BlockInfoTest()
        {
            BlockInfoResponse blockInfoResponse = await _rpcClient.BlockInfoAsync("75F0B821DE3B25908755520117660E1297DDEA774DEC817FAA2C27221442403A");

            Assert.Equal("15000000000000000000000000000", blockInfoResponse.Balance);
            Assert.Equal("nano_1iuz18n4g4wfp9gf7p1s8qkygxw7wx9qfjq6a9aq68uyrdnningdcjontgar", blockInfoResponse.Content.Representative);
        }

        [Fact]
        public async void WorkGenerateTest()
        {
            /*const string hash = "3865BFCD423CE3579C4A7C6010CE763BE4C63964AC06BDA451A63BBCAC9E3712";
            WorkGenerateResponse workGenerate = await _rpcClient.WorkGenerateAsync(hash);

            Assert.Equal(hash, workGenerate.Hash);
            Assert.NotNull(workGenerate.Work);*/
        }

        [Fact]
        public async void SendBlockTest()
        {
            /*Account account = Account.FromPrivateKey();
            await _rpcClient.UpdateAccountAsync(account);
            
            var block = Block.CreateSendBlock();

            await _rpcClient.ProcessAsync(block);*/
        }

        [Fact]
        public async void ReceiveBlockTest()
        {
            /*Account account = Account.FromPrivateKey();
            await _rpcClient.UpdateAccountAsync(account);
            
            var block = Block.CreateReceiveBlock();

            await _rpcClient.ProcessAsync(block);*/
        }
    }
}

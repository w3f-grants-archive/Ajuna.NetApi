using System;
using Ajuna.NetApi.Model.Extrinsics;
using Ajuna.NetApi.Model.Types.Base;
using NUnit.Framework;

namespace Ajuna.NetApi.Test.Extrinsic
{
    public class SignedExtensionsTest
    {
        private Random _random;

        [OneTimeSetUp]
        public void Setup()
        {
            _random = new Random();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }

        [Test]
        public void EncodeExtraTest()
        {
            var genesisHash = new byte[]
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            var blockHash = new byte[]
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            var era = new Era(2048, 99, false);

            var genesis = new Hash();
            genesis.Create(genesisHash);

            var startEra = new Hash();
            startEra.Create(blockHash);

            var assetTxPayment = new ChargeAssetTxPayment(0, 0);

            var signedExtensions = new SignedExtensions(259, 1, genesis, startEra, era, 0, assetTxPayment);

            var bytes = Utils.StringValueArrayBytesArray(
                "58, 6, 0, 0, 0, 3, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0");

            Assert.AreEqual(bytes.AsMemory().Slice(0, 5).ToArray(), signedExtensions.GetExtra());
            Assert.AreEqual(bytes.AsMemory().Slice(5, 72).ToArray(), signedExtensions.GetAdditionalSigned());
            Assert.AreEqual(bytes, signedExtensions.Encode());
        }
    }
}
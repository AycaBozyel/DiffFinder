using NUnit.Framework;
using DiffFinder.Business;
using DiffFinder.Models;

namespace DiffFinderTest
{
    public class DiffFinderTests
    {
        private DiffFinderService _diffFinder;
        [SetUp]
        public void Setup()
        {
            _diffFinder = new DiffFinderService();
        }

        [Test]
        public void Same_Size()
        {
            string leftString = "UHJhZw==";
            string rightString = "em1pcg==";
            bool response = _diffFinder.IsSameSize(leftString, rightString);
            Assert.IsTrue(response);
        }


        [Test]
        public void Different_Size()
        {
            string leftString = "UHJhfdaZw==";
            string rightString = "em1apcg==";
            bool response = _diffFinder.IsSameSize(leftString, rightString);
            Assert.IsFalse(response);
        }

        [Test]
        public void Equal_Calculate_Difference()
        {
            var diffrenceInformation = new DiffrenceInformation()
            {
                DifferenceInformationId = 1,
                LeftString = "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9",
                RightString = "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9"
            };
            var response = _diffFinder.CalculateDifference(diffrenceInformation);
            Assert.AreEqual(response.ResultMessage, "Inputs were equal.");
        }

        [Test]
        public void Not_Equal_Calculate_Difference()
        {
            var diffrenceInformation = new DiffrenceInformation()
            {
                DifferenceInformationId = 1,
                LeftString = "QXlha2Fu",
                RightString = "QmVya2Fu"
            };
            var response = _diffFinder.CalculateDifference(diffrenceInformation);
            Assert.AreEqual(response.ResultMessage, "Offsets and lenghts of the diffences.");
        }

        [Test]
        public void DummyTest()
        {
            var diffrenceInformation = new DiffrenceInformation()
            {
                DifferenceInformationId = 1,
                LeftString = "QXlha2Fu",
                RightString = "QmVya2Fu"
            };
            var response = _diffFinder.CalculateDifference(diffrenceInformation);
            Assert.AreEqual(response.ResultMessage, "Offsets and lenghts of the diffences.");
        }
    }
}
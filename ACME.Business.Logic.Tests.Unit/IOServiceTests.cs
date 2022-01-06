using ACME.Business.Logic.Services.Implementations;
using ACME.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ACME.Business.Logic.Tests.Unit
{
    public class IOServiceTests
    {
        private readonly IIOService _iOService;
        public IOServiceTests()
        {
            _iOService = new IOService();
        }
        [Theory]
        [InlineData("01-03-2022-Week1.txt")]
        public void GetPathFile_NotShuldBeEmpty(string fileName)
        {
            //Act
            string path = _iOService.GetFilePath(fileName);
            //Assert
            Assert.NotEqual(string.Empty, path);

        }

        [Theory]
        [InlineData("01-03-2022-Week1.txt")]
        public void ValidPath_ShuldReturnTrue(string fileName)
        {
            //Assert
            Assert.True(_iOService.ValidPath(fileName));
        }

        [Theory]
        [InlineData("C:\\Users\\victor\\source\\repos\\ACME\\ACME.Business.Logic.Tests.Unit\\Inputs\\01-03-2022-Week1.txt")]
        public async Task ReadTextFile_NotShuldBeEmpty(string path)
        {
            //Assert
            Assert.NotEqual(await _iOService.ReadTextFile(path),string.Empty);
        }

    }
}

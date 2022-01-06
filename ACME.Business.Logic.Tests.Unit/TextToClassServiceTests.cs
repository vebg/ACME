using ACME.Business.Logic.Services.Implementations;
using ACME.Services.Interfaces;
using Xunit;

namespace ACME.Business.Logic.Tests.Unit
{
    public class TextToClassServiceTests
    {

        private readonly ITextToClassService _textToClassService;

        public TextToClassServiceTests()
        {
            _textToClassService = new TextToClassService();
        }
        [Fact]
        public void TextToEmployees_ShuldNotBeEmpty()
        {
            //Act
            string text = "RENE=MO10:15-12:00,TU10:00-12:00,TH13:00-13:15,SA14:00-18:00,SU20:00-21:00 " +
 "ASTRID = MO10:00 - 12:00,TH12: 00 - 14:00,SU20: 00 - 21:00";

            var employess = _textToClassService.TextToEmployees(text);
            //Assert
            Assert.True(employess.Count > 0);

        }
    }
}

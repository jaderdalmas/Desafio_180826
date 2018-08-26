using System;
using Xunit;
using BlackJack.Services;

namespace BlackJack.Teste
{
    public class JogosTest
    {
        private readonly JogosServices _service;

        public JogosTest()
        {
            _service = new JogosServices();
        }

        [Fact]
        public void NovoJogoNotNull()
        {
            var result = _service.NovoJogo();

            Assert.False(result == null, "jogo mustn't be null");
        }
    }
}

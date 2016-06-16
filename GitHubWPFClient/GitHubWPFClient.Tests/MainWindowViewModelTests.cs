using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubWPFClient.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void PassingNullToConstructorThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>new MainWindowViewModel(null));
        }
    }
}

using GitHubApiWrapper;
using Moq;
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

        [Test]
        public void GettingInvalidUserSetsStatusMessage()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Throws<ArgumentException>();

            Assert.IsNull(vm.StatusMessage);
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsNotNull(vm.StatusMessage);
        }
    }
}

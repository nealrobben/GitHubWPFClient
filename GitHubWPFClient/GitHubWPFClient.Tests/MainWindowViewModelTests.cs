using GitHubApiWrapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Assert.IsTrue(string.IsNullOrWhiteSpace(vm.StatusMessage));
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsFalse(string.IsNullOrWhiteSpace(vm.StatusMessage));
        }

        [Test]
        public void GettingValidUserClearsStatusMessage()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Returns(new Mock<IUser>().Object);
            mock.Setup(foo => foo.GetRepositoriesForUser("testuser")).Returns(new List<IRepository>());

            vm.StatusMessage = "test";
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsTrue(string.IsNullOrWhiteSpace(vm.StatusMessage));
        }

        [Test]
        public void GettingValidUserSetsUserProperty()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Returns(new Mock<IUser>().Object);
            mock.Setup(foo => foo.GetRepositoriesForUser("testuser")).Returns(new List<IRepository>());

            Assert.IsNull(vm.User);
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsNotNull(vm.User);
        }

        [Test]
        public void GettingInvalidUserClearsUserProperty()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Throws<ArgumentException>();

            var userMock = new Mock<IUser>();
            vm.User = userMock.Object;
            Assert.IsNotNull(vm.User);
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsNull(vm.User);
        }

        [Test]
        public void GettingValidUserSetsRepositoriesProperty()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Returns(new Mock<IUser>().Object);
            mock.Setup(foo => foo.GetRepositoriesForUser("testuser")).Returns(new List<IRepository>());

            Assert.IsNull(vm.Repositories);
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsNotNull(vm.Repositories);
        }

        [Test]
        public void GettingInvalidUserClearsRepositoriesProperty()
        {
            var mock = new Mock<IGitHubWrapper>();
            var vm = new MainWindowViewModel(mock.Object);
            mock.Setup(foo => foo.GetUser("testuser")).Throws<ArgumentException>();

            var repositoriesMock = new ObservableCollection<IRepository>();
            vm.Repositories = repositoriesMock;
            Assert.IsNotNull(vm.Repositories);
            vm.UserName = "testuser";
            vm.GetUserCommand.Execute(null);
            Assert.IsNull(vm.Repositories);
        }
    }
}

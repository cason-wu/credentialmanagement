using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CredentialManagement.Test
{
    [TestClass]
    public class CredentialTests
    {
        [TestMethod]
        public void Credential_Create_ShouldNotThrowNull()
        {
            new Credential().Should().NotBeNull();
        }

        [TestMethod]
        public void Credential_Create_With_Username_ShouldNotThrowNull()
        {
            new Credential("username").Should().NotBeNull();
        }

        [TestMethod]
        public void Credential_Create_With_Username_And_Password_ShouldNotThrowNull()
        {
            new Credential("username", "password").Should().NotBeNull();
        }
        [TestMethod]
        public void Credential_Create_With_Username_Password_Target_ShouldNotThrowNull()
        {
            new Credential("username", "password","target").Should().NotBeNull();
        }

        [TestMethod]
        public void Credential_ShouldBe_IDisposable()
        {
            Assert.IsTrue(new Credential() is IDisposable, "Credential should implement IDisposable Interface.");
        }
        
        [TestMethod]
        public void Credential_Dispose_ShouldNotThrowException()
        {
            new Credential().Dispose();
        }
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Credential_ShouldThrowObjectDisposedException()
        {
            Credential disposed = new Credential {Password = "password"};
            disposed.Dispose();
            disposed.Username = "username";
        }

        [TestMethod]
        public void Credential_Save()
        {
            Credential saved = new Credential("username", "password", "target", CredentialType.Generic);
            saved.PersistanceType = PersistanceType.LocalComputer;
            saved.Save().Should().BeTrue();
        }
        
        [TestMethod]
        public void Credential_Delete()
        {
            new Credential("username", "password", "target").Save();
            new Credential("username", "password","target").Delete().Should().BeTrue();
        }

        [TestMethod]
        public void Credential_Delete_NullTerminator()
        {
            Credential credential = new Credential((string)null, (string)null, "\0", CredentialType.None);
            credential.Description = (string)null;
            credential.Delete().Should().BeFalse();
        }
       
        [TestMethod]
        public void Credential_Load()
        {
            Credential setup = new Credential("username", "password", "target", CredentialType.Generic);
            setup.Save();

            Credential credential = new Credential {Target = "target", Type = CredentialType.Generic };
            credential.Load().Should().BeTrue();

            credential.Username.Should().NotBeEmpty();
            credential.Password.Should().NotBeNull();
            credential.Username.Should().Be("username");
            credential.Password.Should().Be("password");
            credential.Target.Should().Be("target");
        }

        [TestMethod]
        public void Credential_Exists_Target_ShouldNotBeNull()
        {
            new Credential { Username = "username", Password = "password", Target = "target" }.Save();
            
            Credential existingCred = new Credential {Target = "target"};
            existingCred.Exists().Should().BeTrue();
            
            existingCred.Delete();
        }
    }
}

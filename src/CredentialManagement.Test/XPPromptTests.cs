using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CredentialManagement.Test
{
    [TestClass]
    public class XPPromptTests
    {
        static string MAX_LENGTH_VALIDATION_TEXT;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 50000; i++)
            {
                sb.Append('A');
            }
            MAX_LENGTH_VALIDATION_TEXT = sb.ToString();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MAX_LENGTH_VALIDATION_TEXT = null;
        }

        [TestMethod]
        public void XPPrompt_Create_ShouldNotBeNull()
        {
            new XPPrompt().Should().NotBeNull();
        }

        [TestMethod]
        public void XPPrompt_ShouldImplement_IDisposable()
        {
            Assert.IsTrue(new XPPrompt() is IDisposable, "XPPrompt should implement IDisposable interface.");
        }

        [TestMethod]
        public void XPPrompt_Username_MaxLength()
        {
            Action act = () => new XPPrompt {Username = MAX_LENGTH_VALIDATION_TEXT};
            act.Should().Throw<ArgumentOutOfRangeException>();
        }  
        
        [TestMethod]
        public void XPPrompt_Username_NullValue()
        {
            Action act = () => new XPPrompt { Username = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void XPPrompt_Password_NullValue()
        {
            Action act = () => new XPPrompt {Password = null};
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void XPPrompt_Target_NullValue()
        {
            Action act = () => new XPPrompt {Target = null};
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void XPPrompt_Message_MaxLength()
        {
            Action act = () => new XPPrompt { Message = MAX_LENGTH_VALIDATION_TEXT };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void XPPrompt_Message_NullValue()
        {
            Action act = () => new XPPrompt { Message = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void XPPrompt_Title_MaxLength()
        {
            Action act = () => new XPPrompt {Title = MAX_LENGTH_VALIDATION_TEXT};
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void XPPrompt_Title_NullValue()
        {
            Action act = () => new XPPrompt { Title = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_ShouldNotThrowError()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ShowDialog();
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_WithParent_ShouldNotThrowError()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_Without_Target_ShouldThrowError()
        {
            XPPrompt prompt = new XPPrompt();
            Action act = () => prompt.ShowDialog();
            act.Should().Throw<InvalidOperationException>();
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_With_Username()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.Username = "username";
            prompt.Title = "Enter enter valid credentials.";
            prompt.Message = "Please enter valid credentials.";
            prompt.ShowSaveCheckBox = true;
            prompt.GenericCredentials = true;
            prompt.ShowDialog().Should().Be(DialogResult.OK);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_AlwaysShowUI()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.AlwaysShowUI = true;
            prompt.GenericCredentials = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_AlwaysShowUI_Without_GenericCredentials_ShouldThrowError()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.AlwaysShowUI = true;
            Action act = () => prompt.ShowDialog(IntPtr.Zero);
            act.Should().Throw<InvalidOperationException>();
            prompt.Dispose();
        }
        
        [TestMethod]
        public void XPPrompt_ShowDialog_CompleteUsername()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.CompleteUsername = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_DoNotPersist()
        {
            XPPrompt prompt = GetDefaultPrompt();
                    prompt.DoNotPersist = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }
        
        [TestMethod]
        public void XPPrompt_ShowDialog_ExcludeCertificates()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ExcludeCertificates = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_ExpectConfirmation()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ExpectConfirmation = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_GenericCredentials()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.GenericCredentials = true;
            prompt.ShowDialog(IntPtr.Zero).Should().Be(DialogResult.OK);
            prompt.Dispose();
        }
        
        [TestMethod]
        public void XPPrompt_ShowDialog_IncorrectPassword()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.IncorrectPassword = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_Persist()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.Persist = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_RequestAdministrator()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.RequestAdministrator = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_RequreCertificate()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.RequireCertificate = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_RequreSmartCard()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.RequireSmartCard = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_ShowSaveCheckBox()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ShowSaveCheckBox = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_UsernameReadOnly()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.UsernameReadOnly = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void XPPrompt_ShowDialog_ValidateUsername()
        {
            XPPrompt prompt = GetDefaultPrompt();
            prompt.ValidateUsername = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        private XPPrompt GetDefaultPrompt()
        {
            XPPrompt prompt = new XPPrompt {Target = "target"};
            return prompt;
        }
    }
}

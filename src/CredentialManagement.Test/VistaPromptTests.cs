using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CredentialManagement.Test
{
    [TestClass]
    public class VistaPromptTests
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
        public void VistaPrompt_Create_ShouldNotBeNull()
        {
            new VistaPrompt().Should().NotBeNull();
        }

        [TestMethod]
        public void VistaPrompt_ShouldImplement_IDisposable()
        {
            Assert.IsTrue(new VistaPrompt() is IDisposable, "VistaPrompt should implement IDisposable interface.");
        }

        [TestMethod]
        public void VistaPrompt_Username_MaxLength()
        {
            Action act = () => new VistaPrompt { Username = MAX_LENGTH_VALIDATION_TEXT };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void VistaPrompt_Username_NullValue()
        {
            Action act = () => new VistaPrompt { Username = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void VistaPrompt_Password_NullValue()
        {
            Action act = () => new VistaPrompt { Password = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void VistaPrompt_Message_MaxLength()
        {
            Action act = () => new VistaPrompt { Message = MAX_LENGTH_VALIDATION_TEXT };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void VistaPrompt_Message_NullValue()
        {
            Action act = () => new VistaPrompt { Message = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void VistaPrompt_Title_MaxLength()
        {
            Action act = () => new VistaPrompt { Title = MAX_LENGTH_VALIDATION_TEXT };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void VistaPrompt_Title_NullValue()
        {
            Action act = () => new VistaPrompt { Title = null };
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void VistaPrompt_ShowDialog_ShouldNotThrowError()
        {
            VistaPrompt prompt = GetDefaultPrompt();
            prompt.ShowDialog();
            prompt.Dispose();
        }

        [TestMethod]
        public void VistaPrompt_ShowDialog_WithParent_ShouldNotThrowError()
        {
            VistaPrompt prompt = GetDefaultPrompt();
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }


        [TestMethod]
        public void VistaPrompt_ShowDialog_With_Username()
        {
            VistaPrompt prompt = GetDefaultPrompt();
            prompt.Username = "username";
            prompt.ShowDialog().Should().Be(DialogResult.OK);
            prompt.Dispose();
        }

        [TestMethod]
        public void VistaPrompt_ShowDialog_GenericCredentials()
        {
            VistaPrompt prompt = GetDefaultPrompt();
            prompt.Title = "Please provide credentials";
            prompt.GenericCredentials = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [TestMethod]
        public void VistaPrompt_ShowDialog_ShowSaveCheckBox()
        {
            VistaPrompt prompt = GetDefaultPrompt();
            prompt.ShowSaveCheckBox = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        private VistaPrompt GetDefaultPrompt()
        {
            return new VistaPrompt();
        }
    }
}

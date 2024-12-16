using ByteCobra.Logging.Settings;
using NUnit.Framework;
using System;
using System.IO;

namespace ByteCobra.Logging.Tests
{
    public class LogTests
    {
        private const string TestLogMessage = "This is a test log message.";
        private const string TestColor = "cyan";
        private const string TestSourceFilePath = "path/to/test/File.cs";
        private const int TestSourceLineNumber = 123;

        [SetUp]
        public void Setup()
        {
            // Reset the settings before each test
            LogSettings.FileSettings.SaveStates = true;
            LogSettings.ColorSettings.DebugLogColor = TestColor;
            LogSettings.ColorSettings.InfoLogColor = TestColor;
            LogSettings.ColorSettings.WarningLogColor = TestColor;
            LogSettings.ColorSettings.ErrorLogColor = TestColor;
            LogSettings.ColorSettings.AssertLogColor = TestColor;
        }

        [TearDown]
        public void TearDown()
        {
            Log.DeleteLogFiles();
        }

        [Test]
        public void TestDebugLog()
        {
            Log.Debug(TestLogMessage, TestSourceFilePath, TestSourceLineNumber);
            NUnit.Framework.Assert.IsTrue(File.Exists(Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName)));
        }

        [Test]
        public void TestInfo()
        {
            Log.Info(TestLogMessage, TestSourceFilePath, TestSourceLineNumber);
            NUnit.Framework.Assert.IsTrue(File.Exists(Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName)));
        }

        [Test]
        public void TestWarning()
        {
            Log.Warning(TestLogMessage, TestSourceFilePath, TestSourceLineNumber);
            NUnit.Framework.Assert.IsTrue(File.Exists(Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName)));
        }

        [Test]
        public void TestError()
        {
            Log.Error(TestLogMessage, false, TestSourceFilePath, TestSourceLineNumber);
            NUnit.Framework.Assert.IsTrue(File.Exists(Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName)));
        }

        [Test]
        public void TestAssert()
        {
            Log.Assert(false, TestLogMessage, false, TestSourceFilePath, TestSourceLineNumber);
            var path = Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName);
            NUnit.Framework.Assert.IsTrue(File.Exists(path));
        }

        [Test]
        public void TestFatal()
        {
            NUnit.Framework.Assert.Throws<Exception>(() => Log.Fatal(TestLogMessage, false, TestSourceFilePath, TestSourceLineNumber));
            NUnit.Framework.Assert.IsTrue(File.Exists(Path.Combine(LogSettings.FileSettings.LogFilesDirectory.FullName, LogSettings.FileSettings.FileName)));
        }
    }
}
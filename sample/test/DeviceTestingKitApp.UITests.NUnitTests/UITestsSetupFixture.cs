﻿using DeviceRunners.UIAutomation;
using DeviceRunners.UIAutomation.Appium;

using NUnit.Framework.Internal;

namespace DeviceTestingKitApp.UITests.NUnitTests;

[SetUpFixture]
public class UITestsSetupFixture
{
	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		var builder = AutomationTestSuiteBuilder.Create()
			.AddAppium(options => options
				.UseServiceAddress("127.0.0.1", 4723)
				.AddLogger(new TestContextLogger())
				.AddAndroidApp("android", options => options
					.UsePackageName("com.companyname.devicetestingkitapp")
					.UseActivityName(".MainActivity"))
				.AddWindowsApp("windows", options => options
					.UseAppId("com.companyname.devicetestingkitapp_9zz4h110yvjzm!App")))
			;
		//.AddSelenium(options => options
		//	.AddWebApp("https://dot.net/"));

		TestSuite = builder.Build();
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		TestSuite.Dispose();
	}

	public static AutomationTestSuite TestSuite { get; private set; }

	class TestContextLogger : IAppiumDiagnosticLogger
	{
		public void Log(string message) =>
			TestContext.Out.WriteLine(message);
	}
}

# extjswebdriver

## What's this?

ExtJS 4 &amp; Selenium WebDriver C# (.NET 4) framework for easily writing js-heavy scenario tests. 

### Features

There are a couple of baseline element classes you can work with while writing WebDriver tests. Featuers:

* Async waiting: don't just wait x seconds when an Extjs view is loaded to start clicking, but use a clever mechanism.
* DOM-to-C# class mapping using WebDriver attributes - initialize them via the WebDriver PageFactory.
* (Extjs) Javascript utility methods (close all views, tooltips, evaluate JS files, ...) provided.
* Handy WebElement extensions (DoubleClick, Get/Set values, date specific stuff, fill in random values, HTML contents, ...)
* Exception handling: save a screenshot to a file, save extra info (stacktrace, serverside logging, ...) to a file, and wrap these in an exception.

### Requirements

DLL requirements are handled through Nuget.

* Extjs 4. 
* NUnit >= 2.6.4
* Selenium WebDriver >= 2.44.0 including Selenium Support
* RestSharp (why? see below) >= 105.0.1

* * *

## How should you use this?

Start by extending from `BaseScenarioTestCase` and provide methods for logging in. Every test case should extend from your base class.
Then extend from `ScenarioFixture` to provide an implementation for `ResolveHostAndPort()` - so WebDriver knows where to surf to to test your application.

We use NUnit's `[SetupFixture]` attribute to create a new instance of our concrete scenario fixture. The constructor automatically starts Google Chrome by default:

```C#
   [SetUpFixture]
    public class NUnitFixture
    {
        [SetUp]
        public void SetUp()
        {
            MyScenarioFixture.SetUpFixture();
        }

        [TearDown]
        public void TearDown()
        {
            MyScenarioFixture.TearDownFixture();
        }
    }
```

The `SetUpFixture` method is implemented by creating a new instance of our concrete scenario fixture.

After that's done, you can start creating page objects in C# and using them to walk through the flow of your web application. Every page object should extend from `BaseContainerComponent`.
In the constructor, it automatically waits until the "component is loaded". This is abstract and thus should be implemented. Our default implementation looks like this:

```C#
    public IWebElement Window
    {
        get { return Driver.FindElement(By.CssSelector(".wd-window-" + CssClassname)); }
    }

    public override void WaitUntilComponentLoaded()
    {
        WaitUntil(x => Window.Displayed);
        this.WaitUntilExtLoadingDone();
    }
```

The CSS class is provided via the constructor. All <a href="http://docs.sencha.com/extjs/4.1.3/#!/api/Ext.tree.View" target="_blank">Extjs panels, views</a>, ... are identified through CSS classes set in the definition in the javascript files. This is very important, since we need a way to bind an extjs object to a webdriver `WebElement` to start working with it. Since Extjs generates it's own IDs on every element in the DOM tree, we used classes. Every marker class for webdriver starts with '.wd-'.

### Example unit tests

```C#
    [Test]
    public void CreateSomeClientViaUI()
    {
        OpenClientsWindow()
            .ClickMenuActionAdd()
            .SetName(_CLIENT_NAME)
            .SetStreet("TestStreet")
            .SetCountry("BE")
            .SetZip("3500")
            .SetLanguage("1")
            .ClickSave()
            .ExpectOpened<ClientDetailPanel>();
    }
```

What did we have to do in order to achieve this?

- Implement `OpenClientsWindow`: return a new instance of `ClientsWindow`, which inherits `BaseContainerComponent`.
- Implement methods on the clients window, like `SetName` and `SetStreet`. Use `InputField` or simply `IWebElement` from WebDriver itself. 

Mapping web elements can be done like this:

```C#
    [FindsBy(How = How.CssSelector, Using = ".wd-menu-actions"), UsedImplicitly]
    private IWebElement _ActionsMenu;
```

### Using exception handling

You can use PostSharp like we do to catch any exception and redirect to the provided handler, which in turn saves metadata like the screenshot. Wrap it in another exception to bubble up to the actual NUnit unit test.

```C#
    [ScenarioExceptionAspect(AttributeExclude = true)]
    public class ScenarioExceptionAspect : OnMethodBoundaryAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            var exceptionFileName = Directory.GetCurrentDirectory() + @"/" +
                WebDriverExceptionHandler.Handle(args.Exception, new ServiceLogInfoResolver("log/serviceLog.txt"));
            throw new Exception("Scenario test failed"
                + Environment.NewLine
                + " -- Screenshot: " + exceptionFileName + ".png"
                + Environment.NewLine
                + " -- Log: " + exceptionFileName + ".txt", args.Exception);
        }
    }
```

More info on how this works can be found in <a href="http://brainbaking.com/webdriver-exception-handling/" target="_blank">this blog post</a>.

#### Service logging

`ServiceLogInfoResolver` uses REST to retrieve the logfile from the webserver, since the scenario tests don't have access to those files and they are usually not run on the same machine, so there's no way to retrieve them by simply reading a file. If you have some other way to provide extra information when something goes wrong, implement another `IExceptionLogInfoResolver`. 


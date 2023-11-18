# php2desktop 2

-----

## Working with the source code

This software was built using Visual Studio 2022 build tools and CefSharp. The instructions that follow were written for that specific VS version. That's not to say it won't work with other versions of Visual Studio, just that the methodology may vary depending on the version.

All you need is Visual Studio and 10 minutes, max.

If you're downloading the project from Github without using the command line, then the first thing to do is to unzip the package into a directory of your choosing and double click **php2desktop.sln**, which should open the project in VS22.

### Installing NuGet Packages

The NuGet packages should install automatically upon opening the php2desktop.sln file. If for some reason it fails to, or if you want to use the latest updates, you can install the necessary dependancies in the following manner:

In solution explorer right click on **php2desktop** and select **Manage NuGet Packages**. 

A message bar at the top of the screen should display telling you to click to restore from your online package sources. Click this to restore the packages automatically.

If you can't see the message bar, there is a dropdown menu next to **Package source**, click it and make sure that **nuget.org** is in the list and that either **All** or **nuget.org** is selected. If **nuget.org** isn't there, click on the settings cog next to the dropdown menu. On the window that opens, click the green plus sign at the top of the interface. Change the **Name** from **Package source** to **nuget.org** and change the **Source** from **https://packagesource** to **https://api.nuget.org/v3/index.json**. Click **Update** and **OK**. Then make sure that either **All** or **nuget.org** is selected in the dropdown menu next to **Package source** and search for and install/update the following packages:

- Microsoft.Web.WebView2
- NewtonSoft.Json

### Editing the Properties

In solution explorer right click on **php2desktop** and select **properties**

In **Application**, change **Assembly name** and to the name of your app. Don't just change **MSBuildProjectName**.

Next, change the **Default namespace** with your app's name, replacing any spaces with underscores (_)

Next, in **Solution Explorer**, expand **Properties** and double-click **AssemblyInfo.cs**. Change the information there to match your app's details.

### Changing the Icon
It's a simple case of replacing the **icon.ico** file in the root directory of the project (the same place you opened **php2desktop.sln**). For simplicity, keep the same names and make sure the icons' dimensions are square (100x100, 250x250 etc).

Alternatively, you can specify the icon.ico file in the settings.json file.

### Building the application

Assuming you've completed all of the above, the next thing to do is change **Debug** to **Release**, then click on **Build** and **Build php2desktop**. When the build is completed, you should find it in the following directory

\bin\Release

The contents of this directory is your app. Take the entire contents and copy them to a new directory.

Now go to https://windows.php.net/download#php-8.2 and download the zip of the version you wish to use that matches the CPU you've chosen for your build and extract its contents into the php directory located in the root directory for your app that you created in the last step. If done right, php.exe should be available along with other files and directories withing the php directory itself.

Run your .exe file. You should see the welcome message.

Congratulations, you've just built your own app. Now all that's left to do is change the settings in your settings.json file located in the root directory of your app (if required) and to replace everything in the **www** directory with your own web files.

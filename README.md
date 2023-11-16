# php2desktop

-----

## About

php2desktop gives developers the ability to create content rich native Windows GUI applications using web technologies they're familiar with.

Connect to APIs for interactive applications. Use your favourite framework such as Bootstrap or Laravel and change nothing about your workflow ... Your imagination's your only limit.

-----

## Working with the source code

This software was built using Visual Studio 2022 build tools and CefSharp. The instructions that follow were written for that specific VS version. That's not to say it won't work with other versions of Visual Studio, just that the methodology may vary depending on the version.

You can download the pre-built version [here](https://github.com/ozboware/php2desktop/releases/tag/v1.0.0), but I recommend downloading the source code and building it yourself. All you need is Visual Studio and 10 minutes, max. The main code itself is only 69 lines and 5 of them are empty. I kept it as minimalist as possible to make it as easy as possible to develop native apps quickly and efficiently. Also, the bonus with building it yourself includes being able to use your own properties meta and icons without having to use third party hacks.

If you're downloading the project from Github without using the command line, then the first thing to do is to unzip the package into a directory of your choosing and double click **php2desktop.sln**, which should open the project in VS22.

### Installing CefSharp

The NuGet packages should install automatically upon opening the php2desktop.sln file. If for some reason it fails to, or if you want to use the latest CefSharp Chromium update, you can install the necessary dependancies in the following manner:

First, make sure that **Any CPU** is changed to either x64 or x86

In solution explorer right click on **php2desktop** and select **Manage NuGet Packages**. There is a dropdown menu next to **Package source**, click it and make sure that **nuget.org** is in the list and that either **All** or **nuget.org** is selected. If **nuget.org** isn't there, click on the settings cog next to the dropdown menu. On the window that opens, click the green plus sign at the top of the interface. Change the **Name** from **Package source** to **nuget.org** and change the **Source** from **https://packagesource** to **https://api.nuget.org/v3/index.json**. Click **Update** and **OK**. Then make sure that either **All** or **nuget.org** is selected in the dropdown menu next to **Package source** and search for and install/update the following packages:

- CefSharp.Wpf.NETCore
- CefSharp.Common.NETCore

### Editing the Properties

In solution explorer right click on **php2desktop** and select **properties**

In **Application** - **General**, scroll down to **Assembly name** and change it from **$(MSBuildProjectName)** to the name of your app. Don't just change **MSBuildProjectName**. Completely empty the input and change it to the name of your app

Next, change the **Default namespace** with your app's name, replacing any spaces with underscores (_)

Scroll down to **Build** - **General**. Specify your **Platform target**. The default is 32 bit

Scroll down to **Package**. Here you can decide whether or not to *Generate NuGet package on build** and edit properties such as **Product name**, **Description**, **Copyright**, **Project URL**, **Authors** and **Company**. I recommend leaving the icon fields as they are and just changing out the files in the root directory with your own icons.

### Editing Window Properties

In the solution explorer, double click on **MainWindow.xaml** file. In the xaml code you can change the title that will display at the top of the window as well as its starting width and height.

### Changing the Icon
It's a simple case of replacing the **icon.png** and **icon.ico** files in the root directory of the project (the same place you opened **php2desktop.sln**). For simplicity, keep the same names and make sure the icons' dimensions are square (100x100, 250x250 etc).

### Building the application

Assuming you've completed all of the above, the next thing to do is change **Debug** to **Release**, then click on **Build** and **Build php2desktop**. When the build is completed, you should find it in the following directory

\bin\x86\Release\net6.0-windows

x86 is the CPU build you chose earlier. The contents of this directory is your app. Take the entire contents and copy them to a new directory, then, in its new directory, create a directory called **www** and another called **php** (lowercase).

Now go to https://windows.php.net/download#php-8.2 and download the zip of the version you wish to use that matches the CPU you've chosen for your build and extract its contents into the php directory you created for your app in the last step. If done right, php.exe should be available along with other files and directories withing the php directory itself.

The final step is to add an index.php file to the **www** directory, fill it with Hello World! and run your exe file. That should be it.

-----

## Comments

A drawback to the current solution is that it doesn't play MP4 videos by default. I have found a solution to this problem and am currently incorporating it into the next version which is going to be even more powerful, with more options and giving you the ability to create your own video players and change settings on the fly without having to play about too much with the code. That update should be ready before December 2023

-----

## Version

1.0.0

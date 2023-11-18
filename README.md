# php2desktop

-----

## About

From time to time over the last few years, I've found myself looking for PHP to exe compilers so I could make some desktop applications using a web based tech stack. Although there were some good ones available at one point, they seem to hae stagnated in their development. Either the Chromium version is too far out of date to be useful for modern applications, or they rely on having certain PHP versions or Chromium versions in order to build. Looking over the documentation and online interactions weren't a comfort, either. Messages of how many hours it would take to compile everything and still end up with an out of date software left me walking away from project ideas every time.

This year, I looked again because and found the situation hadn't improved any. The last one that was open source and of any real use didn't work out well with the Github API authorisation button because the Chromium ersion was like 50 versions out of date and there wasn't a certain plugin available. Trawling through the documents to see how we could remedy this turned up nothing. But, instead of giving up, I decided to download the source code and see what I could do to update things. After a couple of hours, I gave up and decided to run a blank project to see if I could make it in a more simplified fashion.

That's when I came up with version 1 of the software. Very basic. 65 lines of code, a couple of NuGet packages and I had exactly what I've been looking for. Then ... another road block ... it doesn't play mp4 files! Yes, I knew this was common amongst all the similar software. Something to do with a codec or something not being installed with Chromium by default. But, thanks to all the practice I've been having with Visual Studio, the solution to that was a simple change from using cefSharp to WebView2. I also decided to add in a settings.json file as well to easily modify the Window, Browser-UI and PHP settings and php2desktop 2 was born.

-----

## php2desktop 1.0.0

php2desktop 1.0.0 utilises CefSharp for the Browser-UI. It's limited with its settings and, like all available alternatives, doesn't allow the playback of MP4 files. It's good for most projects, but if you're looking at throwing in some videos, it's practically useless.

-----

## php2desktop 2.0.0

php2desktop 2.0.0 utilises WebView2 for the Browser-UI. It includes a settings file to easily manipulate window, PHP and browser settings after the build and, unlike any similar alternatives, does allow the playback of MP4 files. It's the most powerful version of its kind of software available open source.

-----

## Comments

Both versions have been made in a way that allows you to use the latest version of Chromium in the easiest possible way. There's no need to compile a Chromium version. It's not dependant on a specific Chromium version. It's not dependant on a specific VS version and more importantly it's not going to take you days or even hours to build with the latest updates. All you need is minutes. Even a first-timer should have their first build with the latest updates in under half an hour, it really is that simple.

If there are any additional features you want to see added, open up a ticket and let me know. If there are any additional features you want to add yourself, feel free to fork and drop some commits.

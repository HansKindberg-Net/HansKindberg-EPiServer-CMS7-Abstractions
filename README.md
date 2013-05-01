HansKindberg.EPiServer.Cms7.Abstractions
========================================
Abstract types from EPiServer CMS 7 to be used in EPiServer 4, 5 and 6 libraries.

Important
---------
To be able to build this solution you have to create a NuGet-package, "EPiServer.Licensing" (6.2.267.1), and add it to a folder with the following path, "C:\Data\NuGet-packages".
Use NuGet Package Explorer GUI to create the package, download: http://nuget.codeplex.com/downloads/get/clickOnce/NuGetPackageExplorer.application?releaseId=59864&ProjectName=nuget, information: http://docs.nuget.org/docs/creating-packages/using-a-gui-to-build-packages.
The "nuspec" should look like this:

<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd">
    <metadata>
        <id>EPiServer.Licensing</id>
        <version>6.2.267.1</version>
        <title>EPiServer.Licensing</title>
        <authors>EPiServer AB</authors>
        <owners>EPiServer AB</owners>
        <licenseUrl>http://world.episerver.com/PageFiles/99654/EPiServer EULA.txt</licenseUrl>
        <requireLicenseAcceptance>true</requireLicenseAcceptance>
        <description>EPiServer license handler.</description>
        <copyright>\x00a9 2003-2010 by EPiServer AB. All rights reserved</copyright>
    </metadata>
</package>

and the content like this:
lib
    net20
        EPiServer.Licensing.dll

To get the EPiServer.Licensing.dll, version 6.2.267.1, copy it from an existing EPiServer 6.1.379.0 site, or install a EPiServer 6.1.379.0 site and copy it.


# Service Fabric Tooling

Microsoft has two sets of tooling for Service Fabric and they are _very_ different. Until this is a unified set of tooling, it's best to have an understanding about where we currently are.

## Why did I write this?
The purpose of this guide is to get you up to speed on the specifics on the  two different tooling methods and how they practically differ. There didn't seem to be any documentation out there on the web comparing the two, so hopefully this saves you a couple of hours just getting to this point, yourselves.

## TL;DR
* 


## What Parts Make Up Service Fabric Application Definition
**ApplicationManifest.xml**: Declaratively describes the application type and version. One or more service manifests of the constituent services are referenced to compose an application type. Configuration settings of the constituent services can be overridden using parameterized application settings. Default services, service templates, principals, policies, diagnostics set-up, and certificates can also declared at the application level.
* Defines values for ApplicationParameters that can be overrideen by a given Application Parameter XML file.
* Defines the service packages and versions that make up a given application.
* Defines what services are immediately provisioned when an instance of the application is provisioned on the fabric.
    * Defines partitioning scheme
    * _Don't get this wrong for StatefulServices as there is currently no way to change partitioning scheme after an application has been created._

**Application Parameter XML file**: Contains values that can override the defaults defined in the ApplicationManifest.xml file.

_Windows note_

    By default, these are 1:1 with publish profiles XML files, however that is just a convention. Application Parameter XML files are actually sent to the cluster, wheras PublishProfile xml files are essentially a collection of parameters tightly coupled with the Windows-tooling-specific Deploy-FabricApplication.ps1 file.

**ServiceManifest.xml**: Declaratively describes the service type and version. It lists the independently upgradeable code, configuration, and data packages that together compose a service package to support one or more service types. Resources, diagnostics settings, and service metadata, such as service type, health properties, and load-balancing metrics, are also specified.

## Visual Studio for Windows

In good old Visual Studio, there are Service Fabric Application projects. They end in the `.sfproj` file extension. They are explicitly `.NET Framework`, and not `.NET Core` projects. This seems like a poorly thought-out workaround for some reason as there is actually **ZERO** CLR code in the project itself.

### Summary
Deploy-FabricApplication.ps1 is fed the path to a publish profile XML file along with other optional parameters. The publish profile XML file points to an application parameters XML file and any parameters required to connect/authenticate to the Service Fabric cluster.

Even if this runs in PowerShell-Core on OSX or Linux, the tooling looks for certificates in the Windows certifacte store instead of from something more compatible such as PEM files.

### Solution Structure
```
ServiceFabricApplication.sfproj
└──ApplicationPackageRoot
  |  ApplicationManifest.xml
└──ApplicationParameters
  |  Cloud.xml
  |  Local.1Node.xml
  |  qa-environment.xml
    ...
└──PublishProfiles
  |  Cloud.xml
  |  Local.1Node.xml
  |  qa-environment.xml
     ...
└──Scripts
  |  Deploy-FabricApplication.ps1
└──
```
### What gets deployed to the fabric?
After compiling the Service Fabric Application project, there will be a `pkg` directory alongside other non-source directories such as `bin` and `obj`. The only purpose of theThis will be populated with the contents described below.
```
└──pkg
   └──[Debug || Release || other Build Profile]
      |  ApplicationManifest.xml
      └──Something.Matching.ServiceManifestName.From.AppManifest.SomeServicePkg
        |  ServiceManifest.xml
        └──Code
           |  Everything in the service's build output directory
        └──Config
           |  Settings.xml
        └──Data
      └─Something.Matching.ServiceManifestName.From.AppManifest.AnotherServicePkg 
        ...      
```

### How does it get deployed
`Deploy-FabricApplication` does all of the work both in-and-out of Visual Studio. This PowerShell script takes a number of parameters closely tied to the the Visual Studio form that pops-up when you right-click on a Service Fabric Application project and click `Publish`.
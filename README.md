# service-fabric-reference-architecture
An [IDesign Method][idesign-method-download]-informed architecture of a single unit of deployment hosted in [Service Fabric][service-fabric-overview].

## Scope
The purpose of this vertical slice is to demonstrate how to implement a single core use case, sometimes called a business capability, using [Service Fabric Remoting][service-fabric-remoting]. This implementation is a narrow view of a larger system for acquiring, managing, and consuming a library of audiobooks. For additional context on the rest of the system, see the broader system architecture [here][audiobook-design].

### Core Use Case
Only one core use cases will be implemented in this vertical slice.

**Format Assets**: The core use case being implemented in this example is one that reformats audiobook files. Providing a predictable format for audiobook assets isolates the consumption end of the system from the explosion of formats and reduces overall complexity.
![format-assets.png](./design/diagrams/format-assets.png)

## Architecture
The system is use case driven broken down using [volatility-based decomposition][vbd-video] (VBD). VBD means that instead of isolating components based on function, they are isolated based on how parts change over time.

### Volatilies
These are the volitilities that have been identified in this part of the system.

#### Acquisition
In this system, there are several core use cases, however, use cases realted to managing one's library are going to change very differently than those that are related to managing overall acquisition and preparation of audiobook resources. 
The Acquisition Manager is responsible for orchestrating calls to components, error compensation, and publishing events related to the use case. It *owns* the use case. As a Manager, it is the single point of entry from client applications. _Note: Client applications are typically web applications, API servers, and scheduled jobs._

#### Formatting
The Formatting Engine 
* Audiobooks may come in with different packaging, encoding, and file formats (mp3, m4a, m4v, m4b, aac, etc.).
* Audiobooks may come in with different audio qualities, and may need to be reviewed.
    * Because sometimes files have ticks or encoding errors
    * Because sometimes the book has been mislabeled
    * Because sometimes unexpected file naming schemes are not arranged in the expected order
    * Because sometimes the book has been read by Scott Brick


#### Asset Resources
Metadata and file access are accessed.

[audiobook-design]: ./design/README.md
[idesign-method-download]: http://www.idesign.net/Downloads/GetDownload/1902
[vbd-video]: https://www.youtube.com/watch?v=VIC7QW62-Tw
[service-fabric-overview]: https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-overview
[service-fabric-remoting]: https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-reliable-services-communication-remoting
# Pixie
This is the next version of Pixie. The primary focus is on improving sampler
support. Secondary are general architecture and code clarity without sacrificing
too much performance.

## Features
* Pixie is now a single assembly
* Improved sampler support
* Imporved camera support
* General improved architecture
* Uses `Linie` ray tracing kernel

## Compatibility
All of the former Pixie support is still here. This new version is backwards compatible for now. Some classes are marked obsolete and you might get some warnings but everything should still work as expected. This version brings new things but should not break existing code.

## Tutorial
There are no formal cameras yet for this version. However, that should not stop anybody from rendering images. In fact, clients will now have much more control over how their images are rendered.


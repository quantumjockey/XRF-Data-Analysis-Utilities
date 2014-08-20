XRF Data Analysis Utilities
=============

XRF Data Analysis Utilities supplies the materials scientist with tools for analyzing X-Ray Flourescence patterns, including (at current): separation of element data by layer, false-color data counts rendering, single-element rendering, three-element rendering, temperature adjusment, data zoom, image scaling, image markup, image export (with axes and markup), and multiple simultaneous workspace capabilities.

This software is a collaborative effort among [Nicola B. DiPalma](http://nicoladipalma.com/), [Oliver Tschauner, Ph.D.](http://geoscience.unlv.edu/people/olivertschauner.html), and scientists at the [University of Nevada, Las Vegas High Pressure Science and Engineering Center (HiPSEC)](http://hipsec.unlv.edu/). This software is intended to augment current data analysis capabilities at HiPSEC and is freely available through this repository for use by members of the scientific community. A page containing downloads for application binaries is coming soon. This project represents the culmination of four months of research and [subsequent] development, as of this writing. August 17th, 2014


Dependencies
------------

The repository requires the following components:

* [CompUhaul](https://github.com/quantumjockey/CompUhaul) - Foundation for internal file management framework.
* [LookinSharp](https://github.com/quantumjockey/LookinSharp) - Rendering library for generating data visualizations.
* [TheseColorsDontRun](https://github.com/quantumjockey/TheseColorsDontRun) - Color ramps for use with data visualization components.
* [WpfHelper](https://github.com/quantumjockey/WpfHelper) - Foundation for MVVM implementation.

These can be cloned from their respective repositories (links included above). DLL binaries for these libraries and their dependencies will be made available for download on both the [laboratory site](http://hipsec.unlv.edu/) and [Nicola's portfolio site](http://nicoladipalma.com/). Follow the instructions included in the README for each repository to ensure proper setup. Once cloned, the references to each must be corrected in the Visual Studio Solution file. A walk-through for this process is included below.
		

Notes
-----

Code in this application has been composed in a self-documenting fashion and styled for readability where possible. XML documentation for use with VS Intellisense is limited since reuse of code not otherwise encapsulated in external libraries is unlikely at this time. Comments have been placed within source where readability is limited, and where functionality is a direct consequence of material esoteric to crystallography and materials science.

The solution associated with this application, including unit tests and architectural models, was created, debugged, and deployed using [Visual Studio 2012 Ultimate with MSDN](http://en.wikipedia.org/wiki/Microsoft_Visual_Studio#Visual_Studio_2012) (link contains addtional references). The solution may not open properly if you try using an earlier or less feature-saturated version of Visual Studio. If a later version is used, be sure to check the cloned solution against original source code to ensure that compatiblity changes haven't significantly altered existing functionality.

Instructions
------------

To get your machine ready for development with this repository:

1. Clone the repository to your machine.
2. Navigate to the directory you cloned your repository to.
3. Locate the Visual Studio 2012 solution file.
3. Open the solution in Visual Studio (2012 or later)

Viola! You're good to go!

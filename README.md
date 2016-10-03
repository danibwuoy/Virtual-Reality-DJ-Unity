# VR-DJ - An immersive Google Cardboard music experience
Unity VR Equalizer - Immersive Music experience for google cardboard

Synopsis: VR display of audio visualization, showing different freq bands (or perhaps spatially if we can parse spatial info from the audio file). User can control the gain of individual bands, as if controlling an audio equalizer.

![alt text](https://i.imgur.com/yx5UsUX.jpg "Preview")

Made at SD Hacks 2016. 

##Here's a high level explanation of how it works:
-Run FFT on our time domain signal (aka. the song's spectral data, which can be captured by Unity)
-Render that in the frequency domain with amplitudes averaged out over our 24 frequency ranges/bands visualizer with transformations of our cubes via the height. (We can do this because we ran FFT; think of the visualizer as a chart with frequency on the X axis and amplitudes on the Y axis.
-Add fancy effects (acceleration on beat/tempo detection, perlin noise wave on the bottom, infinite stars, and skybox. Also add bloom shader because we don't have unity pro, so you have to use something like the CinematicsEffects library.)
-Then for the working equalizer, just script a paramEQ for our frequency bands that we want to change. Implement with google cardboard controls by detected when in front, and toggle/control EQ with the cardboard button.

##Potential Additions
-Full DJ Suite
-HTC Vive Support
-Actual Android APK (production ready APK, that is)

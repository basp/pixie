# Pixie
Pixie is an an implementation of the ray-tracer described in the excellent 
[The Ray-Tracer Challenge](http://#) book by **Jamis Buck**. This is a complete 
rewrite that is based on `System.Numerics` for the following reasons:
1. People much smarter than me have already supplied the matrix math stuff.
2. There's also the opportunity to dip into generic math.
3. It is a stable and well documented interface.
4. It is optimized to run on the GPU.

Preliminary tests show that Pixie can be at least 2 times faster (potentially
up to 10x for some operations) when performing basic vector matrix math when
using `System.Numerics` (versus `struct` based `Linie` based library).
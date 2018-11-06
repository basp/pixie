# pixie

<img src="https://i.imgur.com/IhD4pbr.png">

This is a set of experimental classes for computer graphics. The main set pieces are:

* `PixieCanvas` which is useful to draw on a unit canvas (it's useful for plotting and drawing pictures in a plane)
* `Turtle` which is a very basic turtle graphics class.

Both implementations are very much in sandboxing/experimental stage. Use at own risk.

## the pixie canvas
The `PixieCanvas` was designed to provide a uniform canvas for experimentation that automatically scales and/or translates results from any coordinate system appropriately to a bitmap representation.

### overview
The `PixieCanvas` is a `(-1, -1)` to `(1, 1)` canvas where `(-1, -1)` is the bottom left coordinate and `(1, 1)` is the top right coordinate in a plane. The main differences between drawing with `System.Drawing.Graphics` and `PixieCanvas` are: 
* `System.Drawing.Graphics` uses a coordinate system where `(0, 0)` is at the top left corner but in `PixieCanvas` the `(0, 0)` coordinate is always in the center.
* `System.Drawing.Graphics` uses **pixel coordinates** where `PixieCanvas` uses **unit coordinates** in a **unit plane**. This enables it to automatically scale your drawings to the size of the canvas you're using.

### notes
If the client _has_ to be aware of the dimensions of the bitmap being used for output then the burden of translating and scaling in order to _project_ everything on the bitmap is placed on the client as well. This is undesirable since we can easily do this scale-and-translate using a bit of math and code and some agreed-upon preconditions (such as working in the unit system).

* `PixieCanvas` currently depends on `System.Drawing.Common` but I'm planning to replace this with an adapter system soon so you can plug in any rendering device of your choice.
* `PixieCanvas` only works with **square bitmaps** for now. Dealing with rectangular shaped bitmaps is high on the priority list.

### examples
Using `PixieCanvas`:
```
using Pixie

...

using(var pixie = new PixieCanvas(200))
{
    // Draw stuff
    ...

    // If you're using LINQPad, otherwise write to disk or something
    pixie.Bitmap.Dump(); 
}
```

Clear bitmap with a solid color
```
pixie.Clear(System.Drawing.Color.Black.ToPixieColor());
```

Draw a line from the bottom left corner to the top right corner
```
pixie.DrawLine(System.Drawing.Color.White.ToPixieColor(), -1, -1, 1, 1)
```

### tips
It's better to define a _palette_ of colors that you want to use and select from those in your presentations. It's easy to just grab colors from the `System.Drawing.Color` class.

```
static readonly Pixie.Color White = System.Drawing.Color.White.ToPixieColor();
static readonly Pixie.Color Black = System.Drawing.Color.Black.ToPixieColor();
static readonly Pixie.Color Azure = System.Drawing.Color.Azure.ToPixieColor();
```

This gives you a few benefits:
* You will be able to more consistantly change the look and feel of your images
* Changing the look and feel will be easier
* A word like `Black` reads a lot easier than `Color.FromArgb(...)`
* Your images will be more consistent in look and feel

It's even better if you use that palette to define semantic names for your sections such as
```
static readonly Pixie.Color Heading = new {
    Color = White,
    Font = ...
};
```